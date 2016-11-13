using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralNetModel;

namespace NNDesignerUI
{
    public partial class NetDesigner : Form
    {
        private Stack<PageCallback> PageStack;
        private delegate void Callback(bool success);
        private class PageCallback
        {
            public TabPage Page;
            public Callback Callback;
            public PageCallback(TabPage p, Callback c = null)
            {
                Page = p;
                Callback = c;
            }
        }

        private SystemHealthMonitor monitor;

        public NetDesigner()
        {
            InitializeComponent();
            PageStack = new Stack<PageCallback>();
        }

        #region Utility
        private void ClearPreviewPanel()
        {
            PPreview.Controls.Clear();
            PPreviewEdit.Controls.Clear();
        }

        private void SwitchToPage(TabPage next, Callback onReturn = null)
        {
            ClearPreviewPanel();
            PageStack.Push(new PageCallback(hiddenTabControl1.SelectedTab, onReturn));
            hiddenTabControl1.SelectedTab = next;
        }

        private void Back(bool success)
        {
            ClearPreviewPanel();
            PageCallback prev = PageStack.Pop();

            hiddenTabControl1.SelectedTab = prev.Page;
            prev.Callback?.Invoke(success);
        }

        private void ShowLayerSummary(int index)
        {
            ALayer layer = MenuModel.CurrentNet[index];

            // Set up the edit button.
            Button edit = new Button();
            edit.Text = "Edit";
            PPreviewEdit.Controls.Add(edit);
            edit.MouseClick +=
                (object s, MouseEventArgs e) =>
            {
                MenuModel.SetLayer(layer);
                SwitchToPage(LayerEditor);
            };
            edit.Dock = DockStyle.Fill;
            if (layer is Layer)
            {
                Layer locL = layer as Layer;

                PPreview.Controls.Add(new Label() { Text = $"Input Dimension: {locL.InputDimension}", AutoSize = true}, 0, 0);
                PPreview.Controls.Add(new Label() { Text = $"Output Dimension: {locL.OutputDimension}", AutoSize = true }, 1, 0);
                PPreview.Controls.Add(new Label() { Text = $"Activation Function: {locL.ActivationFunction.ToString()}", AutoSize = true }, 0, 1);
                PPreview.Controls.Add(new Label() { Text = $"Regularization Mode: {locL.RegMode.ToString()}", AutoSize = true }, 1, 1);
            }

            BRemoveLayer.Enabled = true;
        }

        private void InitMonitorWindow()
        {
            if(monitor == null || monitor.IsDisposed())
            {
                MLPSystemHealth m = new MLPSystemHealth();
                m.Show();
                monitor = m;
            }
            if(!(monitor == null || monitor.IsDisposed()))
                monitor.AttachToModel();

        }
        #endregion

        #region Main Menu
        private void BNewNet_Click(object sender, EventArgs e)
        {
            SwitchToPage(PPickANet);
        }

        private void BTrainNet_Click(object sender, EventArgs e)
        {
            SwitchToPage(PTrainNet);
        }

        private void BEditNet_Click(object sender, EventArgs e)
        {
            if (MenuModel.CurrentNet is MLP)
                SwitchToPage(EditMLP);
        }
        #endregion

        #region Net Type Selection
        private void BBuildMLP_Click(object sender, EventArgs e)
        {
            MenuModel.NewMLP();
            SwitchToPage(
                EditMLP,
                (bool s) =>
                {
                    if (s)
                        Back(s);
                }
                );
            NAddLayerPos.Value = MenuModel.NumberOfLayers();
        }
        #endregion

        #region Edit MLP
        private void EditMLP_Enter(object sender, EventArgs e)
        {
            DCostFunctionMLP.DataSource = Enum.GetValues(typeof(CostFunction));
            // Set default options up.
            MLP net = MenuModel.CurrentNet as MLP;
            if (net == null)
                throw new MenuException("Cannot edit network that is not an MLP with this screen");

            TMLPLearningRate.Text = net.LearningRate.ToString();
            DCostFunctionMLP.SelectedItem = net.CostFunction.ToString();

            BuildLayerSummary();
        }

        private void BuildLayerSummary()
        {
            NAddLayerPos.Value = MenuModel.CurrentNet.Count;
            PNetSummary.Controls.Clear();

            List<ALayer> layers = MenuModel.GetLayers();
            for (var i = 0; i < layers.Count; i++)
            {
                Button b = new Button();
                b.Margin = new Padding(0);
                b.Height = PNetSummary.Height;


                int index = i;
                b.MouseClick +=
                    (object s, MouseEventArgs ev) =>
                    {
                        ClearPreviewPanel();
                        ShowLayerSummary(index);
                    };

                ALayer l = layers[i];
                string buttonText = "";
                if (l is Layer)
                {
                    Layer locL = l as Layer;
                    buttonText += $"{locL.InputDimension}\n" + new string('-', 5) + $"\n{locL.OutputDimension}";
                }

                b.Text = buttonText;
                b.Parent = PNetSummary;
            }
        }

        private void BAddLayer_Click(object sender, EventArgs e)
        {
            ClearPreviewPanel();
            MenuModel.SetLayer();
            int index = (int)NAddLayerPos.Value;
            if (index > 0)
                NLayerInputDim.Value = MenuModel.CurrentNet[index - 1].OutputDimension;
            if (index < MenuModel.CurrentNet.Count)
                NLayerOutputDim.Value = MenuModel.CurrentNet[index].InputDimension;

            int layerPos = (int)NAddLayerPos.Value;
            SwitchToPage(
                LayerEditor,
                (bool s) =>
                {
                    if (s)
                    {
                        MenuModel.InsertLayer(layerPos);
                        BuildLayerSummary();
                    }
                }
                );
        }

        private void NAddLayerPos_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)NAddLayerPos.Value;
            NAddLayerPos.Value = Math.Min(Math.Max(val, 0), MenuModel.NumberOfLayers());
        }

        private void TMLPLearningRate_Validated(object sender, EventArgs e)
        {
            double res;
            if (!double.TryParse(TMLPLearningRate.Text, out res))
                TMLPLearningRate.Text = "";
        }

        private void BEditMLPDone_Click(object sender, EventArgs e)
        {
            MenuModel.SetNetParam(
                learningRate: double.Parse(TMLPLearningRate.Text), 
                costFunc: (CostFunction)DCostFunctionMLP.SelectedItem
                );
            Back(true);
        }

        private void BRemoveLayer_Click(object sender, EventArgs e)
        {
            int pos = MenuModel.RemoveLayer();
            PNetSummary.Controls.RemoveAt(pos);
            ClearPreviewPanel();
            BRemoveLayer.Enabled = false;
            NAddLayerPos.Value = MenuModel.CurrentNet.Count;
        }
        #endregion
        
        #region Train Net
        private void LoadTestingSet(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
            string name = OpenFileDialog.FileName.Split('\\').Last();
            MenuModel.LoadTestSet(name, OpenFileDialog.FileName);
            LBLoadedTest.Items.Add(name);
        }

        private void LoadTrainingSet(object sender, EventArgs e)
        {
            OpenFileDialog.ShowDialog();
            string name = OpenFileDialog.FileName.Split('\\').Last();
            MenuModel.LoadTrainingSet(name, OpenFileDialog.FileName);
            LBLoadedTrain.Items.Add(name);
        }

        async private void Learn(object sender, EventArgs e)
        {
            try
            {
                BPause.Enabled = true;
                BStartLearn.Enabled = false;
                BTweakNet.Enabled = false;
                InitMonitorWindow();
                // Multi-threading has special considerations. #LateBinding
                string trainName = (string)LBLoadedTrain.SelectedItem;
                string testName = (string)LBLoadedTest.SelectedItem;
                int nEpochs = SNEpochs.Value;
                int batchSize = SBatchSize.Value;
                MenuModel.SelectTest(testName);
                MenuModel.SelectTrain(trainName);
                await Task.Run(
                    () =>
                    MenuModel.TrainNet(nEpochs, batchSize)
                    );
                BStartLearn.Enabled = true;
                BPause.Enabled = false;
                BTweakNet.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EpochPP(params double[] vals)
        {
            MessageBox.Show($"Heres the error: {vals[0]}");
        }

        private void ChangedTestSelection(object sender, EventArgs e)
        {
            BStartLearn.Enabled = LBLoadedTest.SelectedItem != null && LBLoadedTrain.SelectedItem != null;
        }

        private void ChangedTrainSelection(object sender, EventArgs e)
        {
            BStartLearn.Enabled = LBLoadedTest.SelectedItem != null && LBLoadedTrain.SelectedItem != null;
        }

        private void Back_TrainingPage(object sender, EventArgs e)
        {
            Back(false);
        }

        private void Pause(object sender, EventArgs e)
        {
            if (!BStartLearn.Enabled)
            {
                MenuModel.CurrentNet.Abort = true;
            }
        }

        private void TweakNet(object sender, EventArgs e)
        {
            monitor?.DeAttach();
            MenuModel.CacheNet();
            SwitchToPage(EditMLP, TweakOnReturn);
        }
        private void TweakOnReturn(bool success)
        {
            if (!success)
                MenuModel.RestoreFromCache();
            else
            {
                MenuModel.ReInitialize();
            }
        }
        #endregion

        #region Layer Editor
        private void LayerEditor_Enter(object sender, EventArgs e)
        {
            DActivationFunction.DataSource = Enum.GetValues(typeof(ActivationFunction));
            CBRegularizationMode.DataSource = Enum.GetValues(typeof(RegularizationMode));
        }

        private void NLayerOutputDim_ValueChanged(object sender, EventArgs e)
        {
            MenuModel.SetLayerParam(outputDim: (int)NLayerOutputDim.Value);
        }

        private void NLayerInputDim_ValueChanged(object sender, EventArgs e)
        {
            MenuModel.SetLayerParam(outputDim: (int)NLayerInputDim.Value);
        }

        private void CBRegularizationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            MenuModel.SetLayerParam(regMode: (RegularizationMode)CBRegularizationMode.SelectedItem);
        }

        private void CBRegularizationMode_Validated(object sender, EventArgs e)
        {
        }

        private void DActivationFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            MenuModel.SetLayerParam(actFunc: (ActivationFunction)DActivationFunction.SelectedItem);
        }

        private void DActivationFunction_Validation(object sender, CancelEventArgs e)
        {
        }

        private void BDoneLayerEdit_Click(object sender, EventArgs e)
        {
            MenuModel.SetLayerParam((int)NLayerInputDim.Value,
                (int)NLayerOutputDim.Value,
                (ActivationFunction)DActivationFunction.SelectedItem,
                (RegularizationMode)CBRegularizationMode.SelectedItem);
            Back(true);
        }
        #endregion

    }
}
