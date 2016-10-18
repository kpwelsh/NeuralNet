using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNet.cs
{
    class Net
    {
        private List<ILayer> Layers;
        public ILayer this[int n]
        {
            get
            {
                return Layers[n];
            }
            set
            {
                Layers[n] = value;
            }
        }

        public enum CostFunction { MeanSquare, CrossEntropy}
        private ICostFunction CostFunc;
        private double LearningRate;

        public Net(CostFunction costFunc,double learningRate)
        {
            Layers = new List<ILayer>();
            SetParameters(learningRate, costFunc);
        }

        public void SetParameters(double? learningRate=null, CostFunction? costFunc = null)
        {
            if(costFunc!=null)
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
        }

        public void Add(ILayer layer)
        {
            Layers.Add(layer);
        }

        /// <summary>
        /// Performs SGD on a set of training data using a mini-batch size provided.
        /// </summary>
        /// <param name="trainingSet"></param>
        /// <param name="batchSize"></param>
        /// <returns>The average cost function evaluation for each batch</returns>
        public List<double> Learn(HashSet<TrainingData> trainingSet,int batchSize)
        {
            int n = trainingSet.Count;
            int i = 0;
            Vector<double> output;
            List<double> costs = new List<double>();
            double cost = 0;
            foreach(TrainingData td in trainingSet)
            {
                output = Process(td.Data);

                cost += CostFunc.Of(td.GetLabelVector(), output)/batchSize;
                PropogateError(CostFunc.Derivative(td.GetLabelVector(), output),batchSize);

                i++;
                if (i % batchSize == 0)
                {
                    costs.Add(cost);
                    cost = 0;
                    ApplyError();
                    i = 0;
                }
            }

            return costs;
        }

        public Vector<double> Process(Vector<double> input)
        {
            Vector<double> output = DenseVector.OfVector(input);
            foreach (ILayer l in Layers)
                output = l.Process(output);
            return output;
        }

        public double TestClassification(HashSet<TrainingData> testData)
        {
            double nRight = 0;
            foreach(TrainingData td in testData)
            {
                if (Process(td.Data).MaximumIndex() == td.GetLabelVector().MaximumIndex())
                    nRight++;
            }

            return nRight / testData.Count;
        }

        private void PropogateError(Vector<double> outputError, int batchSize)
        {
            for(var i = Layers.Count-1; i>=0; i--)
            {
                outputError = Layers[i].PropogateError(outputError,LearningRate/batchSize);
            }
        }

        private void ApplyError()
        {
            foreach (ILayer l in Layers)
                l.ApplyUpdate();
        }
    }
}
