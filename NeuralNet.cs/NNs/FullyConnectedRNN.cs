using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNetModel
{
    class FullyConnectedRNN : ANet
    {
        private List<List<Vector<double>>> OutputCache;
        private int MaxMemory;
        private double TimeStep;
        private bool ForceOutput = false;
        private Vector<double> PreviousResponse;

        #region Internal
        internal FullyConnectedRNN(double timeStep, int memory = 1)
        {
            MaxMemory = memory;
            TimeStep = timeStep;
            
            OutputCache = new List<List<Vector<double>>>();
            Layers = new List<ALayer>();
        }
        
        internal void SetParameters(double? learningRate = null, CostFunction? costFunc = null, bool? forceOutput = null)
        {
            if (costFunc != null)
            {
                switch (costFunc)
                {
                    case CostFunction.MeanSquare:
                        CostFunc = new MeanSquare();
                        break;
                    case CostFunction.CrossEntropy:
                        CostFunc = new CrossEntropy();
                        break;
                }
            }
            if (learningRate != null)
                LearningRate = (double)learningRate;
            if (forceOutput != null)
                ForceOutput = (bool)forceOutput;
        }

        internal void WipeMemory()
        {
            OutputCache.Clear();
            List<Vector<double>> zeros = new List<Vector<double>>();

            // Add a zero for the input for the first iteration of the first layer.
            if (Layers[0].InputDimension <= Layers[0].OutputDimension) 
                zeros.Add(null);
            else
                zeros.Add(new DenseVector(Layers.First().InputDimension - Layers.First().OutputDimension));

            foreach (ALayer layer in Layers)
                zeros.Add(new DenseVector(layer.OutputDimension));
            OutputCache.Add(zeros);
        }

        internal override void Learn(HashSet<TrainingData> trainSet,int batchSize = 1)
        {
            batchSize = Math.Min(batchSize, trainSet.Count);
            Vector<double> output;
            double cost = 0;
            int nBatch = 0;
            foreach(TrainingData trainSeq in trainSet)
            {
                // Start over for each different training sequence provided.
                WipeMemory();
                PreviousResponse = null;

                for(var i = 0; i < trainSeq.Count; i++)
                {
                    // Process the pair
                    TrainingData.TrainingPair pair = trainSeq[i];
                    output = Process(pair.Data);
                    cost += CostFunc.Of(trainSeq[i].Response, output) / (batchSize * MaxMemory);
                    // If we have completely overwriten our short term memory, then 
                    // update the weights based on how we performed this time.
                    if (i > 0 && i % MaxMemory == 0)
                    {
                        PropogateError(trainSeq.SubSequence(Math.Min(i - MaxMemory, 0), i), batchSize);
                    }
                    // Count batches by number of error propogations
                    if (i % (batchSize * MaxMemory) == 0)
                    {
                        nBatch++;
                        LastCost = cost;
                        Hook?.Invoke(nBatch, this);
                        ApplyError();
                        cost = 0;
                    }

                    // Keep the last... uhh. this is a PIPI (Parallel Implementation Prone to Inconsistency)
                    // See this.Process
                    if (ForceOutput)
                        PreviousResponse = pair.Response;
                    else
                        PreviousResponse = output;
                    if (Abort)
                    {
                        Abort = false;
                        return;
                    }
                }

            }
        }

        internal override double Test(HashSet<TrainingData> ts)
        {
            WipeMemory();
            double err = 0;
            foreach (TrainingData td in ts)
            {
                err += TestOne(td);
            }
            return err / ts.Count;
        }

        internal override Vector<double> Process(Vector<double> input)
        {
            Vector<double> fullInput;
            List<Vector<double>> nextOutputCache = new List<Vector<double>>();

            nextOutputCache.Add(input);
            for (var i = 0; i < Layers.Count; i++)
            {
                // First, get the real full input to the current layer. fullInput = <Last Layer State> + <Previous Layer Output (Can be null for ESMs)>
                // The output cache is structured as follows: [Training Input, Layer 0 output, Layer 1 output ... Last layer Output]
                // The output of layer i is in OutputCache[i + 1] 
                if (ForceOutput && PreviousResponse != null)
                    fullInput = Concatenate(Layers[i].InputDimension, OutputCache.Last()[i + 1], nextOutputCache[i]);
                else
                    fullInput = Concatenate(Layers[i].InputDimension, OutputCache.Last()[i + 1], nextOutputCache[i]);

                input = Layers[i].Process(fullInput);
                nextOutputCache.Add(input);
            }
            
            OutputCache.Add(nextOutputCache);
            // Keep 1 more layer of memory than really, so we can use it for input overriding.
            // The max memory is really how far back you can back prop.
            if (OutputCache.Count > MaxMemory + 1)
            {
                OutputCache.RemoveAt(0);
            }
            return OutputCache.Last().Last();
        }

        internal double TestOne(TrainingData ts)
        {
            double err = 0;
            Vector<double> output;
            WipeMemory();
            foreach (TrainingData.TrainingPair pair in ts)
            {
                output = Process(pair.Data);
                err += (output - pair.Response).L1Norm();
                PreviousResponse = pair.Response;
            }
            return err / ts.Count;
        }
        #endregion

        #region Private Methods
        private void PropogateError(TrainingData ts,int batchSize)
        {
            // Initialize the error from the future
            List<Vector<double>> futureErrorCache = new List<Vector<double>>();
            for (var i = 0; i < Layers.Count; i++)
                futureErrorCache.Add(new DenseVector(Layers[i].OutputDimension)); // Only store the error relevant to that layer

            // Step backwards through the memory
            for(var t = OutputCache.Count - 1; t > 0; t--)
            {
                // Error on the output layer from the training data
                Vector<double> error = CostFunc.Derivative(ts[t - 1].Response, OutputCache[t].Last());
                // Step backwards through the net.
                for(var i = Layers.Count - 1; i >= 0; i--)
                {
                    error += futureErrorCache[i];
                    Vector<double> lastInput = Concatenate(Layers[i].InputDimension, OutputCache[t - 1][i + 1], OutputCache[t][i]);// [t-1][i+1] is the output of the current layer at a previous time
                    Vector<double> jointInputError = Layers[i].PropogateError(error, LearningRate/batchSize, lastInput);
                    Vector<double> pastStateError;

                    // If this is the first layer, error would be the error on the training input, so we can just ignore it.
                    Split(jointInputError, Layers[i].OutputDimension, out pastStateError, out error, OutputCache[t][i]?.Count ?? 1);
                    futureErrorCache[i] = pastStateError; // Store the most recent error from the future.
                }
            }
        }

        private void ApplyError()
        {
            foreach (ALayer l in Layers)
                l.ApplyUpdate();
        }

        /// <summary>
        /// Combines Vectors until the max size has been hit.
        /// </summary>
        /// <param name="catSize"></param>
        /// <param name="vectors"></param>
        /// <returns></returns>
        private Vector<double> Concatenate(int catSize, params Vector<double>[]  vectors)
        {
            Vector<double> cat = new DenseVector(catSize);

            // Concatenate the input with the previous state.
            for (var i = 0; i < cat.Count; i++)
            {
                int length = 0;
                int vecIndex = i;
                for(var j = 0; j < vectors.Length; j++)
                {
                    if(length + vectors[j].Count > i)
                    {
                        vecIndex = j;
                        break;
                    }
                    length += vectors[j].Count;
                }
                cat[i] = vectors[vecIndex][i - length];
            }

            return cat;
        }

        /// <summary>
        /// Splits a vector into two pieceso of variable size.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="n"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v2Size"></param>
        private void Split(Vector<double> v, int n, out Vector<double> v1, out Vector<double> v2, int? v2Size = null)
        {
            v1 = new DenseVector(n);
            if (v2Size == null)
                v2 = new DenseVector(v.Count - n);
            else
                v2 = new DenseVector((int)v2Size);

            for (var i = 0; i < v.Count; i++)
            {
                if (i < n)
                    v1[i] = v[i];
                else
                    v2[i - n] = v[i];
            }
        }
        #endregion

    }
}
