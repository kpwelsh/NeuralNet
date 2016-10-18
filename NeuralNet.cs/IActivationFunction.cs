using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNet.cs
{
    interface IActivationFunction
    {
        Vector<double> Of(Vector<double> x);
        Vector<double> Derivative(Vector<double> x);
    }
}
