using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    class MainMenu : AMenu
    {
        public MainMenu()
        {
            Options.Clear();
            Options.Add(
                new Tuple<int, string, CallBack>(
                    1,
                    "Design a new neural network",
                    () => Text.ShowMenu(new NNBuilderMenu())
                ));
            Options.Add(
                new Tuple<int, string, CallBack>(
                    2,
                    "Load an existing network",
                    () => Text.ShowMenu(new NNLoadMenu())
                ));
        }

        public override string Header()
        {
            return "Welcome to the neural network designer!.\nWhat would you like to do?";
        }
    }
}
