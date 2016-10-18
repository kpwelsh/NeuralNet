using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNet
{
    class TrainingData
    {
        public Vector<double> Data;
        private Vector<double> vectorLabel;
        private int intLabel;
        private bool dirtyLabel;

        public int IntLabel
        {
            get
            {
                if (dirtyLabel)
                {
                    intLabel = vectorLabel.AbsoluteMaximumIndex();
                    dirtyLabel = false;
                }
                return intLabel;
            }
            set
            {
                vectorLabel[intLabel] = 0;
                intLabel = value;
                vectorLabel[intLabel] = 1;
            }

        }

        #region Constructors
        public TrainingData(int dataLength, int labelLength)
        {
            Data = new DenseVector(dataLength);
            vectorLabel = new DenseVector(labelLength);
            dirtyLabel = true;
        }

        public TrainingData(Vector<double> data, Vector<double> label)
        {
            Data = data;
            vectorLabel = label;
            dirtyLabel = true;
        }

        public TrainingData(Vector<double> data, int labelLength, int label)
        {
            intLabel = label;
            Data = data;
            vectorLabel = new DenseVector(labelLength);
            vectorLabel[label] = 1;
            dirtyLabel = false;
        }
        #endregion
        public void SetLabelVector(Vector<double> label)
        {
            vectorLabel = label;
            dirtyLabel = true;
        }

        public Vector<double> GetLabelVector()
        {
            return vectorLabel;
        }
    }
}
