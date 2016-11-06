using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetModel
{
    public enum RegularizationMode { L1, L2, Dropout, None }
    public abstract class ALayer
    {
        protected int? outputDimension;
        public int OutputDimension
        {
            get
            {
                return outputDimension ?? 0;
            }
            protected set
            {
                outputDimension = value;
            }
        }
        protected int? inputDimension;
        public int InputDimension
        {
            get
            {
                return inputDimension ?? 0;
            }
            protected set
            {
                inputDimension = value;
            }
        }
        public IActivationFunction ActFunc
        {
            get;
            protected set;
        }
        public RegularizationMode RegMode
        {
            get;
            protected set;
        }

        internal abstract Vector<double> Process(Vector<double> input);
        internal abstract Vector<double> PropogateError(Vector<double> outputError, double errorWeight, Vector<double> inputCacheOverride = null);
        internal abstract void ApplyUpdate();

    }
}
