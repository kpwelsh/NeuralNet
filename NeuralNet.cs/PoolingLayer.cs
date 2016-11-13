using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetModel
{
    [Serializable]
    public enum PoolingMode { Max, Mean}
    [Serializable]
    class PoolingLayer : ALayer
    {
        public int InputWidth
        {
            get;
            private set;
        }
        public int InputHeight
        {
            get;
            private set;
        }
        public int OutputWidth
        {
            get;
            private set;
        }
        public int OutputHeight
        {
            get;
            private set;
        }
        public int Depth
        {
            get;
            private set;
        }
        public PoolingMode Mode
        {
            get;
            private set;
        }

        private int Range;

        internal PoolingLayer(int range, int inputWidth, int inputHeight, int inputDepth, PoolingMode mode = PoolingMode.Max)
        {
            Range = range;
            InputWidth = inputWidth;
            InputHeight = inputHeight;
            Depth = inputDepth;
            Mode = mode;
        }

        internal override void ReInitialize()
        {
            // There is nothing actually learned in the pooling layer.
        }

        internal override void ApplyUpdate()
        {
            throw new NotImplementedException();
        }

        internal override Vector<double> Process(Vector<double> input)
        {
            throw new NotImplementedException();
        }

        internal override Vector<double> PropogateError(Vector<double> outputError, double errorWeight, Vector<double> inputCacheOverride = null)
        {
            throw new NotImplementedException();
        }

        internal override double WeightMagnitude()
        {
            throw new NotImplementedException();
        }
    }
}
