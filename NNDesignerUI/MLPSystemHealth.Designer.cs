namespace NNDesignerUI
{
    partial class MLPSystemHealth
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.CostChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TestChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.WeightChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.BMLPClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CostChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeightChart)).BeginInit();
            this.SuspendLayout();
            // 
            // CostChart
            // 
            chartArea1.Name = "ChartArea1";
            this.CostChart.ChartAreas.Add(chartArea1);
            this.CostChart.Location = new System.Drawing.Point(2, 29);
            this.CostChart.Name = "CostChart";
            this.CostChart.Size = new System.Drawing.Size(263, 248);
            this.CostChart.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Training Cost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Testing Error";
            // 
            // TestChart
            // 
            chartArea2.Name = "ChartArea1";
            this.TestChart.ChartAreas.Add(chartArea2);
            this.TestChart.Location = new System.Drawing.Point(271, 29);
            this.TestChart.Name = "TestChart";
            this.TestChart.Size = new System.Drawing.Size(255, 248);
            this.TestChart.TabIndex = 3;
            // 
            // WeightChart
            // 
            chartArea3.Name = "ChartArea1";
            this.WeightChart.ChartAreas.Add(chartArea3);
            this.WeightChart.Location = new System.Drawing.Point(12, 314);
            this.WeightChart.Name = "WeightChart";
            this.WeightChart.Size = new System.Drawing.Size(506, 115);
            this.WeightChart.TabIndex = 4;
            this.WeightChart.Text = "chart1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Weight Magnitude";
            // 
            // BMLPClear
            // 
            this.BMLPClear.Location = new System.Drawing.Point(223, 0);
            this.BMLPClear.Name = "BMLPClear";
            this.BMLPClear.Size = new System.Drawing.Size(91, 23);
            this.BMLPClear.TabIndex = 6;
            this.BMLPClear.Text = "Clear";
            this.BMLPClear.UseVisualStyleBackColor = true;
            this.BMLPClear.Click += new System.EventHandler(this.BMLPClear_Click);
            // 
            // MLPSystemHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 441);
            this.Controls.Add(this.BMLPClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.WeightChart);
            this.Controls.Add(this.TestChart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CostChart);
            this.Name = "MLPSystemHealth";
            this.Text = "MLPSystemHealth";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MLPSystemHealth_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.CostChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TestChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WeightChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart CostChart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart TestChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart WeightChart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BMLPClear;
    }
}