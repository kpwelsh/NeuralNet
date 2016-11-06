using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetModel
{
    public enum CostFunction { MeanSquare, CrossEntropy }
    public interface ICostFunction
    {
        double Of(Vector<double> expected, Vector<double> actual);
        Vector<double> Derivative(Vector<double> expected, Vector<double> actual);
    }
}
