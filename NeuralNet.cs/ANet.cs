using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetModel
{
    public abstract class ANet
    {
        #region Properties
        public ALayer this[int n]
        {
            get
            {
                return Layers[n];
            }
            protected set
            {
                Layers[n] = value;
            }
        }
        public int Count
        {
            get { return Layers.Count; }
        }
        #endregion

        #region Protected Fields
        protected List<ALayer> Layers;
        protected MenuController.ProgrammingPoint BatchLevelPP;
        protected ICostFunction CostFunc;
        protected double LearningRate;
        #endregion

        #region Public
        internal void AddBatchLevelPP(MenuController.ProgrammingPoint pp)
        {
            if (BatchLevelPP == null)
                BatchLevelPP = pp;
            else if (!BatchLevelPP.GetInvocationList().Contains(pp))
                BatchLevelPP += pp;
        }
        internal void Add(ALayer layer, int? pos = null)
        {
            if (pos == null)
                Layers.Add(layer);
            else
                Layers.Insert((int)pos, layer);
        }
        #endregion

        #region Overridable
        internal virtual void SetParameters(double? learningRate = null, CostFunction? costFunc = null)
        {
            if (costFunc != null)
            {
                switch (costFunc)
                {
                    case CostFunction.MeanSquare:
                        CostFunc = new MeanSquare();
                        break;
                    case CostFunction.CrossEntropy:
                        CostFunc = new CrossEntropy();
                        break;
                }
            }
            if (learningRate != null)
                LearningRate = (double)learningRate;
        }
        internal abstract void Learn(HashSet<TrainingData> trainingSet, int batchSize);
        internal abstract Vector<double> Process(Vector<double> input);
        /// <summary>
        /// This method tests the net against the training set.
        /// </summary>
        /// <param name="testSet">Set of training data for the neural net.</param>
        /// <returns>A measure of error on the test set</returns>
        internal abstract double Test(HashSet<TrainingData> testSet);
        #endregion
    }
}
