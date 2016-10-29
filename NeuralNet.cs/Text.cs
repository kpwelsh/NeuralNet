using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet.cs
{
    static class Text
    {
        public static int BackUp = 0;

        private static string ErrorText = "Invalid Input.";
        public static void ShowMenu(IMenu menu)
        {
            string input = "?";
            int? val;
            int intVal = 0;
            bool done = false;
            do
            {
                Console.Clear();
                Console.WriteLine(menu.Header());
                Console.WriteLine(menu.ToString());
                Console.Write(menu.Prompt());
                input = Console.ReadLine();
                if (int.TryParse(input, out intVal))
                    done = menu.ValidateChoice(intVal);
                else
                {
                    val = menu.FindStringChoice(input);
                    if (val == null || val == -1)
                        done = false;
                    else
                    {
                        done = true;
                        intVal = (int)val;
                    }
                }
                if (input.ToUpper().Equals(""))
                {
                    BackUp++;
                }
                if (done)
                    menu.DoChoice(intVal);
            } while (BackUp == 0);
            BackUp--;
        }
        
        public static double? GetDouble(string prompt)
        {
            Console.Clear();
            bool done = false;
            string input = "?";
            double val = 0;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (input.ToUpper().Equals("Q"))
                    break;
                done = double.TryParse(input, out val);
                if (!done)
                    Console.WriteLine(ErrorText);
            } while (!done);
            if (!done)
                return null;
            return val;
        }
        public static int? GetInt(string prompt)
        {
            Console.Clear();
            bool done = false;
            string input = "?";
            int val = 0;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (input.ToUpper().Equals("Q"))
                    break;
                done = int.TryParse(input, out val);
                if (!done)
                    Console.WriteLine(ErrorText);
            } while (!done);
            if (!done)
                return null;
            return val;
        }
        public static ActivationFunction? GetActivationFunction(string prompt)
        {
            Console.Clear();
            bool done = false;
            string input = "?";
            int intVal = 0;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (input.ToUpper().Equals("Q"))
                    break;
                if (int.TryParse(input, out intVal))
                    done = Enum.IsDefined(typeof(ActivationFunction), intVal);
                if (!done)
                    Console.WriteLine(ErrorText);
            } while (!done);
            if (!done)
                return null;
            return (ActivationFunction)intVal;
        }
    }
}
