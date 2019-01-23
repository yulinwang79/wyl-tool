using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WYL
{
    public partial class LoadFontRangeDataForm : Form
    {
        public LoadFontRangeDataForm()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (rtbCustFontData.TextLength <= 0)
            {
                MessageBox.Show("CustFontData can not be empty!");
                return;
            }
            if (rtbRangeData.TextLength <= 0)
            {
                MessageBox.Show("RangeData can not be empty!");
                return;
            }
        }
    }
}
