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

        public Layer(int inputDimension,int outputDimension, ActivationFunction actFunc)
        {
            gaussDist = new Normal(0, 1 / Math.Sqrt(outputDimension));

            Output = new DenseVector(outputDimension);
            Biases = DenseVector.CreateRandom(outputDimension,gaussDist);
            Weights = DenseMatrix.CreateRandom(inputDimension, outputDimension, gaussDist);

            BiasErrorCache = new DenseVector(outputDimension);
            WeightErrorCache = new DenseMatrix(inputDimension, outputDimension);
            InputCache = new DenseVector(inputDimension);

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
                default:
                    throw new NNException("No available implementation for activation funciton: " + actFunc.ToString());
            }
        }

        public Vector<double> Process(Vector<double> input)
        {
            InputCache = input;
            Output = ActFunc.Of(input * Weights);
            return Output;
        }

        public Vector<double> PropogateError(Vector<double> outputError, double errorWeight)
        {
            Vector<double> inputError = outputError.PointwiseMultiply(ActFunc.Derivative(InputCache * Weights));
            BiasErrorCache -= inputError * errorWeight;
            WeightErrorCache -= errorWeight * InputCache.OuterProduct(inputError);
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
    }
}
