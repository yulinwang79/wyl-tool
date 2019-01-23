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


namespace LYC_DownLoader
{
    public partial class MainForm : LYC.Common.FormBase
    {
        public MainForm()
        {
            InitializeComponent();

            m_spPort.ReceivedDataHandle = m_atc.ReceivedFunc;

            string CorefileName = System.Windows.Forms.Application.StartupPath + "\\core_update.dat"; /*"\\modbus_daemon.app"; vc_hmi.app*/
            string HmifileName = System.Windows.Forms.Application.StartupPath + "\\hmi_update.dat"; /*"\\modbus_daemon.app"; vc_hmi.app*/

            if (File.Exists(CorefileName))
            {
                tbCoreFile.Text = "core update";
                btUpdateCore.Enabled = true;
                btUpdateCore.Tag = true;
            }
            else
            {
                btUpdateCore.Tag = false;
            }

            if (File.Exists(HmifileName))
            {
                tbHmiFile.Text = "hmi update";
                btUpdateHmi.Enabled = true;
                btUpdateHmi.Tag = true;
            }
            else
            {
                btUpdateHmi.Tag = false;
            }

            SetConnectStateDisplay();
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
                tsReboot.Enabled = true;

            }
            else
            {
                tsslConnectState.Text = "Disconnected";
                tsConnect.Text = "Connect";
                tsReboot.Enabled = false;
                if ((bool)btUpdateCore.Tag == true)
                    btUpdateCore.Enabled = true;
                if ((bool)btUpdateHmi.Tag == true)
                    btUpdateHmi.Enabled = true;

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

        private void btUpdateCore_Click(object sender, EventArgs e)
        {
            BlockSize = 450;
            SourceFile = System.Windows.Forms.Application.StartupPath + "\\core_update.dat";
            TargetFile = "/core/disk/" + "modbus_daemon.app";
            if (DownLoadFile())
            {
                btUpdateCore.Enabled = false;
                btUpdateHmi.Enabled = false;
            }
        }

        private void btUpdateHmi_Click(object sender, EventArgs e)
        {
            BlockSize = 128;
            SourceFile = System.Windows.Forms.Application.StartupPath + "\\hmi_update.dat";
            TargetFile = "/hmi/disk/" + "vc_hmi.app";
            if (DownLoadFile())
            {
                btUpdateCore.Enabled = false;
                btUpdateHmi.Enabled = false;
            }
        }

        protected override void FileTransmittingCallback(atResult result)
        {
            tsPB.Maximum = result.Parameter2;
            tsPB.Value = result.Parameter1;


            if (result.ResultType != LYC.Common.ATResultType.SingleSuccess)
            {
                tsPB.Visible = false;
                if (m_IsDownLoad)
                {
                    if ((bool)btUpdateCore.Tag == true)
                        btUpdateCore.Enabled = true;
                    if ((bool)btUpdateHmi.Tag == true)
                        btUpdateHmi.Enabled = true;
                }
            }

        }

        protected override void RebootingCallback()
        {
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
