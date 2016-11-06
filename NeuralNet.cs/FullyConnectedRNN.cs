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
        private ALayer HiddenLayer;
        private ALayer OutputLayer;
        private List<Vector<double>> HiddenCache;
        private List<Vector<double>> OutputCache;
        private int MaxMemory;
        private double TimeStep;
        private int HiddenDimension;
        private int OutputDimension;
        private bool ForceOutput = false;
        private Vector<double> PreviousResponse;

        #region Internal
        internal FullyConnectedRNN(double timeStep, int memory = 1)
        {
            MaxMemory = memory;
            TimeStep = timeStep;

            HiddenCache = new List<Vector<double>>();
            OutputCache = new List<Vector<double>>();
        }

        internal void SetHiddenLayer(ALayer layer)
        {
            HiddenLayer = layer;
            HiddenDimension = layer.OutputDimension;
        }

        internal void SetOutputLayer(ALayer layer)
        {
            OutputLayer = layer;
            OutputDimension = layer.OutputDimension;
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
            HiddenCache.Clear();
            OutputCache.Clear();
            HiddenCache.Add(new DenseVector(HiddenDimension));
            OutputCache.Add(new DenseVector(OutputDimension));
        }

        internal override void Learn(HashSet<TrainingData> trainSet,int batchSize = 1)
        {
            int count = 0;
            Vector<double> output;
            double cost = 0;
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

                    // If we have completely overwriten our short term memory, then 
                    // update the weights based on how we performed this time.
                    count++;
                    if (count % MaxMemory == 0)
                    {
                        PropogateError(trainSeq.SubSequence(Math.Min(i - MaxMemory + 1, 0), i + 1), batchSize);
                    }
                    // Count batches by number of error propogations
                    if (count % (batchSize * MaxMemory) == 0)
                    {
                        BatchLevelPP?.Invoke(cost);
                        ApplyError();
                        cost = 0;
                    }
                    cost += CostFunc.Of(trainSeq[i].Response, output) / (batchSize * MaxMemory);

                    // Keep the last... uhh. this is a PIPI (Parallel Implementation Prone to Inconsistency)
                    // See this.Process
                    if (ForceOutput)
                        PreviousResponse = pair.Response;
                    else
                        PreviousResponse = output;
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
            if (ForceOutput && PreviousResponse != null)
                fullInput = Concatenate(HiddenLayer.InputDimension, HiddenCache.Last(), PreviousResponse, input);
            else
                fullInput = Concatenate(HiddenLayer.InputDimension, HiddenCache.Last(), OutputCache.Last(), input);

            HiddenCache.Add(HiddenLayer.Process(fullInput));
            OutputCache.Add(OutputLayer.Process(HiddenCache.Last()));
            if (OutputCache.Count > MaxMemory)
            {
                HiddenCache.RemoveAt(0);
                OutputCache.RemoveAt(0);
            }
            return OutputCache.Last();
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
            Vector<double> outputError = new DenseVector(OutputDimension);
            Vector<double> hiddenError = new DenseVector(HiddenDimension);
            Vector<double> jointError;
            Vector<double> jointInput;
            for(var i = OutputCache.Count - 1; i >= 0; i--)
            {
                // Add the error on the output layer from the training data.
                outputError += CostFunc.Derivative(ts[i].Response, OutputCache[i]);

                // Add the error on the hidden layer from the output layer.
                hiddenError += OutputLayer.PropogateError(outputError, LearningRate/batchSize, HiddenCache[i]);

                if (i == 0)
                    break;
                // Get the input that went into the hidden layer at this time step.
                jointInput = Concatenate(HiddenLayer.InputDimension, HiddenCache[i - 1], OutputCache[i - 1], ts[i - 1].Data);
                jointError = HiddenLayer.PropogateError(hiddenError, LearningRate/batchSize, jointInput);
                Split(jointError, HiddenDimension, out hiddenError, out outputError, OutputDimension);
            }
        }

        private void ApplyError()
        {
            HiddenLayer.ApplyUpdate();
            OutputLayer.ApplyUpdate();
        }

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
