using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    class LayerBuilderMenu : AMenu
    {

        public LayerBuilderMenu()
        {
            MenuController.MakeNewLayer();
            Options.Clear();
            Options.Add(
                new Tuple<int, string, CallBack>(
                    0,
                    "Back",
                    () => GoBack(2)
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    1,
                    "Change input dimension",
                    () => ChangeInput()
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    2,
                    "Change output dimension",
                    () => ChangeOutput()
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    3,
                    "Change activation function",
                    () => Text.ShowMenu(new SelectActivationFunction())
                ));
        }

        public override string Header()
        {
            return "Here is the current layer\n" + MenuController.DisplayNetSummary();
        }

        private void ChangeInput()
        {
            int? dim = Text.GetInt("Enter input dimension: ");
            MenuController.SetLayerParam(inputDim : dim);
        }
        private void ChangeOutput()
        {
            int? dim = Text.GetInt("Enter output dimension: ");
            MenuController.SetLayerParam(outputDim : dim);
        }
        private void ChangeActivationFunction()
        {
            ActivationFunction? actFunc = Text.GetActivationFunction("Enter output dimension: ");
            MenuController.SetLayerParam(actFunc : actFunc);
        }
    }
}
