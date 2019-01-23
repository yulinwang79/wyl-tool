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


namespace HmiConfig
{
    public partial class MainForm : LYC.Common.FormBase
    {
        private HmiData m_hmiDataFile;
        private readonly HmiData m_hmiDataFile_default;
        //const string MBCFG_FILENAME = "/hmi/disk/hmidata.dat";

        //xValveData m_cusValveData;
        int m_curGroupId = -1;
        int m_curValveId = -1;

        public MainForm()
        {
            InitializeComponent();
            m_hmiDataFile_default = new HmiData();
            tbValveName.MaxLength = 15; //VALVE_NAME_MAX_LENTH
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
            }
            else
            {
                tsslConnectState.Text = "Disconnected";
                tsConnect.Text = "Connect";
                tsDownLoad.Enabled = false;
                tsReadBack.Enabled = false;
            }

        }

        private void SetPortConfigDisplay()
        {

            this.Text = "ConfigTool  -" + m_hmiDataFile.FileName;
            tvPortInfo.BeginUpdate();
            tvPortInfo.Nodes.Add("Hmi Config");
            //tvPortInfo.Nodes[0].ContextMenuStrip = cmsPortConfig;

            for (int i = 0; i < m_hmiDataFile.xGroup.Length; i++)
            {
                tvPortInfo.Nodes[0].Nodes.Add("ValveGroup " + (i + 1));
                for (int j = 0; j < m_hmiDataFile.xGroup[i].xValve.Length; j++)
                {
                    tvPortInfo.Nodes[0].Nodes[i].Nodes.Add("ValveData " + (j + 1));
                    tvPortInfo.Nodes[0].Nodes[i].Nodes[j].Tag = m_hmiDataFile.xGroup[i].xValve[j];
                }
            }
            tvPortInfo.Nodes[0].Expand();
            tvPortInfo.EndUpdate();
            ResetDisplay();
            ResetValveId();
        }


        private void UpdateDisplay()
        {
            xValveData cusValveData = GetCurrValveData();
            lGroupId.Text = "G" + (m_curGroupId + 1);
            lValveId.Text = "V" + (m_curValveId + 1);
            tbCtrlOpenAddr.Text = cusValveData.xCmd.dBOffsetForOpenCmd.ToString();
            tbCtrlOpenCmd.Text = cusValveData.xCmd.openCmd.ToString();

            tbCtrlCloseAddr.Text = cusValveData.xCmd.dBOffsetForCloseCmd.ToString();
            tbCtrlCloseCmd.Text = cusValveData.xCmd.closeCmd.ToString();

            tbCtrlStopAddr.Text = cusValveData.xCmd.dBoffsetForStopCmd.ToString();
            tbCtrlStopCmd.Text = cusValveData.xCmd.stopCmd.ToString();

            tbCtrlPoscAddr.Text = cusValveData.xCmd.dBOffsetForCurPosCmd.ToString();
            tbCtrlPoscCmd.Text = cusValveData.xCmd.getCurPosCmd.ToString();

            tbCtrlPosAddr.Text = cusValveData.xCmd.dbOffsetForCurPos.ToString();
            tbCtrlPosCmd.Text = cusValveData.xCmd.curPosMaxVal.ToString();

            tbViewOpenAddr.Text = cusValveData.xView.dBOffsetForOpenVal.ToString();

            tbViewCloseAddr.Text = cusValveData.xView.dBOffsetForCloseVal.ToString();

            tbViewLRAddr.Text = cusValveData.xView.dBOffsetForLRVal.ToString();

            tbViewPosAddr.Text = cusValveData.xView.dBOffsetForCurPos.ToString();

            tbViewPosMax.Text = cusValveData.xView.curPosMaxVal.ToString();

            UTF8Encoding temp = new UTF8Encoding(true);
            tbValveName.Text = temp.GetString(cusValveData.acValveName);
        }

        private void ResetDisplay()
        {

            lGroupId.Text = "";
            lValveId.Text = "";
            tbCtrlOpenAddr.Text = "";
            tbCtrlOpenCmd.Text = "";

            tbCtrlCloseAddr.Text = "";
            tbCtrlCloseCmd.Text = "";

            tbCtrlStopAddr.Text = "";
            tbCtrlStopCmd.Text = "";

            tbCtrlPoscAddr.Text = "";
            tbCtrlPoscCmd.Text = "";

            tbCtrlPosAddr.Text = "";
            tbCtrlPosCmd.Text = "";

            tbViewOpenAddr.Text = "";

            tbViewCloseAddr.Text = "";

            tbViewLRAddr.Text = "";

            tbViewPosAddr.Text = "";

            tbViewPosMax.Text = "";

            tbValveName.Text = "";
        }

        private void ResetValveId()
        {
            m_curGroupId = -1;
            m_curValveId = -1;
            if (btSaveValveData.Enabled)
            {
                btSaveValveData.Enabled = false;
            }
        }
        private void SetValveId(int GroupId, int ValveId)
        {
            m_curGroupId = GroupId;
            m_curValveId = ValveId;
            if (!btSaveValveData.Enabled)
            {
                btSaveValveData.Enabled = true;
            }
        }
        private void UpdateValveData()
        {
            xValveData cusValveData = GetCurrValveData();

            if (tbCtrlOpenAddr.TextLength == 0 && UInt16.Parse(tbCtrlOpenAddr.Text) > UInt16.MaxValue
                || tbCtrlOpenCmd.TextLength == 0 && UInt16.Parse(tbCtrlOpenCmd.Text) > UInt16.MaxValue
                || tbCtrlCloseAddr.TextLength == 0 && UInt16.Parse(tbCtrlCloseAddr.Text) > UInt16.MaxValue
                || tbCtrlCloseCmd.TextLength == 0 && UInt16.Parse(tbCtrlCloseCmd.Text) > UInt16.MaxValue
                || tbCtrlStopAddr.TextLength == 0 && UInt16.Parse(tbCtrlStopAddr.Text) > UInt16.MaxValue
                || tbCtrlStopCmd.TextLength == 0 && UInt16.Parse(tbCtrlStopCmd.Text) > UInt16.MaxValue
                || tbCtrlPoscAddr.TextLength == 0 && UInt16.Parse(tbCtrlPoscAddr.Text) > UInt16.MaxValue
                || tbCtrlPoscCmd.TextLength == 0 && UInt16.Parse(tbCtrlPoscCmd.Text) > UInt16.MaxValue
                || tbCtrlPosAddr.TextLength == 0 && UInt16.Parse(tbCtrlPosAddr.Text) > UInt16.MaxValue
                || tbCtrlPosCmd.TextLength == 0 && UInt16.Parse(tbCtrlPosCmd.Text) > UInt16.MaxValue
                || tbViewOpenAddr.TextLength == 0 && UInt32.Parse(tbViewOpenAddr.Text) > UInt32.MaxValue
                || tbViewCloseAddr.TextLength == 0 && UInt32.Parse(tbViewCloseAddr.Text) > UInt32.MaxValue
                || tbViewLRAddr.TextLength == 0 && UInt32.Parse(tbViewLRAddr.Text) > UInt32.MaxValue
                || tbViewCloseAddr.TextLength == 0 && UInt32.Parse(tbViewCloseAddr.Text) > UInt32.MaxValue
                || tbViewPosAddr.TextLength == 0 && UInt32.Parse(tbViewPosAddr.Text) > UInt32.MaxValue
                || tbViewPosMax.TextLength == 0 && UInt16.Parse(tbViewPosMax.Text) > UInt16.MaxValue
                )
            {
                MessageBox.Show("Have one or more of the input exceeds the maximum value, or input is empty!", "Error");
            }
            cusValveData.xCmd.dBOffsetForOpenCmd = UInt16.Parse(tbCtrlOpenAddr.Text);
            cusValveData.xCmd.openCmd = UInt16.Parse(tbCtrlOpenCmd.Text);

            cusValveData.xCmd.dBOffsetForCloseCmd = UInt16.Parse(tbCtrlCloseAddr.Text);
            cusValveData.xCmd.closeCmd = UInt16.Parse(tbCtrlCloseCmd.Text);

            cusValveData.xCmd.dBoffsetForStopCmd = UInt16.Parse(tbCtrlStopAddr.Text);
            cusValveData.xCmd.stopCmd = UInt16.Parse(tbCtrlStopCmd.Text);

            cusValveData.xCmd.dBOffsetForCurPosCmd = UInt16.Parse(tbCtrlPoscAddr.Text);
            cusValveData.xCmd.getCurPosCmd = UInt16.Parse(tbCtrlPoscCmd.Text);

            cusValveData.xCmd.dbOffsetForCurPos = UInt16.Parse(tbCtrlPosAddr.Text);
            cusValveData.xCmd.curPosMaxVal = UInt16.Parse(tbCtrlPosCmd.Text);

            cusValveData.xView.dBOffsetForOpenVal = UInt32.Parse(tbViewOpenAddr.Text);

            cusValveData.xView.dBOffsetForCloseVal = UInt32.Parse(tbViewCloseAddr.Text);

            cusValveData.xView.dBOffsetForLRVal = UInt32.Parse(tbViewLRAddr.Text);

            cusValveData.xView.dBOffsetForCurPos = UInt32.Parse(tbViewPosAddr.Text);

            cusValveData.xView.curPosMaxVal = UInt32.Parse(tbViewPosMax.Text);

            int i = 0;
            for (; i < cusValveData.acValveName.Length - 1 && i < tbValveName.Text.Length; i++)
            {
                cusValveData.acValveName[i] = (byte)tbValveName.Text[i];
            }
            cusValveData.acValveName[i] = 0;

        }
        private xValveData GetCurrValveData()
        {
            if (m_curGroupId >= 0 && m_curValveId >= 0)
            {
                return m_hmiDataFile.xGroup[m_curGroupId].xValve[m_curValveId];
            }
            return null;
        }

        private void tsOpen_Click(object sender, EventArgs e)
        {
            // Provide a file dialog for browsing for the sound file.
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "CFG files (*.dat)|*.dat";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((fileName = openFileDialog1.FileName) != null)
                {
                    Openfile(fileName);
                }
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

            SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            SaveFileDialog1.Filter = "CFG files (*.dat)|*.dat";
            SaveFileDialog1.FilterIndex = 1;
            SaveFileDialog1.RestoreDirectory = true;

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_hmiDataFile = new HmiData();
                m_hmiDataFile.SaveFile(SaveFileDialog1.FileName);
                if (tvPortInfo.Nodes.Count > 0)
                    tvPortInfo.Nodes.Clear();
                SetPortConfigDisplay();
            }

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (m_hmiDataFile != null)
            {
                if (m_hmiDataFile.FileName != null)
                    m_hmiDataFile.SaveFile();
                else
                    tsSaveAs_Click(sender, e);
            }
        }

        private void tsSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

            SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            SaveFileDialog1.Filter = "CFG files (*.dat)|*.dat";
            SaveFileDialog1.FilterIndex = 1;
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.Title = "Save file";

            if (m_hmiDataFile != null && SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Savefile(SaveFileDialog1.FileName);
            }
        }

        private void tsDownLoad_Click(object sender, EventArgs e)
        {
            if (m_hmiDataFile != null)
            {
                Savefile(SourceFile);
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

        private void tsReadBack_Click(object sender, EventArgs e)
        {
            if (ReadBackFile())
            {
                tsPB.Maximum = tsPB.Value = 0;
                tsPB.Visible = true;
                tsDownLoad.Enabled = false;
                tsReadBack.Enabled = false;
                tsReboot.Enabled = false;
            }
        }


        private void tsConnect_Click(object sender, EventArgs e)
        {
            ConnectTarget();
            SetConnectStateDisplay();
        }

        private void tsReboot_Click(object sender, EventArgs e)
        {
            RebootTarget(TargetFileType.HMI);
        }

        private void tvPortInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Tag != null && e.Node.Tag.GetType() == typeof(xValveData))
            {
                SetValveId(e.Node.Parent.Index, e.Node.Index);
                UpdateDisplay();
            }
        }

        private void tvPortInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void tsmiPortConfigConfigure_Click(object sender, EventArgs e)
        {

        }

        private void tsmiPortConfigCopy_Click(object sender, EventArgs e)
        {

        }

        private void tsmiPortConfigPaste_Click(object sender, EventArgs e)
        {

        }

        private void tvPortInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvPortInfo.SelectedNode = tvPortInfo.GetNodeAt(e.X, e.Y);
            }

        }

        private void Openfile(string fileName)
        {
            m_hmiDataFile = new HmiData(fileName);
            if (m_hmiDataFile.m_loaded)
            {
                UTF8Encoding temp = new UTF8Encoding(true);

                PasswordForm dlg = new PasswordForm(temp.GetString(m_hmiDataFile.password));
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (tvPortInfo.Nodes.Count > 0)
                        tvPortInfo.Nodes.Clear();
                    SetPortConfigDisplay();
                }


            }
        }

        private void Savefile(string fileName)
        {
            m_hmiDataFile.SaveFile(fileName);
            this.Text = "ConfigTool  -" + m_hmiDataFile.FileName;
        }


        private void btSaveValveData_Click(object sender, EventArgs e)
        {
            UpdateValveData();
        }

        private void Data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || ((int)e.KeyChar == (int)System.Windows.Forms.Keys.Back) ||
                (e.KeyChar == 3 || e.KeyChar == 22)))//Ctrl C  & Ctrl V
            {
                e.Handled = true;
            }
        }

        private void tbCtrlDbOffset_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 0 && UInt16.Parse(((TextBox)sender).Text) > UInt16.MaxValue)
            {
                MessageBox.Show("Beyond the maximum input!", "Error");
            }

        }

        private void tbCtrlDbCmd_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 0 && UInt16.Parse(((TextBox)sender).Text) > UInt16.MaxValue)
            {
                MessageBox.Show("Beyond the maximum input!", "Error");
            }
        }

        private void tbViewDbOffset_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 0 && UInt32.Parse(((TextBox)sender).Text) > UInt32.MaxValue)
            {
                MessageBox.Show("Beyond the maximum input!", "Error");
            }
        }

        private void tbPos_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).TextLength > 0 && UInt16.Parse(((TextBox)sender).Text) > UInt16.MaxValue)
            {
                MessageBox.Show("Beyond the maximum input!", "Error");
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

                if (!m_IsDownLoad && result.ResultType == LYC.Common.ATResultType.Success)
                {
                    Openfile(SourceFile);
                }

            }
        }

        protected override void RebootingCallback()
        {
            tsDownLoad.Enabled = false;
            tsReadBack.Enabled = false;
            tsReboot.Enabled = false;
        }

    }

}
