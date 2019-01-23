using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WYL
{
    public partial class MainForm : Form
    {
        MTKResourceClass    m_mtkResource;
        public MainForm()
        {
            InitializeComponent();
        }

        private void tsFontView_Click(object sender, EventArgs e)
        {
            FontView frm = new FontView();
            frm.Show();
        }

        private void tsLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            openFileDialog1.Filter = "BIN files (*.bin)|*.bin";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    m_mtkResource = new MTKResourceClass(openFileDialog1.FileName);
            }

        }

        private void tsOLFont_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            openFileDialog1.Filter = "BIN files (*.bin)|*.bin";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    m_mtkResource = new MTKResourceClass(openFileDialog1.FileName, true);
            }

        }
    }
}
