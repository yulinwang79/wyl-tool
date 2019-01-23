using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WYL
{
    public partial class OpenBinForm : Form
    {
        public string FileName;

        public OpenBinForm()
        {
            InitializeComponent();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            BdfClass s_bdf = new BdfClass();
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            openFileDialog1.Filter = "BIN files (*.bin)|*.bin";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((FileName = openFileDialog1.FileName) != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

        }
    }
}
