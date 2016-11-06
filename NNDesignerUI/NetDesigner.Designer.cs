namespace NNDesignerUI
{
    partial class NetDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.hiddenTabControl1 = new NNDesignerUI.HiddenTabControl();
            this.PMainMenu = new System.Windows.Forms.TabPage();
            this.BEditNet = new System.Windows.Forms.Button();
            this.BTrainNet = new System.Windows.Forms.Button();
            this.BNewNet = new System.Windows.Forms.Button();
            this.PPickANet = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BBuildMLP = new System.Windows.Forms.Button();
            this.EditMLP = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TMLPLearningRate = new System.Windows.Forms.TextBox();
            this.DCostFunctionMLP = new System.Windows.Forms.ComboBox();
            this.BEditMLPDone = new System.Windows.Forms.Button();
            this.PNetSummary = new System.Windows.Forms.FlowLayoutPanel();
            this.NAddLayerPos = new System.Windows.Forms.NumericUpDown();
            this.BAddLayer = new System.Windows.Forms.Button();
            this.LayerEditor = new System.Windows.Forms.TabPage();
            this.CBRegularizationMode = new System.Windows.Forms.ComboBox();
            this.BDoneLayerEdit = new System.Windows.Forms.Button();
            this.NLayerInputDim = new System.Windows.Forms.NumericUpDown();
            this.NLayerOutputDim = new System.Windows.Forms.NumericUpDown();
            this.DActivationFunction = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PTrainNet = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BBack_Train = new System.Windows.Forms.Button();
            this.BStartLearn = new System.Windows.Forms.Button();
            this.SBatchSize = new NNDesignerUI.Slider();
            this.SNEpochs = new NNDesignerUI.Slider();
            this.BLoadTrainSet = new System.Windows.Forms.Button();
            this.BLoadTestSet = new System.Windows.Forms.Button();
            this.LBLoadedTest = new System.Windows.Forms.ListBox();
            this.LBLoadedTrain = new System.Windows.Forms.ListBox();
            this.LMiniBatch = new System.Windows.Forms.Label();
            this.LEpochs = new System.Windows.Forms.Label();
            this.PPreview = new System.Windows.Forms.TableLayoutPanel();
            this.PPreviewEdit = new System.Windows.Forms.Panel();
            this.hiddenTabControl1.SuspendLayout();
            this.PMainMenu.SuspendLayout();
            this.PPickANet.SuspendLayout();
            this.EditMLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAddLayerPos)).BeginInit();
            this.LayerEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NLayerInputDim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NLayerOutputDim)).BeginInit();
            this.PTrainNet.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // hiddenTabControl1
            // 
            this.hiddenTabControl1.Controls.Add(this.PMainMenu);
            this.hiddenTabControl1.Controls.Add(this.PPickANet);
            this.hiddenTabControl1.Controls.Add(this.EditMLP);
            this.hiddenTabControl1.Controls.Add(this.LayerEditor);
            this.hiddenTabControl1.Controls.Add(this.PTrainNet);
            this.hiddenTabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hiddenTabControl1.Location = new System.Drawing.Point(0, 0);
            this.hiddenTabControl1.Name = "hiddenTabControl1";
            this.hiddenTabControl1.SelectedIndex = 0;
            this.hiddenTabControl1.Size = new System.Drawing.Size(558, 418);
            this.hiddenTabControl1.TabIndex = 2;
            // 
            // PMainMenu
            // 
            this.PMainMenu.Controls.Add(this.BEditNet);
            this.PMainMenu.Controls.Add(this.BTrainNet);
            this.PMainMenu.Controls.Add(this.BNewNet);
            this.PMainMenu.Location = new System.Drawing.Point(4, 22);
            this.PMainMenu.Name = "PMainMenu";
            this.PMainMenu.Padding = new System.Windows.Forms.Padding(3);
            this.PMainMenu.Size = new System.Drawing.Size(550, 392);
            this.PMainMenu.TabIndex = 0;
            this.PMainMenu.Text = "MainMenu";
            this.PMainMenu.UseVisualStyleBackColor = true;
            // 
            // BEditNet
            // 
            this.BEditNet.Location = new System.Drawing.Point(143, 6);
            this.BEditNet.Name = "BEditNet";
            this.BEditNet.Size = new System.Drawing.Size(116, 88);
            this.BEditNet.TabIndex = 2;
            this.BEditNet.Text = "Edit";
            this.BEditNet.UseVisualStyleBackColor = true;
            this.BEditNet.Click += new System.EventHandler(this.BEditNet_Click);
            // 
            // BTrainNet
            // 
            this.BTrainNet.Location = new System.Drawing.Point(21, 100);
            this.BTrainNet.Name = "BTrainNet";
            this.BTrainNet.Size = new System.Drawing.Size(116, 88);
            this.BTrainNet.TabIndex = 1;
            this.BTrainNet.Text = "Train";
            this.BTrainNet.UseVisualStyleBackColor = true;
            this.BTrainNet.Click += new System.EventHandler(this.BTrainNet_Click);
            // 
            // BNewNet
            // 
            this.BNewNet.Location = new System.Drawing.Point(21, 6);
            this.BNewNet.Name = "BNewNet";
            this.BNewNet.Size = new System.Drawing.Size(116, 88);
            this.BNewNet.TabIndex = 0;
            this.BNewNet.Text = "New";
            this.BNewNet.UseVisualStyleBackColor = true;
            this.BNewNet.Click += new System.EventHandler(this.BNewNet_Click);
            // 
            // PPickANet
            // 
            this.PPickANet.Controls.Add(this.panel1);
            this.PPickANet.Controls.Add(this.BBuildMLP);
            this.PPickANet.Location = new System.Drawing.Point(4, 22);
            this.PPickANet.Name = "PPickANet";
            this.PPickANet.Padding = new System.Windows.Forms.Padding(3);
            this.PPickANet.Size = new System.Drawing.Size(550, 392);
            this.PPickANet.TabIndex = 1;
            this.PPickANet.Text = "PickANet";
            this.PPickANet.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(-4, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 32);
            this.panel1.TabIndex = 1;
            // 
            // BBuildMLP
            // 
            this.BBuildMLP.Location = new System.Drawing.Point(8, 20);
            this.BBuildMLP.Name = "BBuildMLP";
            this.BBuildMLP.Size = new System.Drawing.Size(167, 62);
            this.BBuildMLP.TabIndex = 0;
            this.BBuildMLP.Text = "Build MLP";
            this.BBuildMLP.UseVisualStyleBackColor = true;
            this.BBuildMLP.Click += new System.EventHandler(this.BBuildMLP_Click);
            // 
            // EditMLP
            // 
            this.EditMLP.Controls.Add(this.label6);
            this.EditMLP.Controls.Add(this.label5);
            this.EditMLP.Controls.Add(this.TMLPLearningRate);
            this.EditMLP.Controls.Add(this.DCostFunctionMLP);
            this.EditMLP.Controls.Add(this.BEditMLPDone);
            this.EditMLP.Controls.Add(this.PNetSummary);
            this.EditMLP.Controls.Add(this.NAddLayerPos);
            this.EditMLP.Controls.Add(this.BAddLayer);
            this.EditMLP.Location = new System.Drawing.Point(4, 22);
            this.EditMLP.Name = "EditMLP";
            this.EditMLP.Padding = new System.Windows.Forms.Padding(3);
            this.EditMLP.Size = new System.Drawing.Size(550, 392);
            this.EditMLP.TabIndex = 2;
            this.EditMLP.Text = "EditMLP";
            this.EditMLP.UseVisualStyleBackColor = true;
            this.EditMLP.Enter += new System.EventHandler(this.EditMLP_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Learning Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Cost Function:";
            // 
            // TMLPLearningRate
            // 
            this.TMLPLearningRate.Location = new System.Drawing.Point(89, 149);
            this.TMLPLearningRate.Name = "TMLPLearningRate";
            this.TMLPLearningRate.Size = new System.Drawing.Size(46, 20);
            this.TMLPLearningRate.TabIndex = 2;
            this.TMLPLearningRate.Validated += new System.EventHandler(this.TMLPLearningRate_Validated);
            // 
            // DCostFunctionMLP
            // 
            this.DCostFunctionMLP.FormattingEnabled = true;
            this.DCostFunctionMLP.Items.AddRange(new object[] {
            "MeanSquare",
            "CrossEntropy"});
            this.DCostFunctionMLP.Location = new System.Drawing.Point(89, 175);
            this.DCostFunctionMLP.Name = "DCostFunctionMLP";
            this.DCostFunctionMLP.Size = new System.Drawing.Size(121, 21);
            this.DCostFunctionMLP.TabIndex = 3;
            // 
            // BEditMLPDone
            // 
            this.BEditMLPDone.Location = new System.Drawing.Point(434, 240);
            this.BEditMLPDone.Name = "BEditMLPDone";
            this.BEditMLPDone.Size = new System.Drawing.Size(108, 56);
            this.BEditMLPDone.TabIndex = 4;
            this.BEditMLPDone.Text = "Done";
            this.BEditMLPDone.UseVisualStyleBackColor = true;
            this.BEditMLPDone.Click += new System.EventHandler(this.BEditMLPDone_Click);
            // 
            // PNetSummary
            // 
            this.PNetSummary.Location = new System.Drawing.Point(8, 6);
            this.PNetSummary.Name = "PNetSummary";
            this.PNetSummary.Size = new System.Drawing.Size(534, 62);
            this.PNetSummary.TabIndex = 2;
            // 
            // NAddLayerPos
            // 
            this.NAddLayerPos.Location = new System.Drawing.Point(137, 76);
            this.NAddLayerPos.Name = "NAddLayerPos";
            this.NAddLayerPos.Size = new System.Drawing.Size(36, 20);
            this.NAddLayerPos.TabIndex = 1;
            this.NAddLayerPos.ValueChanged += new System.EventHandler(this.NAddLayerPos_ValueChanged);
            // 
            // BAddLayer
            // 
            this.BAddLayer.Location = new System.Drawing.Point(8, 74);
            this.BAddLayer.Name = "BAddLayer";
            this.BAddLayer.Size = new System.Drawing.Size(123, 25);
            this.BAddLayer.TabIndex = 0;
            this.BAddLayer.Text = "Add Layer at position";
            this.BAddLayer.UseVisualStyleBackColor = true;
            this.BAddLayer.Click += new System.EventHandler(this.BAddLayer_Click);
            // 
            // LayerEditor
            // 
            this.LayerEditor.Controls.Add(this.CBRegularizationMode);
            this.LayerEditor.Controls.Add(this.BDoneLayerEdit);
            this.LayerEditor.Controls.Add(this.NLayerInputDim);
            this.LayerEditor.Controls.Add(this.NLayerOutputDim);
            this.LayerEditor.Controls.Add(this.DActivationFunction);
            this.LayerEditor.Controls.Add(this.label4);
            this.LayerEditor.Controls.Add(this.label3);
            this.LayerEditor.Controls.Add(this.label2);
            this.LayerEditor.Controls.Add(this.label1);
            this.LayerEditor.Location = new System.Drawing.Point(4, 22);
            this.LayerEditor.Name = "LayerEditor";
            this.LayerEditor.Padding = new System.Windows.Forms.Padding(3);
            this.LayerEditor.Size = new System.Drawing.Size(550, 392);
            this.LayerEditor.TabIndex = 3;
            this.LayerEditor.Text = "LayerEditor";
            this.LayerEditor.UseVisualStyleBackColor = true;
            this.LayerEditor.Enter += new System.EventHandler(this.LayerEditor_Enter);
            // 
            // CBRegularizationMode
            // 
            this.CBRegularizationMode.FormattingEnabled = true;
            this.CBRegularizationMode.Items.AddRange(new object[] {
            "None",
            "L2"});
            this.CBRegularizationMode.Location = new System.Drawing.Point(115, 101);
            this.CBRegularizationMode.Name = "CBRegularizationMode";
            this.CBRegularizationMode.Size = new System.Drawing.Size(121, 21);
            this.CBRegularizationMode.TabIndex = 4;
            this.CBRegularizationMode.SelectedIndexChanged += new System.EventHandler(this.CBRegularizationMode_SelectedIndexChanged);
            this.CBRegularizationMode.Validated += new System.EventHandler(this.CBRegularizationMode_Validated);
            // 
            // BDoneLayerEdit
            // 
            this.BDoneLayerEdit.Location = new System.Drawing.Point(357, 136);
            this.BDoneLayerEdit.Name = "BDoneLayerEdit";
            this.BDoneLayerEdit.Size = new System.Drawing.Size(75, 23);
            this.BDoneLayerEdit.TabIndex = 5;
            this.BDoneLayerEdit.Text = "Done";
            this.BDoneLayerEdit.UseVisualStyleBackColor = true;
            this.BDoneLayerEdit.Click += new System.EventHandler(this.BDoneLayerEdit_Click);
            // 
            // NLayerInputDim
            // 
            this.NLayerInputDim.Location = new System.Drawing.Point(116, 23);
            this.NLayerInputDim.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NLayerInputDim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NLayerInputDim.Name = "NLayerInputDim";
            this.NLayerInputDim.Size = new System.Drawing.Size(51, 20);
            this.NLayerInputDim.TabIndex = 1;
            this.NLayerInputDim.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NLayerInputDim.ValueChanged += new System.EventHandler(this.NLayerInputDim_ValueChanged);
            // 
            // NLayerOutputDim
            // 
            this.NLayerOutputDim.Location = new System.Drawing.Point(116, 47);
            this.NLayerOutputDim.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NLayerOutputDim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NLayerOutputDim.Name = "NLayerOutputDim";
            this.NLayerOutputDim.Size = new System.Drawing.Size(51, 20);
            this.NLayerOutputDim.TabIndex = 2;
            this.NLayerOutputDim.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NLayerOutputDim.ValueChanged += new System.EventHandler(this.NLayerOutputDim_ValueChanged);
            // 
            // DActivationFunction
            // 
            this.DActivationFunction.FormattingEnabled = true;
            this.DActivationFunction.Items.AddRange(new object[] {
            "Sigmoid",
            "ReLU",
            "SoftPlus",
            "Identity"});
            this.DActivationFunction.Location = new System.Drawing.Point(115, 74);
            this.DActivationFunction.Name = "DActivationFunction";
            this.DActivationFunction.Size = new System.Drawing.Size(121, 21);
            this.DActivationFunction.TabIndex = 3;
            this.DActivationFunction.SelectedIndexChanged += new System.EventHandler(this.DActivationFunction_SelectedIndexChanged);
            this.DActivationFunction.Validating += new System.ComponentModel.CancelEventHandler(this.DActivationFunction_Validation);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Regularization:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Activation Function:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output Dimension:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Dimension:";
            // 
            // PTrainNet
            // 
            this.PTrainNet.Controls.Add(this.label8);
            this.PTrainNet.Controls.Add(this.label7);
            this.PTrainNet.Controls.Add(this.BBack_Train);
            this.PTrainNet.Controls.Add(this.BStartLearn);
            this.PTrainNet.Controls.Add(this.SBatchSize);
            this.PTrainNet.Controls.Add(this.SNEpochs);
            this.PTrainNet.Controls.Add(this.BLoadTrainSet);
            this.PTrainNet.Controls.Add(this.BLoadTestSet);
            this.PTrainNet.Controls.Add(this.LBLoadedTest);
            this.PTrainNet.Controls.Add(this.LBLoadedTrain);
            this.PTrainNet.Controls.Add(this.LMiniBatch);
            this.PTrainNet.Controls.Add(this.LEpochs);
            this.PTrainNet.Location = new System.Drawing.Point(4, 22);
            this.PTrainNet.Name = "PTrainNet";
            this.PTrainNet.Padding = new System.Windows.Forms.Padding(3);
            this.PTrainNet.Size = new System.Drawing.Size(550, 392);
            this.PTrainNet.TabIndex = 4;
            this.PTrainNet.Text = "TrainNet";
            this.PTrainNet.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(332, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Testing Set";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(102, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Training Set";
            // 
            // BBack_Train
            // 
            this.BBack_Train.Location = new System.Drawing.Point(9, 389);
            this.BBack_Train.Name = "BBack_Train";
            this.BBack_Train.Size = new System.Drawing.Size(75, 23);
            this.BBack_Train.TabIndex = 9;
            this.BBack_Train.Text = "Back";
            this.BBack_Train.UseVisualStyleBackColor = true;
            this.BBack_Train.Click += new System.EventHandler(this.BBack_Train_Click);
            // 
            // BStartLearn
            // 
            this.BStartLearn.Enabled = false;
            this.BStartLearn.Location = new System.Drawing.Point(449, 341);
            this.BStartLearn.Name = "BStartLearn";
            this.BStartLearn.Size = new System.Drawing.Size(95, 48);
            this.BStartLearn.TabIndex = 8;
            this.BStartLearn.Text = "Learn!";
            this.BStartLearn.UseVisualStyleBackColor = true;
            this.BStartLearn.Click += new System.EventHandler(this.BStartLearn_Click);
            // 
            // SBatchSize
            // 
            this.SBatchSize.Location = new System.Drawing.Point(153, 251);
            this.SBatchSize.Max = 500;
            this.SBatchSize.Min = 0;
            this.SBatchSize.Name = "SBatchSize";
            this.SBatchSize.Size = new System.Drawing.Size(146, 25);
            this.SBatchSize.TabIndex = 7;
            this.SBatchSize.Value = 100;
            // 
            // SNEpochs
            // 
            this.SNEpochs.Location = new System.Drawing.Point(153, 220);
            this.SNEpochs.Max = 20;
            this.SNEpochs.Min = 0;
            this.SNEpochs.Name = "SNEpochs";
            this.SNEpochs.Size = new System.Drawing.Size(137, 25);
            this.SNEpochs.TabIndex = 6;
            this.SNEpochs.Value = 2;
            // 
            // BLoadTrainSet
            // 
            this.BLoadTrainSet.Location = new System.Drawing.Point(76, 125);
            this.BLoadTrainSet.Margin = new System.Windows.Forms.Padding(0);
            this.BLoadTrainSet.Name = "BLoadTrainSet";
            this.BLoadTrainSet.Size = new System.Drawing.Size(120, 23);
            this.BLoadTrainSet.TabIndex = 5;
            this.BLoadTrainSet.Text = "Load From File";
            this.BLoadTrainSet.UseVisualStyleBackColor = true;
            this.BLoadTrainSet.Click += new System.EventHandler(this.BLoadTrainSet_Click);
            // 
            // BLoadTestSet
            // 
            this.BLoadTestSet.Location = new System.Drawing.Point(302, 125);
            this.BLoadTestSet.Margin = new System.Windows.Forms.Padding(0);
            this.BLoadTestSet.Name = "BLoadTestSet";
            this.BLoadTestSet.Size = new System.Drawing.Size(120, 23);
            this.BLoadTestSet.TabIndex = 4;
            this.BLoadTestSet.Text = "Load From File";
            this.BLoadTestSet.UseVisualStyleBackColor = true;
            this.BLoadTestSet.Click += new System.EventHandler(this.BLoadTestSet_Click);
            // 
            // LBLoadedTest
            // 
            this.LBLoadedTest.FormattingEnabled = true;
            this.LBLoadedTest.Location = new System.Drawing.Point(302, 27);
            this.LBLoadedTest.Name = "LBLoadedTest";
            this.LBLoadedTest.Size = new System.Drawing.Size(120, 95);
            this.LBLoadedTest.TabIndex = 3;
            this.LBLoadedTest.SelectedIndexChanged += new System.EventHandler(this.LBLoadedTest_SelectedIndexChanged);
            // 
            // LBLoadedTrain
            // 
            this.LBLoadedTrain.FormattingEnabled = true;
            this.LBLoadedTrain.Location = new System.Drawing.Point(76, 27);
            this.LBLoadedTrain.Name = "LBLoadedTrain";
            this.LBLoadedTrain.Size = new System.Drawing.Size(120, 95);
            this.LBLoadedTrain.TabIndex = 2;
            this.LBLoadedTrain.SelectedIndexChanged += new System.EventHandler(this.LBLoadedTrain_SelectedIndexChanged);
            // 
            // LMiniBatch
            // 
            this.LMiniBatch.AutoSize = true;
            this.LMiniBatch.Location = new System.Drawing.Point(52, 256);
            this.LMiniBatch.Name = "LMiniBatch";
            this.LMiniBatch.Size = new System.Drawing.Size(58, 13);
            this.LMiniBatch.TabIndex = 1;
            this.LMiniBatch.Text = "Batch Size";
            // 
            // LEpochs
            // 
            this.LEpochs.AutoSize = true;
            this.LEpochs.Location = new System.Drawing.Point(52, 225);
            this.LEpochs.Name = "LEpochs";
            this.LEpochs.Size = new System.Drawing.Size(95, 13);
            this.LEpochs.TabIndex = 0;
            this.LEpochs.Text = "Number of Epochs";
            // 
            // PPreview
            // 
            this.PPreview.ColumnCount = 3;
            this.PPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.93877F));
            this.PPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.06123F));
            this.PPreview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.PPreview.Location = new System.Drawing.Point(4, 420);
            this.PPreview.Name = "PPreview";
            this.PPreview.RowCount = 3;
            this.PPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PPreview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PPreview.Size = new System.Drawing.Size(496, 59);
            this.PPreview.TabIndex = 0;
            // 
            // PPreviewEdit
            // 
            this.PPreviewEdit.Location = new System.Drawing.Point(500, 420);
            this.PPreviewEdit.Name = "PPreviewEdit";
            this.PPreviewEdit.Size = new System.Drawing.Size(54, 59);
            this.PPreviewEdit.TabIndex = 3;
            // 
            // NetDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 480);
            this.Controls.Add(this.PPreviewEdit);
            this.Controls.Add(this.PPreview);
            this.Controls.Add(this.hiddenTabControl1);
            this.Name = "NetDesigner";
            this.Text = "Main Menu";
            this.hiddenTabControl1.ResumeLayout(false);
            this.PMainMenu.ResumeLayout(false);
            this.PPickANet.ResumeLayout(false);
            this.EditMLP.ResumeLayout(false);
            this.EditMLP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAddLayerPos)).EndInit();
            this.LayerEditor.ResumeLayout(false);
            this.LayerEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NLayerInputDim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NLayerOutputDim)).EndInit();
            this.PTrainNet.ResumeLayout(false);
            this.PTrainNet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private HiddenTabControl hiddenTabControl1;
        private System.Windows.Forms.TabPage PMainMenu;
        private System.Windows.Forms.TabPage PPickANet;
        private System.Windows.Forms.Button BNewNet;
        private System.Windows.Forms.Button BBuildMLP;
        private System.Windows.Forms.TabPage EditMLP;
        private System.Windows.Forms.TabPage LayerEditor;
        private System.Windows.Forms.TabPage PTrainNet;
        private System.Windows.Forms.NumericUpDown NAddLayerPos;
        private System.Windows.Forms.Button BAddLayer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DActivationFunction;
        private System.Windows.Forms.Button BDoneLayerEdit;
        private System.Windows.Forms.NumericUpDown NLayerInputDim;
        private System.Windows.Forms.NumericUpDown NLayerOutputDim;
        private System.Windows.Forms.Button BTrainNet;
        private System.Windows.Forms.Label LEpochs;
        private System.Windows.Forms.Button BLoadTestSet;
        private System.Windows.Forms.ListBox LBLoadedTest;
        private System.Windows.Forms.ListBox LBLoadedTrain;
        private System.Windows.Forms.Label LMiniBatch;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button BLoadTrainSet;
        private Slider SNEpochs;
        private Slider SBatchSize;
        private System.Windows.Forms.Button BStartLearn;
        private System.Windows.Forms.Button BBack_Train;
        private System.Windows.Forms.FlowLayoutPanel PNetSummary;
        private System.Windows.Forms.ComboBox CBRegularizationMode;
        private System.Windows.Forms.Button BEditMLPDone;
        private System.Windows.Forms.ComboBox DCostFunctionMLP;
        private System.Windows.Forms.TextBox TMLPLearningRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BEditNet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel PPreview;
        private System.Windows.Forms.Panel PPreviewEdit;
    }
}

