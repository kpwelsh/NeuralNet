using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNet.cs
{
    interface ILayer
    {
        Vector<double> Process(Vector<double> input);
        Vector<double> PropogateError(Vector<double> outputError, double errorWeight, Vector<double> inputCacheOverride = null);
        void ApplyUpdate();
        string DimensionString();
        int GetInputDimension();
        int GetOutputDimension();
    }
}
