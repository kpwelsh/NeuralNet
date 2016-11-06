using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNetModel
{

    public enum ActivationFunction { Sigmoid, ReLU, SoftPlus, Identity}
    public interface IActivationFunction
    {
        Vector<double> Of(Vector<double> x);
        Vector<double> Derivative(Vector<double> x);
    }

    public class SoftPlus : IActivationFunction
    {
        public Vector<double> Derivative(Vector<double> x)
        {
            Vector<double> ret = DenseVector.OfVector(x);
            for (var i = 0; i < ret.Count; i++)
                ret[i] = 1/(1 + Math.Exp(-ret[i]));
            return ret;
        }

        public Vector<double> Of(Vector<double> x)
        {
            Vector<double> ret = DenseVector.OfVector(x);
            for (var i = 0; i < ret.Count; i++)
                ret[i] = Math.Log(1 + Math.Exp(ret[i]));
            return ret;
        }
    }

    public class RectiLinear : IActivationFunction
    {
        public Vector<double> Derivative(Vector<double> x)
        {
            Vector<double> ret = DenseVector.OfVector(x);
            for (var i = 0; i < ret.Count; i++)
                ret[i] = ret[i] > 0 ? 1 : 0;
            return ret;
        }

        public Vector<double> Of(Vector<double> x)
        {
            Vector<double> ret = DenseVector.OfVector(x);
            for (var i = 0; i < ret.Count; i++)
                ret[i] = ret[i] > 0 ? ret[i] : 0;
            return ret;
        }
    }

    public class Identity : IActivationFunction
    {
        public Vector<double> Derivative(Vector<double> x)
        {
            Vector<double> ret = DenseVector.Create(x.Count, 1);
            return ret;
        }

        public Vector<double> Of(Vector<double> x)
        {
            Vector<double> ret = DenseVector.OfVector(x);
            return ret;
        }
    }
}
