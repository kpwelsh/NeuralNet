using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Distributions;
using System.IO;
using System.Diagnostics;

namespace NeuralNetModel
{
    class Program
    {
        static List<double> Costs = new List<double>();
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
                td.Response = DenseVector.Create(1, z / Math.Pow(10, 2 * digits));

                data.Add(td);
            }
        }

        static Vector<double> EncodeNumber(int n,int nDigits)
        {
            Vector<double> ret = DenseVector.Create((int)Math.Pow(10, nDigits),1);
            ret[n] = 1;
            return ret;
        }

        static void LoadSin(out HashSet<TrainingData> data, int n)
        {
            data = new HashSet<TrainingData>();
            ContinuousUniform rand = new ContinuousUniform(0,Math.PI);
            for (var i = 0; i < n; i++)
            {
                double x = rand.Sample();
                TrainingData td = new TrainingData(1, 1);
                double scale = Math.PI;
                td.Data[0] = x / scale;
                td.Response = DenseVector.Create(1, Math.Sin(x));

                data.Add(td);
            }
        }

        static void LoadSinSeq(out HashSet<TrainingData> data, double dt, int seqLength, int nData)
        {
            data = new HashSet<TrainingData>();
            ContinuousUniform rand = new ContinuousUniform(0, 2 * Math.PI);
            for(var i = 0; i < nData; i++)
            {
                double theta = rand.Sample();
                TrainingData td = new TrainingData(0,1);
                for(var j = 0; j < seqLength; j++)
                {
                    TrainingData.TrainingPair pair = new TrainingData.TrainingPair();
                    theta += dt;
                    pair.Response = DenseVector.Create(1, Math.Sin(theta));
                    td[j] = pair;
                }
                data.Add(td);
            }
        }

        static void LoadXOR(out HashSet<TrainingData> data, int length, int nData)
        {
            data = new HashSet<TrainingData>();
            ContinuousUniform rand = new ContinuousUniform(0, 1);
            for(var i = 0; i < nData; i++)
            {
                TrainingData td = new TrainingData(1, 1);
                td[0] = new TrainingData.TrainingPair(DenseVector.Create(1, rand.Sample() > 0.5 ? 1 : 0), DenseVector.Create(1, 0.5));
                for(var j = 1; j < length; j++)
                {
                    TrainingData.TrainingPair p = new TrainingData.TrainingPair();
                    p.Data = DenseVector.Create(1, rand.Sample() > 0.5 ? 1 : 0);
                    if (td[j - 1].Data[0] == p.Data[0])
                        p.Response = DenseVector.Create(1, 0);
                    else
                        p.Response = DenseVector.Create(1, 1);
                    td[j] = p;
                }
                data.Add(td);
            }
        }

        static void Main(string[] args)
        {
            TrainAutoEncoder();
        }

        static void AddToCosts(int b, ANet net)
        {
            Costs.Add(net.LastCost);
        }

        static void TrainAutoEncoder()
        {

            MenuModel.LoadTrainingSet("newMnist_test", "newMnist_test.csv");
            MenuModel.LoadTestSet("newMnist_test", "newMnist_test.csv");

            AutoEncoder net = new AutoEncoder(activationGoal: 0.2 ,sparsityWeight: 30);
            net.SetParameters(learningRate: 1, costFunc: CostFunction.MeanSquare);
            net.Add(new Layer(784, 100, ActivationFunction.Sigmoid));

            MenuModel.CurrentNet = net;

            MenuModel.SelectTest("newMnist_test");
            MenuModel.SelectTrain("newMnist_test");
            MenuModel.TestSampleFreq = 10;
            MenuModel.AddTestMonitor(PrintToScreen,10101010);
            
            Console.WriteLine("Done Loading");
            MenuModel.TrainNet(10, 100);
            using(StreamWriter fout = new StreamWriter("autoEncoded.txt"))
            {
                Matrix<double> w = (net[0] as Layer).Weights;

                for (var i = 0; i < w.ColumnCount; i++)
                {
                    StringBuilder line = new StringBuilder();
                    for(var j = 0; j < w.RowCount; j++)
                    {
                        line.Append(w[j, i] + " ");   
                    }
                    line.Remove(line.Length - 1,1);
                    fout.WriteLine(line.ToString());
                }
            }
            using (StreamWriter fout = new StreamWriter("replication.txt"))
            {
                TrainingData td = MenuModel.SelectedTest.First();
                Vector<double> output = net.Process(td.Data);
                
                StringBuilder line = new StringBuilder();
                for (var j = 0; j < output.Count; j++)
                {
                    line.Append(output[j] + " ");
                }
                line.Remove(line.Length - 1, 1);
                fout.WriteLine(line.ToString());

                line = new StringBuilder();
                for (var j = 0; j < td.Data.Count; j++)
                {
                    line.Append(td.Data[j] + " ");
                }
                line.Remove(line.Length - 1, 1);
                fout.WriteLine(line.ToString());
            }
        }

        static void TrainRNN()
        {
            HashSet<TrainingData> training;
            //HashSet<TrainingData> test;
            LoadXOR(out training, 10000, 10);
            //LoadSinSeq(out test, 0.25, 300, 1000);
            int size = 4;

            FullyConnectedRNN net = new FullyConnectedRNN(0.01, 20);
            net.SetParameters(0.0001, CostFunction.MeanSquare, true);
            net.Add(new Layer(size + 1, size, ActivationFunction.Sigmoid, RegularizationMode.L2));
            net.Add(new Layer(size + 1, 1, ActivationFunction.Identity, RegularizationMode.L2));

            net.Hook += AddToCosts;
            int epochs = 1;
            do
            {
                for (var i = 0; i < epochs; i++)
                {
                    net.Learn(training, 10);
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"Last Cost: {Costs.Last()}");
                    Console.WriteLine(string.Format("Error on test set: {0}", net.Test(training)));
                    Console.WriteLine("Epoch Complete");
                    Console.WriteLine("---------------------------------------");
                }
                Console.Write("\nEnter the number of training epochs: ");
            } while (int.TryParse(Console.ReadLine(), out epochs));
        }

        async static void TrainFeedForward()
        {

            MenuModel.LoadTrainingSet("xor_train", "xor_train.csv");
            MenuModel.LoadTestSet("xor_test", "xor_test.csv");

            MLP net = new MLP();
            net.SetParameters(learningRate: 1, costFunc: CostFunction.MeanSquare);
            net.Add(new Layer(2, 30, ActivationFunction.Sigmoid));
            net.Add(new Layer(30, 2, ActivationFunction.Sigmoid));

            MenuModel.CurrentNet = net;

            MenuModel.SelectTest("xor_test");
            MenuModel.SelectTrain("xor_train");

            Stopwatch sw = new Stopwatch();
            double t;
            Console.WriteLine("Done Loading");
            sw.Start();
            while (Console.ReadLine().Equals("y"))
            {
                t = sw.ElapsedMilliseconds;
                await Task.Run(() => MenuModel.TrainNet(1, 100));
                Console.WriteLine($"Time Difference: {sw.ElapsedMilliseconds - t}");
            }
        }

        private static void PrintToScreen(int x, params double[] vals)
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(string.Format("Error on test set: {0}", vals[0]));
            Console.WriteLine("---------------------------------------");
        }

        /// <summary>
        /// Puts approximately percent of the input values into out1 and 1-percent into out2
        /// </summary>
        /// <param name="input"></param>
        /// <param name="out1"></param>
        /// <param name="out2"></param>
        /// <param name="percent"></param>
        private static void SplitSet(HashSet<TrainingData> input, out HashSet<TrainingData> out1, out HashSet<TrainingData> out2, double percent)
        {
            ContinuousUniform rand = new ContinuousUniform(0, 1);
            out1 = new HashSet<TrainingData>();
            out2 = new HashSet<TrainingData>();
            foreach(TrainingData td in input)
            {
                if (rand.Sample() < percent)
                    out1.Add(td);
                else
                    out2.Add(td);
            }
        }
    }
}
