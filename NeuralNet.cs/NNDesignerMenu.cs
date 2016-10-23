using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    class NNDesignerMenu : AMenu
    {
        public NNDesignerMenu()
        {
            Options.Add(
                new Tuple<int, string, CallBack>(
                    1,
                    "Build a fully connected network",
                    () => Text.ShowMenu(new NNBuilderMenu())
                ));
        }

        public override string Header()
        {
            return "Which kind of neural network would you like to build?";
        }
    }
}
