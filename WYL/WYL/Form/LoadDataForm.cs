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
    public partial class LoadDataForm : Form
    {
        public BdfClass m_bdf = new BdfClass();
       
        public LoadDataForm()
        {
            
            InitializeComponent();
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btCreate_Click(object sender, EventArgs e)
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
            if (rtbOffset.TextLength <= 0)
            {
                MessageBox.Show("Offset can not be empty!");
                return;       
            }
            if (rtbData.TextLength <= 0)
            {
                MessageBox.Show("Data can not be empty!");
                return;
            }
            if (m_bdf.LoadData(rtbCustFontData.Text, rtbRangeData.Text, rtbWidth.Text, rtbDWidth.Text, rtbOffset.Text, rtbData.Text))
            {
                //m_parent.LoadBdf(m_bdf);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
