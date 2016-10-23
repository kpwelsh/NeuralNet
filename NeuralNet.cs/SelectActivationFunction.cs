using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    class SelectActivationFunction : AMenu
    {
        public SelectActivationFunction()
        {
            Options.Add(
                new Tuple<int, string, CallBack>(
                    1,
                    "Sigmoid",
                    () =>
                    {
                        MenuController.SetLayerParam(actFunc: ActivationFunction.Sigmoid);
                        GoBack();
                    }
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    2,
                    "ReLU",
                    () =>
                    {
                        MenuController.SetLayerParam(actFunc: ActivationFunction.ReLU);
                        GoBack();
                    }
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    3,
                    "SoftPlus",
                    () =>
                    {
                        MenuController.SetLayerParam(actFunc: ActivationFunction.SoftPlus);
                        GoBack();
                    }
                ));
        }

        public override string Header()
        {
            return "Available activation functions: ";
        }
    }
}
