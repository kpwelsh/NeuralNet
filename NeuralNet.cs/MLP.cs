using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace NeuralNetModel
{
    [Serializable]
    public class MLP : ANet
    {
        #region Constructor
        public MLP()
        {
            SetParameters(1, CostFunction.MeanSquare);
            Layers = new List<ALayer>();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Performs SGD on a set of training data using a mini-batch size provided.
        /// </summary>
        /// <param name="trainingSet"></param>
        /// <param name="batchSize"></param>
        /// <returns>The average cost function evaluation for each batch</returns>
        internal override void Learn(HashSet<TrainingData> trainingSet,int batchSize)
        {
            Vector<double> output;
            int count = 0;
            double cost = 0;
            int batchNumber = 0;
            foreach(TrainingData td in trainingSet)
            {
                output = Process(td.Data);

                cost += CostFunc.Of(td.Response, output) / batchSize;
                PropogateError(CostFunc.Derivative(td.Response, output), batchSize);

                count++;
                if (count > 0 && count % batchSize == 0)
                {
                    batchNumber++;
                    LastCost = cost;
                    Hook?.Invoke(batchNumber, this); // Trigger the batch level external control
                    ApplyError();
                    count = 0;
                    cost = 0;
                }
                if (Abort)
                {
                    return;
                }
            }
        }

        internal override Vector<double> Process(Vector<double> input)
        {
            Vector<double> output = DenseVector.OfVector(input);
            foreach (ALayer l in Layers)
                output = l.Process(output);
            return output;
        }

        internal override double Test(HashSet<TrainingData> testData)
        {
            double nRight = 0;
            foreach (TrainingData td in testData)
            {
                if (SoftMax(Process(td.Data)).MaximumIndex() == td.Response.MaximumIndex())
                    nRight++;
            }

            return 1 - nRight / testData.Count;
        }
        
        #endregion

        #region Private Methods
        private void PropogateError(Vector<double> outputError, int batchSize)
        {
            for(var i = Layers.Count-1; i>=0; i--)
            {
                outputError = Layers[i].PropogateError(outputError,LearningRate/batchSize);
            }
        }

        private void ApplyError()
        {
            foreach (ALayer l in Layers)
                l.ApplyUpdate();
        }

        private Vector<double> SoftMax(Vector<double> x)
        {
            Vector<double> ret = DenseVector.OfVector(x);
            double sum = 0;
            for (var i = 0; i < ret.Count; i++)
            {
                ret[i] = Math.Exp(ret[i]);
                sum += ret[i];
            }
            for (var i = 0; i < ret.Count; i++)
                ret[i] /= sum;
            return ret;
        }


        #endregion
    }
}
