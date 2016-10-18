using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNet.cs
{

    [Serializable]
    public class Sigmoid : IActivationFunction
    {
        public Vector<double> Derivative(Vector<double> input)
        {
            Vector<double> ret = new DenseVector(input.Count);
            for (var i = 0; i < input.Count; i++)
                ret[i] = Derivative(input[i]);
            return ret;
        }

        public Vector<double> Of(Vector<double> input)
        {
            Vector<double> ret = new DenseVector(input.Count);
            for (var i = 0; i < input.Count; i++)
                ret[i] = Of(input[i]);
            return ret;
        }

        private double Of(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }
        private double Derivative(double x)
        {
            double s = Of(x);
            return s * (1 - s);
        }
    }
}
