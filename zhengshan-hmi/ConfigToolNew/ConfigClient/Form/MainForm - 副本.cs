//#define Telnet_Enable

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
    public enum ConnectionType{
                NONE,
                NETWORK,
                SERIAL_PORT,
    };
    


    public partial class MainForm : Form
    {
        public const int TELNET_PORT = 23;
        public const int COMMANDS_PORT = 10001;
        private PortConfigFile m_portConfigFile;
        private readonly PortConfigFile m_portConfigFile_default;
        public string m_address = null;
        public string m_com = null;
        private ConfigNode m_copyNode;
        public ConnectionType m_connection = Config.ConnectionType.NONE;
        FtpServer m_ftpServer = new FtpServer();
        public SerialPortData m_spPort = new SerialPortData( );
        public TextCmdClient m_cmdClient = new TextCmdClient();
        const string MBCFG_FILENAME = "/core/disk/modbus.cfg";
        public bool m_IsDownLoad= false;
        private bool m_pre_net = false;
        //Telnet
#if Telnet_Enable
        private Terminal m_telnet=null;
#endif        
        public atCommand m_atc = new atCommand();
        delegate void ReceiveFileCB(int[] number);
        delegate void ReceiveMessage(string message);
        public MainForm()
        {
            InitializeComponent();
            m_portConfigFile_default = new PortConfigFile();
            SetConnectStateDisplay();
            m_spPort.ReceivedDataHandle = m_atc.ReceivedFunc;
            
            string ftpHomeDir;
            ftpHomeDir = System.Windows.Forms.Application.StartupPath + "\\configfile";
            DirectoryInfo dir = new DirectoryInfo(ftpHomeDir);
            dir.Create();
#if Telnet_Enable

            /*
             * 服务器的最大连接数
             */
            m_ftpServer.Capacity = 1000;

            /*
            * 连接超时时间
            */
            m_ftpServer.HeartBeatPeriod = 120000;  //120秒

            /*
              * 创建一个使用FTP的用户，
              */
            FtpUser user = new FtpUser("ftp");
            user.Password = "123";
            user.AllowWrite = true;
            //dir.Create();

            user.HomeDir = ftpHomeDir;

            /*
              * 限制该帐号的用户的连接服务器的最大连接数
             * 也就是限制该使用该帐号的FTP同时连接服务器的数量。
             */
            user.MaxConnectionCount = 2;

            /*
              * 限制用户的最大上传文件为20M，超过这个值上传文件会失败。
             * 默认不限制该值，可以传输大文件。
             */
            user.MaxUploadFileLength = 1024 * 1024 * 20;
            m_ftpServer.AddUser(user);

            //把当前目录作为匿名用户的目录，测试目的(必须指定)
            m_ftpServer.AnonymousUser.HomeDir = System.Windows.Forms.Application.StartupPath + "\\configfile";
#endif            
        }
        public bool IsConnected()
        {
            if(m_connection == Config.ConnectionType.NETWORK)
            {
#if Telnet_Enable
                if (m_telnet!= null && m_telnet.IsOpenConnection())
#endif
                {
                    return true;
                }
            }
            else
            {
                return m_spPort.IsOpen;
            }
#if Telnet_Enable
            return false;
#endif
        }
        public bool IsNetworkConnected()
        {
            if(m_connection == Config.ConnectionType.NETWORK)
            {
#if Telnet_Enable
                if (m_telnet!= null && m_telnet.IsOpenConnection())
#endif
                {
                    return true;
                }
            }
            return false;
        }
        private void SetConnectStateDisplay()
        {
            if(m_connection == Config.ConnectionType.NETWORK)
            {
                tsslConnection.Text = "Connection:Network--";
            }
            else if(m_connection == Config.ConnectionType.SERIAL_PORT)
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
                    
                    PortConfig[ ] currentPortConfig = new PortConfig[]{(PortConfig)m_portConfigFile.m_portConfigArray[i],
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
                    PortConfig[ ] currentPortConfig = new PortConfig[]{(PortConfig)m_portConfigFile.m_pcPortConfigArray[i],
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
                    PortConfig[ ] currentEtherNet = new PortConfig[]{(PortConfig)m_portConfigFile.m_etherNetPortArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_etherNetPortArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Tag = currentEtherNet;

                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes.Add(((PortConfig)(m_portConfigFile.m_etherNetClientConfigArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[0].ContextMenuStrip = cmsPortConfig;
                    PortConfig[ ] curEtherNetClientCfg = new PortConfig[]{(PortConfig)m_portConfigFile.m_etherNetClientConfigArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_etherNetClientConfigArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[0].Tag = curEtherNetClientCfg;

                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes.Add(((PortCommands)(m_portConfigFile.m_etherNetClientCommandsArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[1].ContextMenuStrip = cmsPortConfig;
                    PortCommands[ ] curEtherNetClientCmd = new PortCommands[]{(PortCommands)m_portConfigFile.m_etherNetClientCommandsArray[i],
                                                                                                        (PortCommands)m_portConfigFile_default.m_etherNetClientCommandsArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[1].Tag = curEtherNetClientCmd;


                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes.Add(((PortConfig)(m_portConfigFile.m_etherNetServerConfigArray[i])).GetName());
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[2].ContextMenuStrip = cmsPortConfig;
                    PortConfig[ ] curEtherNetServerCfg = new PortConfig[]{(PortConfig)m_portConfigFile.m_etherNetServerConfigArray[i],
                                                                                                        (PortConfig)m_portConfigFile_default.m_etherNetServerConfigArray[i] };
                    tvPortInfo.Nodes[0].Nodes[node_id].Nodes[i].Nodes[2].Tag = curEtherNetServerCfg;

                }
                node_id++;
            }
            
            tvPortInfo.Nodes[0].Nodes.Add(m_portConfigFile.m_dataRemap.GetName());
            tvPortInfo.Nodes[0].Nodes[node_id].ContextMenuStrip = cmsPortConfig;
            PortCommands[ ] curDataRemap= new PortCommands[]{(PortCommands)m_portConfigFile.m_dataRemap,
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
            rtbPortInfo.Text = node.GetTextStringWithErrorInfo(OffsetArray,LengthArray); 
            for(int i=0; i < OffsetArray.Count; i++)
            {
                rtbPortInfo.Select((int)OffsetArray[i],(int)LengthArray[i]);
                //rtbPortInfo.SelectionColor = Color.Red;
                rtbPortInfo.SelectionBackColor = Color.Red;
            }
        }
        public void ShowAllContent()
        {
            ArrayList OffsetArray = new ArrayList();
            ArrayList LengthArray = new ArrayList();
            rtbPortInfo.Clear();
            rtbPortInfo.Text = m_portConfigFile.GetTextStringWithErrorInfo(OffsetArray,LengthArray); 
            for(int i=0; i < OffsetArray.Count; i++)
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
                m_portConfigFile = new PortConfigFile();
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
	
            if (m_portConfigFile!= null && SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Savefile(SaveFileDialog1.FileName);
            }
        }
        
        private void tsDownLoad_Click(object sender, EventArgs e)
        {
            if (IsConnected()/*m_telnet.IsOpenConnection()*/)
            {
                if (MessageBox.Show("Sure you want to download?", "Download", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
                string fileName = System.Windows.Forms.Application.StartupPath + "\\configfile" + "\\modbus.cfg";
                if (m_portConfigFile != null)
                {
                    Savefile(fileName);
                }
                
                if (File.Exists(fileName) && m_portConfigFile.Errors==Config.EditType.EDIT_NONE)
                {
                
#if Telnet_Enable
                    if(m_connection == Config.ConnectionType.NETWORK)
                    {
                        m_telnet.VirtualScreen.CleanScreen();
                        m_telnet.SendResponse("ftpget " + "-u ftp -p 123 " + GetIPAddress() + " modbus.cfg" + " modbus.cfg", true);
                        f = m_telnet.WaitForString("]$", false, 60);
                        if (f != null)
                        {
                            string outText = m_telnet.VirtualScreen.Hardcopy();
                            string[] item_value = outText.Split('\n');
                            if (item_value[1].IndexOf("]$") != -1)
                            {
                                m_atc.SendUpdateMbCfg();
                                MessageBox.Show("DownLoad successed!");
                                return;
                            }
                        }
                        MessageBox.Show("DownLoad failure!");
                    }
                    else
#endif
                    {
                        m_atc.ReceivedEventHandle = ReceivedFileChange;
                        m_atc.Putfile(fileName,MBCFG_FILENAME);
                        m_IsDownLoad = true;
                    }
                 }
                else
                {
                    MessageBox.Show("File does not exist!");
                }
            }
            else
            {
              MessageBox.Show("Please connect!");
            }

        }
        
        private void tsReadBack_Click(object sender, EventArgs e)
        {
            if (IsConnected()/*m_telnet.IsOpenConnection()*/)
            {
#if Telnet_Enable
                if(m_connection == Config.ConnectionType.NETWORK)
                {
                    string f = null;
                    // ftpput -v -u username -p password  ftp-server to-remote-file from-local-file
                    m_telnet.VirtualScreen.CleanScreen();
                    m_telnet.SendResponse("ftpput " + "-u ftp -p 123 " + GetIPAddress() + " modbus.cfg" + " modbus.cfg", true);
                    f = m_telnet.WaitForString("]$",false,60);
                    if (f != null)
                    {
                        string outText = m_telnet.VirtualScreen.Hardcopy();
                        string[] item_value = outText.Split('\n');
                        if (item_value[1].IndexOf("]$") != -1)
                        {
                            Openfile(System.Windows.Forms.Application.StartupPath + "\\configfile" + "\\modbus.cfg");
                            MessageBox.Show("ReadBack successed!");
                            return;
                        }
//
                    }

                MessageBox.Show("ReadBack failure!");
                }
                else
#endif
                {
                    m_atc.ReceivedEventHandle = ReceivedFileChange;
                    m_atc.Getfile(System.Windows.Forms.Application.StartupPath + "\\configfile" + "\\modbus.cfg",MBCFG_FILENAME);
                    m_IsDownLoad = false;
                }
            }
            else
            {
                MessageBox.Show("Please connect!");
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
            try
            {
                if(IsConnected())
                {
                    if (m_spPort.IsOpen)
                    {
                        m_spPort.Close();
                    }
                    if (m_cmdClient.IsRun)
                    {
                        m_cmdClient.Stop();
                    }
#if Telnet_Enable
                    if ((m_telnet != null && m_telnet.IsOpenConnection()) )
                    {
                        if (m_telnet != null && m_telnet.IsOpenConnection())
                        {
                            m_telnet.Close();
                        }
                    }
#endif
                m_connection = Config.ConnectionType.NONE;

                }
                else
                {

                    IPSettings dlg = new IPSettings();
                    if (m_connection == Config.ConnectionType.NETWORK)
                    {
                        dlg.rbNetwork.Checked = true;
                    }
                    else
                    {
                        dlg.rbSerialPort.Checked = true;
                    }
                    
                    if (m_address != null)
                        dlg.tIPAddress.Text = m_address;

                    if (dlg.ShowDialog() == DialogResult.OK)                
                    {

                        if(dlg.rbNetwork.Checked == true)
                        {
#if Telnet_Enable
                            if (dlg.tIPAddress.Text.Length < 7)
                            {
                                MessageBox.Show("Unknown host");
                                return;
                            }
                            connect_state = CmdPing(dlg.tIPAddress.Text);
                            if (connect_state != "Connect")
                            {
                                MessageBox.Show(connect_state);
                                return;
                            }
                            else
                            {
                                m_address = dlg.tIPAddress.Text;
                                //tsConnect.Enabled = true;
                                
                                //Startup ftp server
                                StartUpFtpServer(m_address);
                                
                                //Startup telnet
                                StartUpTelnet(m_address);
                                //StartUpTelnetXP(m_address);
                            }
                        m_connection = Config.ConnectionType.NETWORK;
  #else                          
                            try
                            {
                               //m_cmdClient = new TextCmdClient();
                               m_pre_net = true;
                               m_cmdClient.Host = dlg.tIPAddress.Text;// "localhost";
                               m_cmdClient.Port = COMMANDS_PORT;
                               m_cmdClient.OnReceivedEventHandle = m_atc.ReceivedFunc;
                               m_atc.SendCmd = m_cmdClient.Send;
                               m_cmdClient.Start();
                                Thread.Sleep(500);
                                m_atc.ReceivedEventHandle = ReceivedConnectReponse;
                                m_atc.SendExit();
                                int count =0;
                                while(m_connection != Config.ConnectionType.NETWORK )
                                {
                                    if(count < 5)
                                    {
                                        count ++;
                                        m_atc.SendExit();
                                        Thread.Sleep(800);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Connection timed out!");
                                        return;
                                    }
                                }
                             }
                            catch
                            {
                                MessageBox.Show("Failed to open network!");
                                return;
                            }
                            m_address = dlg.tIPAddress.Text;
#endif                 
                        }
                        else
                        {
                            m_pre_net = false;
                            m_atc.SendCmd = m_spPort.SendData;
                            m_spPort.ReceivedDataHandle = m_atc.ReceivedFunc;
                            
                            if(dlg.cbPort.Text != null && dlg.cbPort.Text.Length!=0)
                            {
                                try
                                {
                                    m_spPort.PortName = dlg.cbPort.Text;
                                    m_spPort.Open();
                                    Thread.Sleep(500);
                                    m_atc.ReceivedEventHandle = ReceivedConnectReponse;
                                    m_atc.SendExit();
                                    int count =0;
                                    while(m_connection != Config.ConnectionType.SERIAL_PORT)
                                    {
                                        if(count < 5)
                                        {
                                            count ++;
                                            m_atc.SendExit();
                                            Thread.Sleep(800);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Connection timed out!");
                                            if (m_spPort.IsOpen)
                                            {
                                                m_spPort.Close();
                                            }
                                            return;
                                        }
                                    }                                    
                                } 
                                catch
                                {
                                    MessageBox.Show("Failed to open serial port!");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please choose a serial port!");
                            }
                            m_connection = Config.ConnectionType.SERIAL_PORT;

                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception eeeee)
            {
                MessageBox.Show(eeeee.Message, "Application Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
             
            SetConnectStateDisplay();
        }

        private void tsSPConnect_Click(object sender, EventArgs e)
        {
            SearialPortSettings dlg = new SearialPortSettings();
            if (dlg.ShowDialog() == DialogResult.OK && dlg.cbPort.Text != null && dlg.cbPort.Text.Length!=0)
            {
                try
                {
                    if (m_spPort.IsOpen)
                    {
                        m_spPort.Close();
                    }
                    //else
                    {
                        m_spPort.PortName = dlg.cbPort.Text;
                        m_spPort.Open();
                    }
                }
                catch
                {
                    MessageBox.Show("Failed to open serial port!");
                }
            }
            else
            {
                MessageBox.Show("Please choose a serial port!");
            }
            SetConnectStateDisplay();
        }   

        private void tsReboot_Click(object sender, EventArgs e)
	{
            if (IsConnected()/*m_telnet.IsOpenConnection()*/)
            {
#if Telnet_Enable
                if(m_connection == Config.ConnectionType.NETWORK)
                {

                    m_telnet.SendResponse("reboot", true);
                    Thread.Sleep(2000);
                    m_telnet.Close();

                    SetConnectStateDisplay();
                }
                else
#endif
                {
                    m_atc.ReceivedEventHandle = ReceivedReboot;
                    
                    m_atc.SendReboot("CORE");
                }
            }
            else
            {
                MessageBox.Show("Please connect!");
            }	
	}

        private void tvPortInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag!=null)
            {
                //rtbPortInfo.Text = ((ConfigNode[])e.Node.Tag)[0].GetTextString();
                ShowContent(((ConfigNode[])e.Node.Tag)[0]);
            }
            else
            {
                //rtbPortInfo.Text = m_portConfigFile.GetTextString();
                ShowAllContent();
            }
 
        }
        
        private void tvPortInfo_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Form frm;
             {
                 if (tvPortInfo.SelectedNode!=null)
                 {
                     if (tvPortInfo.SelectedNode.Tag!= null && typeof(PortConfig) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType()) //(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortConfig)
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
                         frm = new PortCommandsEditForm((PortCommands[])tvPortInfo.SelectedNode.Tag);;
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
                    //rtbPortInfo.Text = ((ConfigNode[])tvPortInfo.SelectedNode.Tag)[0].GetTextString();
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

                    //rtbPortInfo.Text = portConfig[0].GetTextString();
                    
                    ShowContent(portConfig[0]);
                }
                else if (tvPortInfo.SelectedNode.Tag != null && typeof(PortCommands) == ((object[])tvPortInfo.SelectedNode.Tag)[0].GetType())//(tvPortInfo.SelectedNode.ContextMenuStrip == cmsPortCommands)
                {
                    PortCommands[] portCommands = (PortCommands[])tvPortInfo.SelectedNode.Tag;
                    //rtbPortInfo.Text = portCommands[0].GetTextString();
                    
                    ShowContent(portCommands[0]);
                }
                else
                {
                    //rtbPortInfo.Text = m_portConfigFile.GetTextString();
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
            if(m_portConfigFile.Errors!=Config.EditType.EDIT_NONE)
            {
                MessageBox.Show("Configuration file errors!");
            }
        }

        private void Savefile(string fileName)
        {
            m_portConfigFile.SaveFile(fileName);
            this.Text = "ConfigTool  -" + m_portConfigFile.FileName;
            if(m_portConfigFile.Errors!=Config.EditType.EDIT_NONE)
            {
                MessageBox.Show("Configuration file errors!");
            }
        }

        private bool isInner(long clientIp, long begin, long end)
        {
            return (clientIp >= begin) && (clientIp <= end);
        } 

        private long getIpBaseNum(String ipSubnet,String ipAddress)
        {
            String[] ip = ipAddress.Split(new char[] { '.' });
            String[] subnet = ipSubnet.Split(new char[] { '.' });
            long a = (long.Parse(ip[0]) & long.Parse(subnet[0])) * 256 * 256 * 256;
            long b = (long.Parse(ip[1]) & long.Parse(subnet[1])) * 256 * 256;
            long c = (long.Parse(ip[2]) & long.Parse(subnet[2])) * 256;
            long d = (long.Parse(ip[3]) & long.Parse(subnet[3]));
            return a + b + c + d;
        }

        private void StartUpFtpServer(string HostIP)
        {
            try
            {
                m_ftpServer.Stop();
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
                ManagementObjectCollection nics = query.Get();
                foreach (ManagementObject nic in nics)
                {
                    {
                        if (getIpBaseNum((nic["IPSubnet"] as String[])[0], (nic["IPAddress"] as String[])[0]) == getIpBaseNum((nic["IPSubnet"] as String[])[0], HostIP))
                        {
                            m_ftpServer.PasvIPSetting = (nic["IPAddress"] as String[])[0];
                            break;
                        }
                        else
                        {
                            m_ftpServer.PasvIPSetting = null;
                        }
                    }
                }
        
                m_ftpServer.Start();
                //Console.WriteLine("Press enter to exit...");
                //Console.ReadLine();

                //m_ftpServer.Stop();

            }
            catch (System.Exception e)
            {
                NetDebuger.PrintErrorMessage("FATAL ERROR:" + e.Message);
            }

        }
#if Telnet_Enable
        private bool StartUpTelnet(string HostIP)
        {
            string f = null;

            //using(TextCmdClient cmdClient = new TextCmdClient())
            //{
            //   cmdClient.Host = HostIP;// "localhost";
            //   cmdClient.Port = COMMANDS_PORT;
             //  cmdClient.Start();
              //  Thread.Sleep(500);
              //  m_atc.SendCmd = cmdClient.Send;
              //  m_atc.SendTelnet();
              //  Thread.Sleep(1000);
             //}
            m_atc.SendTelnet();
            Thread.Sleep(1000);

                //Startup telnet
                m_telnet = new Terminal(HostIP, TELNET_PORT, 20, 80, 40);
                m_telnet.Connect(); // physcial connection
                do
                {
                    f = m_telnet.WaitForString("Login");
                    if (f == null)
                    {
                        MessageBox.Show("No login possible");
                        break;
                    }
                    //Console.WriteLine(tn.VirtualScreen.Hardcopy().TrimEnd());
                    m_telnet.SendResponse("root", true);    // send username
                    f = m_telnet.WaitForString("Password");
                    if (f == null)
                    {
                        MessageBox.Show("No password prompt found");
                        break;
                    }
                    //Console.WriteLine(tn.VirtualScreen.Hardcopy().TrimEnd());
                    m_telnet.SendResponse("123456", true);  // send password 
                    f = m_telnet.WaitForString("]$");
                    if (f == null)
                    {
                        MessageBox.Show("No > prompt found");
                        break;
                    }
                } while (false);
                if (f == null)
                {
                    m_telnet.Close();
                }
                else
                {
                    return true;
                }

            return false;
        }

        private bool StartUpTelnetXP(string HostIP)
        {
            string f = null;
            //Startup telnet
            m_telnet = new Terminal(HostIP, TELNET_PORT, 20, 80, 40);
            m_telnet.Connect(); // physcial connection
            do
            {

                f = m_telnet.WaitForString("Login username");
                if (f == null)
                {
                    MessageBox.Show("No login possible");
                    break;
                }
                //Console.WriteLine(tn.VirtualScreen.Hardcopy().TrimEnd());
                m_telnet.SendResponse("Administrator", true);    // send username
                f = m_telnet.WaitForString("Login password");
                if (f == null)
                {
                    MessageBox.Show("No password prompt found");
                    break;
                }
                //Console.WriteLine(tn.VirtualScreen.Hardcopy().TrimEnd());
                m_telnet.SendResponse("12qw!@", true);  // send password 
                f = m_telnet.WaitForString("Domain name");
                if (f == null)
                {
                    MessageBox.Show("No domain name found");
                    break;
                } 
                m_telnet.SendResponse("MSHOME", true);  // send Domain name 

                f = m_telnet.WaitForString(">");
                if (f == null)
                {
                    MessageBox.Show("No > prompt found");
                    break;
                }
             } while (false);
            if (f == null)
            {
                m_telnet.Close();
                return false;
            }
            return true;
        }        
       
        private static string GetIPAddress()
        {

            System.Net.IPAddress addr;

            // 获得本机局域网IP地址

            addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);

            return addr.ToString();
        }

        private static string CmdPing(string strIp)
         {
              Process p = new Process();
              p.StartInfo.FileName = "cmd.exe";
              p.StartInfo.UseShellExecute = false;
              p.StartInfo.RedirectStandardInput = true;
              p.StartInfo.RedirectStandardOutput = true;
              p.StartInfo.RedirectStandardError = true;
              p.StartInfo.CreateNoWindow = true;
              string pingrst;
              p.Start();
              p.StandardInput.WriteLine("ping -n 1 "+strIp);
              p.StandardInput.WriteLine("exit");
              string strRst = p.StandardOutput.ReadToEnd();
              if (strRst.IndexOf("(0% ") != -1)//if (strRst.IndexOf("(0% loss)") != -1)
                  pingrst = "Connect";
              else if (strRst.IndexOf("Destination host unreachable.") != -1)
                  pingrst = "Destination host unreachable.";
              else if (strRst.IndexOf("Request timed out.") != -1)
                  pingrst = "Request timed out.";
              else if (strRst.IndexOf("Unknown host") != -1)
                  pingrst = "Unknown host";
              else
                  pingrst = strRst;
              p.Close();
              return pingrst;
         }
#endif
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
            
        private void ShowProgressBar(int[] number)
        {
            tsPB.Maximum = number[1]; 
            tsPB.Value = number[0];
            

            if (number[2]== 0 && tsPB.Visible == false)
            {
                tsPB.Visible = true;
                if(tsDownLoad.Enabled)
                    tsDownLoad.Enabled = false;
                if (tsReadBack.Enabled)
                    tsReadBack.Enabled = false;
                if (tsCommand.Enabled)
                    tsCommand.Enabled = false;
                if (tsReboot.Enabled)
                    tsReboot.Enabled = false;
                
            }
            else if(number[2]== 1)
            {
                tsPB.Visible = false;
                tsDownLoad.Enabled = true;
                tsReadBack.Enabled = true;
                tsCommand.Enabled = true;
                tsReboot.Enabled = true;
 
                if(m_IsDownLoad)
                {
                    m_atc.SendUpdateMbCfg();
                    MessageBox.Show("DownLoad Successed!");
                }
                else
                {
                    Openfile(System.Windows.Forms.Application.StartupPath + "\\configfile" + "\\modbus.cfg");
                    MessageBox.Show("ReadBack Successed!");
                }
            }
            else if(number[2]== 2)
            {
                if(m_IsDownLoad)
                {
                    MessageBox.Show("DownLoad Failure!");
                }
                else
                {
                    MessageBox.Show("ReadBack Failure!");
                }

            }
        }

        private void ReceivedConnectReponse(object data)
        {
             if(((string)data)[0] == 'O' && ((string)data)[1] == 'K')
             {
                if(m_pre_net)
                    m_connection = Config.ConnectionType.NETWORK;
                else
                    m_connection = Config.ConnectionType.SERIAL_PORT;
                
             }
        }
        
        private void ReceivedFileChange(object data)
        {
             ReceiveFileCB d = new ReceiveFileCB(ShowProgressBar);
             this.Invoke(d, new object[] { data });
        }

        private void Rebooted(string buf)
        {

            if (buf.IndexOf("OK")!=-1)
            {
                    tsDownLoad.Enabled = false;
                    tsReadBack.Enabled = false;
                    tsCommand.Enabled = false;
                    tsReboot.Enabled = false;
                    if (m_spPort.IsOpen)
                    {
                        m_spPort.Close();
                    }
                    if (m_cmdClient.IsRun)
                    {
                        m_cmdClient.Stop();
                    }
#if Telnet_Enable
                    if ((m_telnet != null && m_telnet.IsOpenConnection()) )
                    {
                        if (m_telnet != null && m_telnet.IsOpenConnection())
                        {
                            m_telnet.Close();
                        }
                    }
#endif
                m_connection = Config.ConnectionType.NONE;
            }
        }      
        private void ReceivedReboot(object data)
        {
             ReceiveMessage d = new ReceiveMessage(Rebooted);
             this.Invoke(d, new object[] { data });
        }

     }


}
