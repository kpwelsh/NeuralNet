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
    [Serializable]
    public class ConvLayer : ALayer
    {
        #region Properties
        public int Range
        {
            get;
            private set;
        }
        public int Stride
        {
            get;
            private set;
        }
        public int NFilters
        {
            get;
            private set;
        }
        public int InputHeight
        {
            get;
            private set;
        }
        public int InputWidth
        {
            get;
            private set;
        }
        public int InputDepth
        {
            get;
            private set;
        }
        public int OutputWidth
        {
            get;
            private set;
        }
        public int OutputDepth
        {
            get;
            private set;
        }
        public int OutputHeight
        {
            get;
            private set;
        }
        #endregion

        #region Private Fields
        private Vector<double> InputCache;
        private Vector<double> DirectInputCache;
        private Matrix<double>[] Weights;
        private double[] Biases;
        private Matrix<double>[] WeightErrorCache;
        private double[] BiasErrorCache;
        private Normal gaussDist;
        #endregion

        internal ConvLayer(int range, int stride, int nFilters, int inputWidth, int inputHeight, int inputDepth = 1)
        {
            Range = range;
            Stride = stride;
            NFilters = nFilters;
            InputWidth = inputWidth;
            InputHeight = inputHeight;
            InputDepth = inputDepth;
            Initialize();
            ActFunc = new Sigmoid();
        }
        
        internal void Initialize()
        {
            Weights = new Matrix<double>[NFilters];
            WeightErrorCache = new Matrix<double>[NFilters];
            gaussDist = new Normal(0, 1 / Range);
            for (var i = 0; i < Weights.Length; i++)
            {
                Weights[i] = DenseMatrix.CreateRandom(Range, Range, gaussDist);
                WeightErrorCache[i] = DenseMatrix.Create(Range, Range, 0);
            }

            Biases = new double[NFilters];
            BiasErrorCache = new double[NFilters];
            gaussDist = new Normal(0, 1);
            for (var i = 0; i < Biases.Length; i++)
            {
                Biases[i] = gaussDist.Sample();
                BiasErrorCache[i] = 0;
            }

            SetOutputDimensions();
            DirectInputCache = new DenseVector(OutputDimension);

        }
        internal override void ReInitialize()
        {
            Initialize();
        }

        #region ALayer Implementation
        internal override Vector<double> Process(Vector<double> input)
        {
            InputCache = input;

            for (var i = 0; i < OutputHeight; i++)
            {
                for (var j = 0; j < OutputWidth; j++)
                {
                    for (var inputFrame = 0; inputFrame < InputDepth; inputFrame++)
                    {
                        for (var f = 0; f < NFilters; f++)
                        {
                            // (inputFrame * NFilters + f) =  the output frame
                            // OutputHeight * OutputWidth = Number of pixels per frame
                            int outputIndex = (inputFrame * NFilters + f) * OutputHeight * OutputWidth + i * OutputWidth + j;
                            DirectInputCache[outputIndex] = ApplyFilter(input, inputFrame, f, i * Stride, j * Stride);
                        }
                    }
                }
            }
            return 10 * (ActFunc.Of(DirectInputCache) - 0.5);  // DEBUG 10*
        }

        internal override Vector<double> PropogateError(Vector<double> outputError, double errorWeight, Vector<double> inputCacheOverride = null, Vector<double> additionalError = null)
        {
            if (additionalError != null)
                throw new NNException("Additional error is not supported for Convolutional Layers");
            int nNodes = OutputHeight * OutputWidth;
            int nInputNodes = InputHeight * InputWidth;
            Vector<double> directInputError = 10*outputError.PointwiseMultiply(ActFunc.Derivative(DirectInputCache)); // DEBUG 10*
            Vector<double> inputError = new DenseVector(InputDimension);

            for (var channel = 0; channel < InputDepth; channel++)
            {
                for(var f = 0; f < NFilters; f++)
                {
                    int outputLayer = channel * NFilters + f;
                    for(var i = 0; i < OutputHeight; i++)
                    {
                        for(var j = 0; j < OutputWidth; j++)
                        {
                            double nodeError = directInputError[outputLayer * nNodes + i * OutputWidth + j];
                            BiasErrorCache[f] -= errorWeight * nodeError / nNodes; // Average the update over all instances of the same filter.

                            // Get the matrix of weight errors by multiplying the nodeError by the matrix in the input cache.
                            Matrix<double> weightError = new DenseMatrix(Weights[f].RowCount, Weights[f].ColumnCount);
                            for(var m = 0; m < weightError.RowCount; m++)
                            {
                                for(var n = 0; n < weightError.ColumnCount; n++)
                                {
                                    int inputIndex = channel * nInputNodes + (m + i * Stride) * InputWidth + n + j * Stride;
                                    weightError[m, n] = nodeError * InputCache[inputIndex];
                                    inputError[inputIndex] = Weights[f][m, n] * nodeError;
                                }
                            }

                            WeightErrorCache[f] -= errorWeight * weightError / nNodes; // Average the update over all instances of the same filter.
                        }
                    }
                }
            }

            return inputError;
        }

        internal override void ApplyUpdate()
        {
            for(var i = 0; i < NFilters; i++)
            {
                Weights[i] -= WeightErrorCache[i];
                Biases[i] -= BiasErrorCache[i];
                WeightErrorCache[i].Clear();
                BiasErrorCache[i] = 0;
            }
        }
        #endregion

        #region Private
        private void SetOutputDimensions()
        {
            float width = (InputWidth - Range + 1) / Stride;
            float height = (InputHeight - Range + 1) / Stride;
            OutputWidth = (int)width;
            OutputHeight = (int)height;
            if (OutputWidth != width || OutputHeight != height)
                throw new NNException(string.Format("Invalid ConvLayer dimensions.\nRange: {0}\nStride: {1}\nInput: {2}x{3}\nOutput: {4}x{5}", Range, Stride, InputHeight, InputWidth, height, width));

            OutputDepth = InputDepth * NFilters;
            OutputDimension = OutputDepth * OutputHeight * OutputWidth;
            InputDimension = InputDepth * InputHeight * InputWidth;
        }

        private double ApplyFilter(Vector<double> input, int depth, int f, int i, int j)
        {
            double res = 0;
            int depthOffset = depth * OutputHeight * OutputWidth;
            Matrix<double> weight = Weights[f];
            for (var m = 0; m < weight.RowCount; m++)
            {
                for (var n = 0; n < weight.ColumnCount; n++)
                {
                    // (i + m) = the row of the 2d input
                    // j + n = the column of the 2d input
                    res += weight[m, n] * input[depthOffset + (i + m) * InputWidth + j + n];
                }
            }

            return res;
        }

        internal override double WeightMagnitude()
        {
            double mag = 0;
            for(var i = 0; i < Weights.Length; i++)
            {
                mag += Weights[i].L1Norm() / (Weights[i].RowCount * Weights[i].ColumnCount);
            }
            return mag / Weights.Length;
        }
        #endregion
    }
}
