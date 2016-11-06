namespace NNDesignerUI
{
    partial class Slider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Number = new System.Windows.Forms.TextBox();
            this.Bar = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.Bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // Number
            // 
            this.Number.Location = new System.Drawing.Point(114, 3);
            this.Number.Name = "Number";
            this.Number.Size = new System.Drawing.Size(30, 20);
            this.Number.TabIndex = 0;
            this.Number.Validated += new System.EventHandler(this.Number_TextChanged);
            // 
            // Bar
            // 
            this.Bar.AutoSize = false;
            this.Bar.Location = new System.Drawing.Point(4, 5);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(104, 15);
            this.Bar.TabIndex = 1;
            this.Bar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Bar.Scroll += new System.EventHandler(this.Bar_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(141, 144);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 2;
            // 
            // Slider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.Bar);
            this.Controls.Add(this.Number);
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(151, 28);
            ((System.ComponentModel.ISupportInitialize)(this.Bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Number;
        private System.Windows.Forms.TrackBar Bar;
        private System.Windows.Forms.TrackBar trackBar2;
    }
}
