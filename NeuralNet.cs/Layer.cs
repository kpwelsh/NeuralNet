using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;

namespace NeuralNetModel
{
    public class Layer : ALayer
    {
        public Matrix<double> Weights;
        public Vector<double> Biases;

        private Vector<double> BiasErrorCache;
        private Matrix<double> WeightErrorCache;
        private Vector<double> InputCache;

        private Normal gaussDist;
        private double NormalizationWeight = 0.01;

        private bool Configured = true;

        internal Layer(int? inputDimension = null,int? outputDimension = null, ActivationFunction? actFunc = null, RegularizationMode regMode = RegularizationMode.None)
        {
            SetParameters(inputDimension, outputDimension, actFunc, regMode);
        }
        

        private void InitializeWeights()
        {
            gaussDist = new Normal(0, 1 / Math.Sqrt(OutputDimension));
            
            Biases = DenseVector.CreateRandom(OutputDimension, gaussDist);
            Weights = DenseMatrix.CreateRandom(InputDimension, OutputDimension, gaussDist);

            BiasErrorCache = new DenseVector(OutputDimension);
            WeightErrorCache = new DenseMatrix(InputDimension, OutputDimension);
            InputCache = new DenseVector(InputDimension);
        }

        internal void SetParameters(int? inputDimension = null, int? outputDimension = null, ActivationFunction? actFunc = null, RegularizationMode? regMode = null)
        {
            if (inputDimension != null)
                InputDimension = (int)inputDimension;
            if (outputDimension != null)
                OutputDimension = (int)outputDimension;
            if (actFunc != null)
                SetActivationFunction((ActivationFunction)actFunc);
            if (regMode != null)
                RegMode = (RegularizationMode)regMode;

            if (this.inputDimension != null && this.outputDimension != null)
                InitializeWeights();

            Configured = this.inputDimension != null && this.outputDimension != null && ActFunc != null;
        }

        internal override Vector<double> Process(Vector<double> input)
        {
            InputCache = input;
            return ActFunc.Of(input * Weights);
        }

        internal override Vector<double> PropogateError(Vector<double> outputError, double errorWeight,Vector<double> inputCacheOverride = null)
        {
            Vector<double> inputError;

            if(inputCacheOverride == null)
                inputError = outputError.PointwiseMultiply(ActFunc.Derivative(InputCache * Weights));
            else
                inputError = outputError.PointwiseMultiply(ActFunc.Derivative(inputCacheOverride * Weights));

            BiasErrorCache -= inputError * errorWeight;
            WeightErrorCache -= errorWeight * InputCache.OuterProduct(inputError);
            if (RegMode == RegularizationMode.L2)
            {
                WeightErrorCache -= errorWeight * NormalizationWeight * Weights;
            }
            return Weights * inputError;
        }

        /// <summary>
        /// Consumes the cached Weight and Bias errors to update the weights and biases for the layer.
        /// </summary>
        internal override void ApplyUpdate()
        {
            Weights += WeightErrorCache;
            Biases += BiasErrorCache;
            WeightErrorCache.Clear();
            Biases.Clear();
        }

        internal override double WeightMagnitude()
        {
            return Weights.L1Norm() / (OutputDimension * InputDimension);
        }
    }
}
