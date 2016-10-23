using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    class LayerDesigner : AMenu
    {
        public LayerDesigner()
        {
            Options.Add(
                new Tuple<int, string, CallBack>(
                    1,
                    "Build a fully connected layer",
                    () => Text.ShowMenu(new LayerBuilderMenu())
                ));
        }

        public override string Header()
        {
            return "Which kind of layer would you like to build?";
        }
    }
}
