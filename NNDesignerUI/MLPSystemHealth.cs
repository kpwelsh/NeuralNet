using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NeuralNetModel;

namespace NNDesignerUI
{
    public partial class MLPSystemHealth : Form, SystemHealthMonitor
    {
        public MLPSystemHealth()
        {
            InitializeComponent();
            WeightChart.Legends.Add(new Legend("Legend"));
            Series cost = new Series("Cost");
            cost.ChartType = SeriesChartType.Spline;
            CostChart.Series.Add(cost);
            ChartArea ca = new ChartArea();
            ca.AxisX.Title = "batch";
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = false;
            CostChart.ChartAreas[0] = ca;


            Series test = new Series("Test");
            test.ChartType = SeriesChartType.Spline;
            TestChart.Series.Add(test);

            ca = new ChartArea();
            ca.AxisX.Title = "batch";
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = false;
            TestChart.ChartAreas[0] = ca;

            ca = new ChartArea();
            ca.AxisX.Title = "batch";
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = false;
            ca.AxisX.ScaleBreakStyle.BreakLineStyle = BreakLineStyle.None;
            WeightChart.ChartAreas[0] = ca;
        }

        

        public void CostUpdate(int x, double c)
        {
            CostUpdate(x, c);
        }

        private void CostUpdate(int dx, params double[] c)
        {
            if (CostChart.InvokeRequired)
            {
                CostChart.Invoke((MenuModel.UpdateCallback)CostUpdate, dx, c);
            }
            else
            {
                double x = dx;
                if (CostChart.Series[0].Points.Count > 0)
                    x += CostChart.Series[0].Points.Last().XValue;
                CostChart.Series[0].Points.AddXY(
                    x,
                    c[0]);
            }
        }

        public void TestUpdate(int x, double t)
        {
            TestUpdate(x, t);
        }

        public void TestUpdate(int dx, params double[] t)
        {
            if (TestChart.InvokeRequired)
            {
                TestChart.Invoke((MenuModel.UpdateCallback)TestUpdate, dx, t);
            }
            else
            {
                double x = dx;
                if (TestChart.Series[0].Points.Count > 0)
                    x += TestChart.Series[0].Points.Last().XValue;
                TestChart.Series[0].Points.AddXY(
                    x,
                    t[0]);
            }
        }

        public void WeightUpdate(int dx, double[] w)
        {
            if (WeightChart.InvokeRequired)
            {
                WeightChart.Invoke((MenuModel.UpdateCallback)WeightUpdate, dx, w);
            }
            else
            {
                for (var i = 0; i < w.Length; i++)
                {
                    if (i >= WeightChart.Series.Count)
                    {
                        Series s = new Series(i.ToString());
                        s.Legend = "Legend";
                        s.ChartType = SeriesChartType.Spline;
                        WeightChart.Series.Add(s);
                    }

                    double x = dx;
                    if (WeightChart.Series[i].Points.Count > 0)
                        x += WeightChart.Series[i].Points.Last().XValue;
                    WeightChart.Series[i].Points.AddXY(
                        x,
                        w[i]);
                }
            }
        }

        public void AttachToModel()
        {
            MenuModel.AddCostMonitor(CostUpdate);
            MenuModel.AddTestMonitor(TestUpdate);
            MenuModel.AddWeightMonitor(WeightUpdate);
        }

        public void DeAttach()
        {
            MenuModel.RemoveHooks();
        }

        private void MLPSystemHealth_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeAttach();
        }
    }
}
