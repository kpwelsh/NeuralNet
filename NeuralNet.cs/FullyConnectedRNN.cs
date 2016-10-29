using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNet.cs
{
    class FullyConnectedRNN
    {
        private ILayer HiddenLayer;
        private ILayer OutputLayer;
        private List<Vector<double>> HiddenCache;
        private List<Vector<double>> OutputCache;
        private int MaxMemory;
        private readonly int _maxMemory = 1000;
        private double TimeStep;
        private ICostFunction CostFunc;
        private double LearningRate;

        private int HiddenDimension;
        private int OutputDimension;
        private bool ForceOutput = false;
        private Vector<double> PreviousLabel;

        public FullyConnectedRNN(double timeStep, int? memory = null)
        {
            MaxMemory = memory == null ? _maxMemory : (int)memory;
            TimeStep = timeStep;

            HiddenCache = new List<Vector<double>>();
            OutputCache = new List<Vector<double>>();
        }

        public void SetHiddenLayer(ILayer layer)
        {
            HiddenLayer = layer;
            HiddenDimension = layer.GetOutputDimension();
        }

        public void SetOutputLayer(ILayer layer)
        {
            OutputLayer = layer;
            OutputDimension = layer.GetOutputDimension();
        }

        public void SetParameters(double? learningRate = null, CostFunction? costFunc = null, bool? forceOutput = null)
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

        public void WipeMemory()
        {
            HiddenCache.Clear();
            OutputCache.Clear();
            HiddenCache.Add(new DenseVector(HiddenDimension));
            OutputCache.Add(new DenseVector(OutputDimension));
        }

        public List<double> Learn(HashSet<List<TrainingData>> trainSet,int batchSize = 1)
        {
            int count = 0;
            Vector<double> prevOutput;
            List<double> costs = new List<double>();
            double cost = 0;
            foreach(List<TrainingData> ts in trainSet)
            {
                count++;
                WipeMemory();
                PreviousLabel = null;
                foreach (TrainingData td in ts)
                {
                    prevOutput = Process(td.Data);
                    PreviousLabel = td.GetLabelVector();
                    cost += CostFunc.Of(ts.Last().GetLabelVector(), prevOutput) / ts.Count;
                }

                PropogateError(ts, batchSize);
                if (count >= batchSize)
                {
                    ApplyError();
                    costs.Add(cost / count);
                    Console.WriteLine(costs.Last());
                    cost = 0;
                    count = 0;
                }
            }
            return costs;
        }

        public double TestAccuracy(HashSet<List<TrainingData>> ts)
        {
            WipeMemory();
            double err = 0;
            foreach (List<TrainingData> td in ts)
            {
                err += TestAccuracy(td);
            }
            return err / ts.Count;
        }

        public double TestAccuracy(List<TrainingData> ts)
        {
            double err = 0;
            Vector<double> output;
            WipeMemory();
            foreach(TrainingData td in ts)
            {
                output = Process(td.Data);
                err += (output - td.GetLabelVector()).L1Norm();
            }
            return err / ts.Count;
        }
        
        public void PropogateError(List<TrainingData> ts,int batchSize)
        {
            Vector<double> outputError = new DenseVector(OutputDimension);
            Vector<double> hiddenError = new DenseVector(HiddenDimension);
            Vector<double> jointError;
            Vector<double> jointInput;
            for(var i = OutputCache.Count - 1; i > 0; i--)
            {
                // Add the error on the output layer from the training data.
                outputError += CostFunc.Derivative(ts[i - 1].GetLabelVector(), OutputCache[i-1]);

                // Add the error on the hidden layer from the output layer.
                hiddenError += OutputLayer.PropogateError(outputError, LearningRate/batchSize, HiddenCache[i]);

                // Get the input that went into the hidden layer at this time step.
                if (i == 1)
                    jointInput = Concatenate(HiddenLayer.GetInputDimension(), HiddenCache[i - 1], OutputCache[0], ts.First().Data);
                else
                    jointInput = Concatenate(HiddenLayer.GetInputDimension(), HiddenCache[i - 1], OutputCache[i - 1], ts[i - 1].Data);
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

        public Vector<double> Process(Vector<double> input)
        {
            if (PreviousLabel == null)
                PreviousLabel = new DenseVector(OutputLayer.GetOutputDimension());
            Vector<double> fullInput;
            if(ForceOutput)
                fullInput = Concatenate(HiddenLayer.GetInputDimension(), HiddenCache.Last(), PreviousLabel, input);
            else
                fullInput = Concatenate(HiddenLayer.GetInputDimension(), HiddenCache.Last(), OutputCache.Last(), input);

            HiddenCache.Add(HiddenLayer.Process(fullInput));
            OutputCache.Add(OutputLayer.Process(HiddenCache.Last()));
            if(OutputCache.Count > MaxMemory)
            {
                HiddenCache.RemoveAt(0);
                OutputCache.RemoveAt(0);
            }
            return OutputCache.Last();
        }

    }
}
