using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    abstract class AMenu : IMenu
    {
        public delegate void CallBack();
        protected List<Tuple<int, string, CallBack>> Options;
        
        public AMenu()
        {
            Options = new List<Tuple<int, string, CallBack>>();
            Options.Add(
                new Tuple<int, string, CallBack>(
                    0,
                    "Back",
                    () => GoBack()
                ));
        }

        public bool ValidateChoice(int c)
        {
            bool has = false;
            foreach(Tuple<int,string,CallBack> t in Options)
            {
                if(t.Item1 == c)
                {
                    has = true;
                    break;
                }
            }
            return has;
        }

        public int? FindStringChoice(string choice)
        {
            List<Tuple<int, string, CallBack>> matches = new List<Tuple<int, string, CallBack>>();
            choice = choice.ToUpper();
            foreach(Tuple<int, string, CallBack> t in Options)
            {
                string op = t.Item2.ToUpper();
                if (op.StartsWith(choice))
                    matches.Add(t);
            }

            if (matches.Count > 1)
                return null;
            else if (matches.Count == 1)
                return matches[0].Item1;
            return -1;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            foreach (Tuple<int, string, CallBack> t in Options)
            {
                res.Append(string.Format("\n\t{0}: {1}",t.Item1,t.Item2));
            }

            return res.ToString();
        }

        public void DoChoice(int n)
        {
            foreach (Tuple<int, string, CallBack> t in Options)
            {
                if (t.Item1 == n)
                {
                    t.Item3();
                    break;
                }
            }
        }

        public virtual string Header()
        {
            return "This page is under construction:";
        }
        public virtual string Prompt()
        {
            return "Selection: ";
        }

        protected void GoBack(int n = 1)
        {
            Text.BackUp += n;
        }
    }
}
