using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNetModel
{
    [Serializable]
    public class CrossEntropy : ICostFunction
    {
        public Vector<double> Derivative(Vector<double> expected, Vector<double> actual)
        {
            Vector<double> ret = new DenseVector(expected.Count);
            for (var i = 0; i < expected.Count; i++)
                ret[i] = expected[i] / actual[i] - (1 - expected[i]) / (1 - actual[i]);
            return -ret;
        }

        public double Of(Vector<double> expected, Vector<double> actual)
        {
            double sum = 0;
            for (var i = 0; i < expected.Count; i++)
                sum -= expected[i] * Math.Log(actual[i]) + (1 - expected[i]) * Math.Log(1 - actual[i]);
            return sum / Math.Log(2); // Note: convert from Ln to Log2
        }
    }
}
