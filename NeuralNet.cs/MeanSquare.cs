using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetModel
{
    [Serializable]
    public class MeanSquare : ICostFunction
    {
        public Vector<double> Derivative(Vector<double> expected, Vector<double> actual)
        {
            return actual - expected;
        }

        public double Of(Vector<double> expected, Vector<double> actual)
        {
            Vector<double> diff = expected - actual;
            return (diff * diff) / 2;
        }
    }
}
