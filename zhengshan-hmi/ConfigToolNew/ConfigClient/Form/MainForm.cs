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



namespace Config
{
    public partial class MainForm : LYC.Common.FormBase
    {
        private PortConfigFile m_portConfigFile;
        private readonly PortConfigFile m_portConfigFile_default;
        private ConfigNode m_copyNode;
        //const string MBCFG_FILENAME = "/core/disk/modbus.cfg";
        public MainForm()
        {
            InitializeComponent();
            m_portConfigFile_default = new PortConfigFile(Config.Resource.Resource.DefaultCfg);
            SetConnectStateDisplay();
            SourceFile = System.Windows.Forms.Application.StartupPath + "\\configfile" + "\\modbus.cfg";
            TargetFile = "/core/disk/modbus.cfg";
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
                tsCommand.Enabled = true;
            }
            else
            {
                tsslConnectState.Text = "Disconnected";
                tsConnect.Text = "Connect";
                tsDownLoad.Enabled = false;
                tsReadBack.Enabled = false;
                tsReboot.Enabled = false;
                tsCommand.Enabled = false;
            }

        }

        private void SetPortConfigDisplay()
        {
            int node_id = 0;
            this.Text = "ConfigTool  -" + m_portConfigFile.FileName;
            tvPortInfo.BeginUpdate();
            tvPortInfo.Nodes.Add("Module Config");
            tvPortInfo.Nodes[0].ContextMenuStrip = cmsPortConfig;

            if (m_portConfigFile.m_portConfigArray.Count > 0)
            {
                tvPortInfo.Nodes[0].Nodes.Add("Group Port Config");
                for (int i = 0; i < m_portConfigFile.m_portConfigArray.Count; i++)
                {
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes.Add(((PortConfig)(m_portConfigFile.m_portConfigArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].ContextMenuStrip = cmsPortConfig;

                    PortConfig[] currentPortConfig = new PortConfig[]{(PortConfig)m_portConfigFile.m_portConfigArray[i],
                                                                       (PortConfig)m_portConfigFile_default.m_portConfigArray[i] };

                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Tag = currentPortConfig;
                }

                node_id++;
            }
            if (m_portConfigFile.m_portCommandsArray.Count > 0)
            {
                tvPortInfo.Nodes[0].Nodes.Add("Group Port Commands");
                for (int i = 0; i < m_portConfigFile.m_portCommandsArray.Count; i++)
                {
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes.Add(((PortCommands)(m_portConfigFile.m_portCommandsArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].ContextMenuStrip = cmsPortConfig;
                    PortCommands[] currentPortCommands = new PortCommands[]{(PortCommands)m_portConfigFile.m_portCommandsArray[i],
                                                                                                        (PortCommands)m_portConfigFile_default.m_portCommandsArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Tag = currentPortCommands;
                }
                node_id++;
            }
            if (m_portConfigFile.m_pcPortConfigArray.Count > 0)
            {
                tvPortInfo.Nodes[0].Nodes.Add("Modbus PC Port");
                for (int i = 0; i < m_portConfigFile.m_pcPortConfigArray.Count; i++)
                {
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes.Add("Modbus PC Port " + i);
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].ContextMenuStrip = cmsPortConfig;
                    PortConfig[] currentPortConfig = new PortConfig[]{(PortConfig)m_portConfigFile.m_pcPortConfigArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_pcPortConfigArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Tag = currentPortConfig;
                }
                node_id++;
            }
            if (m_portConfigFile.m_pcPortCommandsArray.Count > 0)
            {
                tvPortInfo.Nodes[0].Nodes.Add("Modbus PC Port Commands");
                for (int i = 0; i < m_portConfigFile.m_pcPortCommandsArray.Count; i++)
                {
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes.Add(((PortCommands)(m_portConfigFile.m_pcPortCommandsArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].ContextMenuStrip = cmsPortConfig;
                    PortCommands[] currentPortCommands = new PortCommands[]{(PortCommands)m_portConfigFile.m_pcPortCommandsArray[i],
                                                                                                        (PortCommands)m_portConfigFile_default.m_pcPortCommandsArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Tag = currentPortCommands;
                }
                node_id++;
            }

            if (m_portConfigFile.m_etherNetPortArray.Count > 0)
            {
                tvPortInfo.Nodes[0].Nodes.Add("EtherNet");
                for (int i = 0; i < m_portConfigFile.m_etherNetPortArray.Count; i++)
                {
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes.Add(((PortConfig)(m_portConfigFile.m_etherNetPortArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].ContextMenuStrip = cmsPortConfig;
                    PortConfig[] currentEtherNet = new PortConfig[]{(PortConfig)m_portConfigFile.m_etherNetPortArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_etherNetPortArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Tag = currentEtherNet;

                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes.Add(((PortConfig)(m_portConfigFile.m_etherNetClientConfigArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[0].ContextMenuStrip = cmsPortConfig;
                    PortConfig[] curEtherNetClientCfg = new PortConfig[]{(PortConfig)m_portConfigFile.m_etherNetClientConfigArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_etherNetClientConfigArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[0].Tag = curEtherNetClientCfg;

                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes.Add(((PortCommands)(m_portConfigFile.m_etherNetClientCommandsArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[1].ContextMenuStrip = cmsPortConfig;
                    PortCommands[] curEtherNetClientCmd = new PortCommands[]{(PortCommands)m_portConfigFile.m_etherNetClientCommandsArray[i],
                                                                                                        (PortCommands)m_portConfigFile_default.m_etherNetClientCommandsArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[1].Tag = curEtherNetClientCmd;


                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes.Add(((PortConfig)(m_portConfigFile.m_etherNetServerConfigArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[2].ContextMenuStrip = cmsPortConfig;
                    PortConfig[] curEtherNetServerCfg = new PortConfig[]{(PortConfig)m_portConfigFile.m_etherNetServerConfigArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_etherNetServerConfigArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[2].Tag = curEtherNetServerCfg;

                }
                node_id++;
            }

            tvPortInfo.Nodes[0].Nodes.Add(m_portConfigFile.m_dataRemap.GetName());
            tvPortInfo.Nodes[0].Nodes[node_id].ContextMenuStrip = cmsPortConfig;
            PortCommands[] curDataRemap = new PortCommands[]{(PortCommands)m_portConfigFile.m_dataRemap,
                                                                                                (PortCommands)m_portConfigFile_default.m_dataRemap };
            tvPortInfo.Nodes[0].Nodes[node_id].Tag = curDataRemap;

            tvPortInfo.Nodes[0].Expand();
            tvPortInfo.EndUpdate();
            ShowAllContent();
        }
        public void ShowContent(ConfigNode node)
        {
            ArrayList OffsetArray = new ArrayList();
            ArrayList LengthArray = new ArrayList();
            rtbPortInfo.Text = node.GetTextStringWithErrorInfo(OffsetArray, LengthArray);
            for (int i = 0; i < OffsetArray.Count; i++)
            {
                rtbPortInfo.Select((int)OffsetArray[i], (int)LengthArray[i]);
                //rtbPortInfo.SelectionColor = Color.Red;
                rtbPortInfo.SelectionBackColor = Color.Red;
            }
        }
        public void ShowAllContent()
        {
            ArrayList OffsetArray = new ArrayList();
            ArrayList LengthArray = new ArrayList();
            rtbPortInfo.Clear();
            rtbPortInfo.Text = m_portConfigFile.GetTextStringWithErrorInfo(OffsetArray, LengthArray);
            for (int i = 0; i < OffsetArray.Count; i++)
            {
                rtbPortInfo.Select((int)OffsetArray[i], (int)LengthArray[i]);
                //rtbPortInfo.SelectionColor = Color.Red;
                rtbPortInfo.SelectionBackColor = Color.Red;
            }
        }
        public static int MemoryCompare(byte[] b1, byte[] b2)
        {
            int result = 0;
            if (b1.Length != b2.Length)
                result = b1.Length - b2.Length;
            else
            {
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i])
                    {
                        result = (int)(b1[i] - b2[i]);
                        break;
                    }
                }
            }
            return result;
        }

        private void tsOpen_Click(object sender, EventArgs e)
        {
            // Provide a file dialog for browsing for the sound file.
            string fileName;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            openFileDialog1.Filter = "CFG files (*.cfg)|*.cfg";
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
            SaveFileDialog1.Filter = "CFG files (*.cfg)|*.cfg";
            SaveFileDialog1.FilterIndex = 1;
            SaveFileDialog1.RestoreDirectory = true;

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_portConfigFile = new PortConfigFile(Config.Resource.Resource.DefaultCfg);
                m_portConfigFile.SaveFile(SaveFileDialog1.FileName);
                if (tvPortInfo.Nodes.Count > 0)
                    tvPortInfo.Nodes.Clear();
                SetPortConfigDisplay();
            }

        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (m_portConfigFile != null)
            {
                if (m_portConfigFile.FileName != null)
                    m_portConfigFile.SaveFile();
                else
                    tsSaveAs_Click(sender, e);
            }
        }

        private void tsSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

            SaveFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath + "\\configfile";
            SaveFileDialog1.Filter = "CFG files (*.cfg)|*.cfg";
            SaveFileDialog1.FilterIndex = 1;
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.Title = "Save file";

            if (m_portConfigFile != null && SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Savefile(SaveFileDialog1.FileName);
            }
        }

        private void tsDownLoad_Click(object sender, EventArgs e)
        {
            if (m_portConfigFile != null)
            {
                Savefile(SourceFile);

                if (m_portConfigFile.Errors == Config.EditType.EDIT_NONE)
                {
                    if (DownLoadFile())
                    {
                        tsPB.Maximum = tsPB.Value = 0;
                        tsPB.Visible = true;
                        tsDownLoad.Enabled = false;
                        tsReadBack.Enabled = false;
                        tsCommand.Enabled = false;
                        tsReboot.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Exists errors!");
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
                tsCommand.Enabled = false;
                tsReboot.Enabled = false;
            }
        }

        private void tsCommand_Click(object sender, EventArgs e)
        {
            CommandForm frm = new CommandForm(this);
            try
            {
                frm.ShowDialog(this);
            }
            finally
            {
                frm.Dispose();
            }
        }

        private void tsConnect_Click(object sender, EventArgs e)
        {
            ConnectTarget();
            SetConnectStateDisplay();
        }


        private void tsReboot_Click(object sender, EventArgs e)
        {
            RebootTarget(TargetFileType.CORE);
        }

        private void tvPortInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                ShowContent(((ConfigNode[])e.Node.Tag)[0]);
            }
            else
            {
                ShowAllContent();
            }

        }

        private void tvPortInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Form frm;
            {
                if (tvPortInfo.SelectedNode != null)
                {
                    if (tvPortInfo.SelectedNode.Tag != null && typeof(PortConfig) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType()) //(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortConfig)
                    {
                        frm = new PortConfigEditForm((PortConfig[])tvPortInfo.SelectedNode.Tag);
                        try
                        {
                            frm.ShowDialog(this);
                        }
                        finally
                        {
                            frm.Dispose();
                        }

                    }
                    else if (tvPortInfo.SelectedNode.Tag != null && typeof(PortCommands) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType())//(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortCommands)
                    {
                        frm = new PortCommandsEditForm((PortCommands[])tvPortInfo.SelectedNode.Tag); ;
                        try
                        {
                            frm.ShowDialog(this);
                        }
                        finally
                        {
                            frm.Dispose();
                        }

                    }
                }
            }
        }

        private void tsmiPortConfigConfigure_Click(object sender, EventArgs e)
        {
            Form frm;
            {
                if (tvPortInfo.SelectedNode != null)
                {
                    if (tvPortInfo.SelectedNode.Tag != null && typeof(PortConfig) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType()) //(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortConfig)
                    {
                        frm = new PortConfigEditForm((PortConfig[])tvPortInfo.SelectedNode.Tag);
                        try
                        {
                            frm.ShowDialog(this);
                        }
                        finally
                        {
                            frm.Dispose();
                        }

                    }
                    else if (tvPortInfo.SelectedNode.Tag != null && typeof(PortCommands) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType())//(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortCommands)
                    {
                        frm = new PortCommandsEditForm((PortCommands[])tvPortInfo.SelectedNode.Tag); ;
                        try
                        {
                            frm.ShowDialog(this);
                        }
                        finally
                        {
                            frm.Dispose();
                        }

                    }
                }
            }
        }

        private void tsmiPortConfigCopy_Click(object sender, EventArgs e)
        {
            if (tvPortInfo.SelectedNode != null)
            {
                if (tvPortInfo.SelectedNode.Tag != null)
                {
                    m_copyNode = ((ConfigNode[])tvPortInfo.SelectedNode.Tag)[0];
                }
            }
        }

        private void tsmiPortConfigPaste_Click(object sender, EventArgs e)
        {
            if (tvPortInfo.SelectedNode != null)
            {
                if (tvPortInfo.SelectedNode.Tag != null && m_copyNode != null && ((ConfigNode[])tvPortInfo.SelectedNode.Tag)[0].m_type == m_copyNode.m_type)
                {
                    ((ConfigNode[])tvPortInfo.SelectedNode.Tag)[0].Update(m_copyNode.CreateDataTable());
                    ShowContent(((ConfigNode[])tvPortInfo.SelectedNode.Tag)[0]);
                }
            }
        }

        private void tvPortInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvPortInfo.SelectedNode = tvPortInfo.GetNodeAt(e.X, e.Y);
            }

        }
        private void MainForm_GotoNextError()
        {

        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (tvPortInfo.SelectedNode != null)
            {
                if (tvPortInfo.SelectedNode.Tag != null && typeof(PortConfig) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType())//(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortConfig)
                {
                    PortConfig[] portConfig = (PortConfig[])tvPortInfo.SelectedNode.Tag;
                    ShowContent(portConfig[0]);
                }
                else if (tvPortInfo.SelectedNode.Tag != null && typeof(PortCommands) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType())//(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortCommands)
                {
                    PortCommands[] portCommands = (PortCommands[])tvPortInfo.SelectedNode.Tag;
                    ShowContent(portCommands[0]);
                }
                else
                {
                    ShowAllContent();
                }
            }

        }

        private void Openfile(string fileName)
        {
            m_portConfigFile = new PortConfigFile(fileName);
            if (tvPortInfo.Nodes.Count > 0)
                tvPortInfo.Nodes.Clear();
            SetPortConfigDisplay();
            if (m_portConfigFile.Errors != Config.EditType.EDIT_NONE)
            {
                MessageBox.Show("Configuration file errors!");
            }
        }

        private void Savefile(string fileName)
        {
            m_portConfigFile.SaveFile(fileName);
            this.Text = "ConfigTool  -" + m_portConfigFile.FileName;
            if (m_portConfigFile.Errors != Config.EditType.EDIT_NONE)
            {
                MessageBox.Show("Configuration file errors!");
            }
        }

        private void cmsPortConfig_Opening(object sender, CancelEventArgs e)
        {
            if (tvPortInfo.SelectedNode != null)
            {
                if (tvPortInfo.SelectedNode.Tag != null && m_copyNode != null && ((ConfigNode[])tvPortInfo.SelectedNode.Tag)[0].m_type == m_copyNode.m_type)
                {
                    tsmiPortConfigPaste.Enabled = true;
                }
                else
                {
                    tsmiPortConfigPaste.Enabled = false;
                }
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
                tsCommand.Enabled = true;
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
            tsCommand.Enabled = false;
            tsReboot.Enabled = false;
        }

    }

}