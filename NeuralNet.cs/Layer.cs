using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

namespace NeuralNet.cs
{
    class Layer : ILayer
    {
        public Vector<double> Output;
        public Matrix<double> Weights;
        public Vector<double> Biases;

        private Vector<double> BiasErrorCache;
        private Matrix<double> WeightErrorCache;
        private Vector<double> InputCache;
        private IActivationFunction ActFunc;

        private Normal gaussDist;
        private bool? Normalize;
        private double NormalizationWeight = 0.1;

        private bool Configured = true;

        private int? InputDimension;
        private int? OutputDimension;

        public Layer(int? inputDimension = null,int? outputDimension = null, ActivationFunction? actFunc = null, bool normalization = false)
        {
            SetParameters(inputDimension, outputDimension, actFunc, normalization);
        }

        private void SetActFunc(ActivationFunction? actFunc)
        {
            switch (actFunc)
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
                case null:
                    Configured = false;
                    ActFunc = null;
                    break;
                default:
                    throw new NNException("No available implementation for activation funciton: " + actFunc.ToString());
            }
        }

        private void InitializeWeights()
        {
            gaussDist = new Normal(0, 1 / Math.Sqrt((int)OutputDimension));

            Output = new DenseVector((int)OutputDimension);
            Biases = DenseVector.CreateRandom((int)OutputDimension, gaussDist);
            Weights = DenseMatrix.CreateRandom((int)InputDimension, (int)OutputDimension, gaussDist);

            BiasErrorCache = new DenseVector((int)OutputDimension);
            WeightErrorCache = new DenseMatrix((int)InputDimension, (int)OutputDimension);
            InputCache = new DenseVector((int)InputDimension);
        }

        public void SetParameters(int? inputDimension = null, int? outputDimension = null, ActivationFunction? actFunc = null, bool? normalization = null)
        {
            if (inputDimension != null)
                InputDimension = (int)inputDimension;
            if (outputDimension != null)
                OutputDimension = (int)outputDimension;
            if (actFunc != null)
                SetActFunc(actFunc);
            if (normalization != null)
                Normalize = (bool)normalization;

            if (InputDimension != null && OutputDimension != null)
                InitializeWeights();

            Configured = InputDimension != null && OutputDimension != null && ActFunc != null && Normalize != null;
        }

        public Vector<double> Process(Vector<double> input)
        {
            InputCache = input;
            Output = ActFunc.Of(input * Weights);
            return Output;
        }

        public Vector<double> PropogateError(Vector<double> outputError, double errorWeight,Vector<double> inputCacheOverride = null)
        {
            Vector<double> inputError;

            if(inputCacheOverride == null)
                inputError = outputError.PointwiseMultiply(ActFunc.Derivative(InputCache * Weights));
            else
                inputError = outputError.PointwiseMultiply(ActFunc.Derivative(inputCacheOverride * Weights));

            BiasErrorCache -= inputError * errorWeight;
            WeightErrorCache -= errorWeight * InputCache.OuterProduct(inputError);
            if ((bool)Normalize)
            {
                WeightErrorCache -= errorWeight * NormalizationWeight * Weights;
            }
            return Weights * inputError;
        }

        /// <summary>
        /// Consumes the cached Weight and Bias errors to update the weights and biases for the layer.
        /// </summary>
        public void ApplyUpdate()
        {
            Weights += WeightErrorCache;
            Biases += BiasErrorCache;
            WeightErrorCache.Clear();
            Biases.Clear();
        }

        public string DimensionString()
        {
            return InputDimension + "|" + OutputDimension;
        }

        public override string ToString()
        {
            return 
                "Input Dimension: " + (InputDimension != null ? "" + InputDimension : "-") + "\n" + 
                "Output Dimension: " + (OutputDimension != null ? "" + OutputDimension : "-") + "\n" + 
                "Activation Function: " + (ActFunc != null ? "" + ActFunc : "-") + "\n" + 
                "Normalization Weight: " + ((bool)Normalize ? NormalizationWeight : 0 );
        }

        public int GetInputDimension()
        {
            return (int)InputDimension;
        }

        public int GetOutputDimension()
        {
            return (int)OutputDimension;
        }
    }
}
