using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace LYC.Common
{
    public partial class IPSettings : Form
    {
        public IPSettings()
        {
            InitializeComponent();
        }

        private void SERVER_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || ((int)e.KeyChar == (int)System.Windows.Forms.Keys.Back) ||
                 (e.KeyChar == 3 || e.KeyChar==22)))//Ctrl C  & Ctrl V
            {
                e.Handled = true;
            }   
        }

        private void cbPort_DropDown(object sender, EventArgs e)
        {
            // Search valible serial ports and open them
            string[] ports = SerialPort.GetPortNames();
            cbPort.Items.Clear();
            Array.Sort(ports);
            cbPort.Items.AddRange(ports);
        }

        private void rbutton_CheckedChanged(object sender, EventArgs e)
        {
            tIPAddress.Enabled = rbNetwork.Checked;
            cbPort.Enabled = rbSerialPort.Checked;
        }
    }
}
