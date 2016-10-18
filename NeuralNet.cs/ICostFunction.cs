using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNet.cs
{
    interface ICostFunction
    {
        double Of(Vector<double> expected, Vector<double> actual);
        Vector<double> Derivative(Vector<double> expected, Vector<double> actual);
    }
}
