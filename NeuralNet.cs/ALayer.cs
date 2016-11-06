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
        private ActivationFunction _activationFunction;
        public ActivationFunction ActivationFunction
        {
            get
            {
                return _activationFunction;
            }
            protected set
            {
                _activationFunction = value;

            }
        }
        public RegularizationMode RegMode
        {
            get;
            protected set;
        }

        protected IActivationFunction ActFunc;

        protected virtual void SetActivationFunction(ActivationFunction a)
        {
            switch (a)
            {
                case ActivationFunction.Sigmoid:
                    ActFunc = new Sigmoid();
                    break;
                case ActivationFunction.ReLU:
                    ActFunc = new RectiLinear();
                    break;
                case ActivationFunction.SoftPlus:
                    ActFunc = new SoftPlus();
                    break;
                case ActivationFunction.Identity:
                    ActFunc = new Identity();
                    break;
                default:
                    throw new NNException("No available implementation for activation funciton: " + a.ToString());
            }
        }

        internal abstract Vector<double> Process(Vector<double> input);
        internal abstract Vector<double> PropogateError(Vector<double> outputError, double errorWeight, Vector<double> inputCacheOverride = null);
        internal abstract void ApplyUpdate();

    }
}
