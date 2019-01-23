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

namespace Config
{
    public partial class SearialPortSettings : Form
    {
        public SearialPortSettings()
        {
            InitializeComponent();
        }

        private void cbPort_DropDown(object sender, EventArgs e)
        {
            // Search valible serial ports and open them
            string[] ports = SerialPort.GetPortNames();
            cbPort.Items.Clear();
            Array.Sort(ports);
            cbPort.Items.AddRange(ports);
        }
    }
}
