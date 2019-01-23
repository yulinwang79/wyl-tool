using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using Microsoft.Win32;
using System.Threading;
using System.Net;
using LYC.Common.Tcp;
using LYC.Common.Ftp;
using LYC.Common;
using System.Net.Sockets;
using System.Diagnostics;
using Telnet;
using LYC.Common.Serial;
using System.Management;


namespace DownLoader
{


    public partial class MainForm : LYC.Common.FormBase
    {

        const string MBCFG_FILENAME = "/hmi/disk/hmidata.dat";
        public string m_filename = "";
        public string m_safefilename = "";

        public MainForm()
        {
            InitializeComponent();

            SetConnectStateDisplay();
            m_spPort.ReceivedDataHandle = m_atc.ReceivedFunc;

            SourceFile = System.Windows.Forms.Application.StartupPath + "\\configfile" + "\\hmidata.dat";
            TargetFile = "/hmi/disk/hmidata.dat";
        }
        private void SetConnectStateDisplay()
        {
            if (m_connection == ConnectionType.NETWORK)
            {
                tsslConnection.Text = "Connection:Network--";
            }
            else if (m_connection == ConnectionType.SERIAL_PORT)
            {
                tsslConnection.Text = "Connection:Serial Port --";
            }
            if (IsConnected())
            {
                tsslConnectState.Text = "Connected";
                tsConnect.Text = "Disconnect";
                tsDownLoad.Enabled = true;
                tsReadBack.Enabled = true;
                tsReboot.Enabled = true;
            }
            else
            {
                tsslConnectState.Text = "Disconnected";
                tsConnect.Text = "Connect";
                tsDownLoad.Enabled = false;
                tsReadBack.Enabled = false;
                tsReboot.Enabled = false;
            }

        }

        private void tsOpen_Click(object sender, EventArgs e)
        {
            // Provide a file dialog for browsing for the sound file.
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((fileName = openFileDialog1.FileName) != null)
                {
                    m_filename = fileName;
                    m_safefilename = openFileDialog1.SafeFileName;
                    tbFile.Text = m_filename;
                }
            }
        }


        private void tsDownLoad_Click(object sender, EventArgs e)
        {
            if (cbSize.Text != null && cbSize.Text.Length > 0)
            {
                BlockSize = 128;
            }
            else
            {
                BlockSize = Int16.Parse(cbSize.Text);
            }

            SourceFile = tbFile.Text;
            if (rbHmi.Checked)
            {
                TargetFile = "/hmi/disk/" + m_safefilename;
            }
            else
            {
                TargetFile = "/core/disk/" + m_safefilename;
            }
            if (DownLoadFile())
            {
                tsPB.Maximum = tsPB.Value = 0;
                tsPB.Visible = true;
                tsDownLoad.Enabled = false;
                tsReadBack.Enabled = false;
                tsReboot.Enabled = false;
            }

        }
        private bool IsValidFileName(string fileName)
        {
            bool isValid = true;
            string errChar = "\\/:*?\"<>|";  //
            if (string.IsNullOrEmpty(fileName))
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < errChar.Length; i++)
                {
                    if (fileName.Contains(errChar[i].ToString()))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }
        private void tsReadBack_Click(object sender, EventArgs e)
        {

            if (IsValidFileName(tbFile.Text))
            {
                SourceFile = System.Windows.Forms.Application.StartupPath + "\\" + tbFile.Text;
                if (rbHmi.Checked)
                {
                    TargetFile = "/hmi/disk/" + tbFile.Text;
                }
                else
                {
                    TargetFile = "/core/disk/" + tbFile.Text;
                }
                if (ReadBackFile())
                {
                    tsPB.Maximum = tsPB.Value = 0;
                    tsPB.Visible = true;
                    tsDownLoad.Enabled = false;
                    tsReadBack.Enabled = false;

                    tsReboot.Enabled = false;
                }

            }
            else
            {
                MessageBox.Show("Invalid File name!");
            }
        }


        private void tsConnect_Click(object sender, EventArgs e)
        {
            ConnectTarget();
            SetConnectStateDisplay();

        }


        private void tsReboot_Click(object sender, EventArgs e)
        {
            if (rbHmi.Checked)
            {
                RebootTarget(TargetFileType.HMI);

            }
            else
            {
                RebootTarget(TargetFileType.CORE);

            }


        }


        protected override void FileTransmittingCallback(atResult result)
        {
            tsPB.Maximum = result.Parameter2;
            tsPB.Value = result.Parameter1;


            if (result.ResultType != LYC.Common.ATResultType.SingleSuccess)
            {
                tsPB.Visible = false;
                tsDownLoad.Enabled = true;
                tsReadBack.Enabled = true;
                tsReboot.Enabled = true;
            }

        }

        protected override void RebootingCallback()
        {
            tsDownLoad.Enabled = false;
            tsReadBack.Enabled = false;
            tsReboot.Enabled = false;
        }

        private void cbSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || ((int)e.KeyChar == (int)System.Windows.Forms.Keys.Back)))
            {
                e.Handled = true;
            }
        }
    }
}
