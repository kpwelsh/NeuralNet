using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNetModel
{
    class AutoEncoder : ANet
    {
        public override int Count
        {
            get
            {
                return Layers.Count/2;
            }
        }
        public double ActivationGoal
        {
            get;
            private set;
        }
        public double SparsityWeight;
        private Vector<double>[] AverageActivations;

        public AutoEncoder(double activationGoal = 0.05, double sparsityWeight = 0.01)
        {
            SetParameters(learningRate: 1, costFunc: CostFunction.MeanSquare);
            Layers = new List<ALayer>();
            ActivationGoal = activationGoal;
            SparsityWeight = sparsityWeight;
        }

        #region Internal Methods
        internal override void Learn(HashSet<TrainingData> trainingSet, int batchSize)
        {
            batchSize = Math.Min(batchSize, trainingSet.Count);
            Vector<double> output;
            int count = 0;
            double cost = 0;
            int batchNumber = 0;
            foreach (TrainingData td in trainingSet)
            {

                output = Process(td.Data);

                // The autoencoder only cares about the data, and not the response.
                cost += CostFunc.Of(td.Data, output) / batchSize; 
                PropogateError(CostFunc.Derivative(td.Data, output), batchSize);

                count++;
                if (Abort)
                {
                    CleanUp();
                    return;
                }

                if (count > 0 && count % batchSize == 0)
                {
                    batchNumber++;
                    LastCost = cost;
                    InhibitActivation(trainingSet, batchSize);
                    ApplyError();
                    if (!Abort) // Trying to make this kind of threadsafe
                        Hook?.Invoke(batchNumber, this); // Trigger the batch level external control
                    count = 0;
                    cost = 0;
                }
            }
            CleanUp();
            return;
        }

        internal override Vector<double> Process(Vector<double> input)
        {
            Vector<double> output = DenseVector.OfVector(input);
            for(var i = 0; i < Layers.Count; i++)
            {
                output = Layers[i].Process(output);
                if (AverageActivations != null && i < Count)
                    AverageActivations[i] += output;

            }
            return output;
        }

        internal override double Test(HashSet<TrainingData> testSet)
        {
            double err = 0;
            foreach(var td in testSet)
            {
                Vector<double> output = Process(td.Data);
                err += (output - td.Data).L2Norm() / td.Response.L2Norm();
            }

            return err / testSet.Count;
        }

        internal override void Add(ALayer layer, int? pos = null)
        {
            pos = pos ?? Count;
            if (!(layer is Layer))
                throw new NNException("Cannot autoencode without a non-standard layer type.");
            base.Add(layer, pos);
            Layer l = new Layer(layer.OutputDimension, layer.InputDimension, layer.ActivationFunction, layer.RegMode);
            l.SetWeights((layer as Layer)?.Weights.Transpose());
            base.Add(l, base.Count - pos);
        }

        internal  void SetParameters(double? learningRate = default(double?), CostFunction? costFunc = default(CostFunction?),
            double? activationGoal = null, double? sparsityWeight = null)
        {
            base.SetParameters(learningRate, costFunc);
            ActivationGoal = activationGoal ?? ActivationGoal;
            SparsityWeight = sparsityWeight ?? SparsityWeight;
        }
        #endregion

        #region Private Methods
        private void PropogateError(Vector<double> outputError, int batchSize)
        {
            for (var i = Layers.Count - 1; i >= 0; i--)
            {
                outputError = Layers[i].PropogateError(outputError, LearningRate / batchSize);
            }
        }

        private void InhibitActivation(HashSet<TrainingData> data, int batchSize)
        {
            if (SparsityWeight <= 0)
                return;
            Vector<double> outputError = DenseVector.Create(Layers[Count-1].OutputDimension,0);
            AverageActivations = new Vector<double>[Count];
            for (var i = 0; i < AverageActivations.Length; i++)
                AverageActivations[i] = new DenseVector(Layers[i].OutputDimension);

            CalcAvgActivation(data);

            for (var i = Count-1; i>= 0; i--)
                outputError = Layers[i].PropogateError(outputError + GetActivationError(AverageActivations[i]), LearningRate / batchSize);

            AverageActivations = null;
        }

        private Vector<double> GetActivationError(Vector<double> activations)
        {
            Vector<double> error = new DenseVector(activations.Count);
            double v = 0;
            for (var i = 0; i < activations.Count; i++)
            {
                v = LearningRate * SparsityWeight * ((1 - ActivationGoal) / (1 - activations[i]) - activations[i] / ActivationGoal);
                error[i] = v;
            }

            return error;
        }

        private void CalcAvgActivation(HashSet<TrainingData> dataSet)
        {
            foreach (var td in dataSet)
                Process(td.Data);
            for (var i = 0; i < AverageActivations.Length; i++)
                AverageActivations[i] /= dataSet.Count;
        }

        private void ApplyError()
        {
            foreach (ALayer l in Layers)
                l.ApplyUpdate();
        }

        private void CleanUp()
        {
            AverageActivations = null;
        }
        #endregion
    }
}
