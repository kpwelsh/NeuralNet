using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetModel
{
    interface IMenu
    {
        bool ValidateChoice(int c);
        int? FindStringChoice(string choice);
        string Prompt();
        void DoChoice(int n);
        string Header();
    }
}
