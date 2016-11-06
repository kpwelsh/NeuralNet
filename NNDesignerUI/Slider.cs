using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NNDesignerUI
{
    public partial class Slider : UserControl
    {
        [Category("Properties"),Description("Maximum value to allow")]
        public int Max
        {
            get
            {
                return Bar.Maximum;
            }
            set
            {
                Bar.Maximum = value;
                UpdateTextWidth(Math.Max(Math.Abs(Bar.Maximum), Math.Abs(Bar.Minimum)));
            }
        }

        [Category("Properties"), Description("Minimum value to allow")]
        public int Min
        {
            get
            {
                return Bar.Minimum;
            }
            set
            {
                Bar.Minimum = value;
                UpdateTextWidth(Math.Max(Math.Abs(Bar.Maximum),Math.Abs(Bar.Minimum)));
            }
        }

        [Category("Properties")]
        public int Value
        {
            get
            {
                return Bar.Value;
            }
            set
            {
                Bar.Value = value;
                Number.Text = value.ToString();
            }
        }

        public Slider()
        {
            InitializeComponent();
        }

        private void Number_TextChanged(object sender, EventArgs e)
        {
            int val;
            if (!int.TryParse(Number.Text, out val)) val = 0;

            val = Math.Max(Math.Min(val, Bar.Maximum), Bar.Minimum);
            Bar.Value = val;
            Number.Text = val.ToString();
            UpdateTextWidth(Math.Max(val, Bar.Value));
        }

        private void Bar_Scroll(object sender, EventArgs e)
        {
            Number.Text = Bar.Value.ToString();
        }

        private void UpdateTextWidth(int n)
        {
            int c = (int)Math.Ceiling(Math.Log10(n+1));
            Number.Width = 9 * c + 3;
            Width = Bar.Width + Number.Width + 12;
        }
    }
}
