using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    static class MenuController
    {
        static Net CurrentNet;
        static Layer CurrentLayer;

        public static void InitializeNN()
        {
            if(CurrentNet == null)
                CurrentNet = new Net();
        }

        public static void SetNetParam(double? learningRate = null, CostFunction? costFunc = null)
        {
            CurrentNet.SetParameters(learningRate, costFunc);
        }

        public static void SetLayerParam(int? inputDim = null, int? outputDim = null, ActivationFunction? actFunc = null, bool? norm = null)
        {
            CurrentLayer.SetParameters(inputDim, outputDim, actFunc, norm);
        }

        public static void MakeNewLayer()
        {
            CurrentLayer = new Layer();
        }

        public static void InsertLayer(int pos = -1)
        {
            CurrentNet.Add(CurrentLayer, pos);
        }

        public static string AvailableLayerPositions()
        {
            return "(0-" + CurrentNet.Count + ")";
        }

        public static string DisplayNetSummary()
        {
            return CurrentNet.ToString();
        }

        public static string DisplayLayerSummary()
        {
            return CurrentLayer.ToString();
        }
    }
}
