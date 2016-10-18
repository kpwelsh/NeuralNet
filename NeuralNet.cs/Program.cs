using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;
using System.IO;

namespace NeuralNet.cs
{
    class Program
    {
        static void LoadMnist(out HashSet<TrainingData> data, string fp, int nDataPoints)
        {
            data = new HashSet<TrainingData>();
            using (StreamReader trainingIn = new StreamReader(fp))
            {
                int n = 0;
                while (!trainingIn.EndOfStream && n < nDataPoints)
                {
                    string[] line = trainingIn.ReadLine().Split(',');
                    TrainingData td = new TrainingData(784, 10);
                    td.IntLabel = int.Parse(line[0]);
                    for (var i = 1; i <= 784; i++)
                        td.Data[i - 1] = double.Parse(line[i]) / 255;
                    data.Add(td);
                    n++;
                }
            }
        }

        static void LoadMultiplication(out HashSet<TrainingData> data, int digits, int n)
        {
            data = new HashSet<TrainingData>();
            DiscreteUniform rand = new DiscreteUniform(1, (int)Math.Pow(10, digits) - 1);
            for(var i = 0; i < n; i++)
            {
                double x = rand.Sample();
                double y = rand.Sample();
                double z = x * y;
                TrainingData td = new TrainingData(2, 1);
                double scale = Math.Pow(10, digits);
                td.Data[0] = x / scale;
                td.Data[1] = y / scale;
                td.SetLabelVector(DenseVector.Create(1, z / Math.Pow(10, 2 * digits)));

                data.Add(td);
            }
        }

        static Vector<double> EncodeNumber(int n,int nDigits)
        {
            Vector<double> ret = DenseVector.Create((int)Math.Pow(10, nDigits),1);
            ret[n] = 1;
            return ret;
        }

        static void Main(string[] args)
        {
            HashSet<TrainingData> td;
            HashSet<TrainingData> test;
            int nDigits = 2;
            LoadMultiplication(out td, nDigits, 10000);
            LoadMultiplication(out test, nDigits, 10000);

            //ConvLayer l = new ConvLayer(5, 1, 5, 28, 28, 1);
            //net.Add(l);
            //l = new ConvLayer(2, 2, 1, l.OutputHeight, l.OutputWidth, l.OutputDepth);
            //net.Add(l);

            //LoadMnist(out td, "mnist_train.csv", 6000);
            //LoadMnist(out test, "mnist_test.csv", 1000);
            Net net = new Net(Net.CostFunction.MeanSquare, 10);
            net.Add(new Layer(2, 50, Layer.ActivationFunction.Sigmoid));
            net.Add(new Layer(50, 1, Layer.ActivationFunction.Sigmoid));


            int epochs = 1;
            do
            {
                for (var i = 0; i < epochs; i++)
                {
                    List<double> costs = net.Learn(td, 1);
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine(string.Format("Final Cost: {0}",costs[costs.Count - 1]));
                    //Console.WriteLine(string.Format("Correctness on test set: {0}",net.TestClassification(test)));
                    Console.WriteLine("Epoch Complete");
                    Console.WriteLine("---------------------------------------");
                }
                Console.Write("\nEnter the number of training epochs: ");
            }while (int.TryParse(Console.ReadLine(),out epochs));
            
            double diff = 0;
            foreach(TrainingData t in test)
            {
                diff += Math.Abs(net.Process(t.Data)[0] - t.GetLabelVector()[0]) / t.GetLabelVector()[0];
            }

            TrainingData d = test.ElementAt(0);

            Console.WriteLine(string.Format("Average Error: {0}%", 100 * diff / test.Count));
            Console.WriteLine(string.Format("Guess: {0}\n Actual: {1}", net.Process(d.Data)[0], d.GetLabelVector()[0]));
        }
    }
}
