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

namespace LYC.Common
{
    public partial class FormBase : Form
    {
        public enum ConnectionType{
                    NONE,
                    NETWORK,
                    SERIAL_PORT,
        };
        public enum TargetFileType{
                    HMI,
                    CORE,
                    UNKNOWN
        };
        public const int TELNET_PORT = 23;
        public const int COMMANDS_PORT = 10001;
        public string m_address = null;
        public ConnectionType m_connection = ConnectionType.NONE;
        public SerialPortData m_spPort = new SerialPortData( );
        public TextCmdClient m_cmdClient = new TextCmdClient();
        public bool m_IsDownLoad= false;
        public bool m_pre_net = false;
        public atCommand m_atc = new atCommand();
        public string m_sourceFile;
        public string m_targetFile;
        public int m_blockSize = 128;
        delegate void ReceiveCallBack(atResult result);

        public string SourceFile
        {
            get { return m_sourceFile;}
            set { m_sourceFile = value;}
        }

        public string TargetFile
        {
            get { return m_targetFile;}
            set { m_targetFile = value;}
        }

        public int BlockSize
        {
            get { return m_blockSize;}
            set { m_blockSize = value;}
        }

        public FormBase()
        {
            InitializeComponent();
            m_spPort.ReceivedDataHandle = m_atc.ReceivedFunc;
            string homeDir;
            homeDir = System.Windows.Forms.Application.StartupPath + "\\configfile";
            DirectoryInfo dir = new DirectoryInfo(homeDir);
            dir.Create();
        }
        public bool IsConnected()
        {
            if(m_connection == ConnectionType.NETWORK)
            {
                {
                    return true;
                }
            }
            else
            {
                return m_spPort.IsOpen;
            }
        }
        public bool IsNetworkConnected()
        {
            if(m_connection == ConnectionType.NETWORK)
            {
                {
                    return true;
                }
            }
            return false;
        }

        protected bool DownLoadFile()
        {
            if (IsConnected()/*m_telnet.IsOpenConnection()*/)
            {
                if (MessageBox.Show("Sure you want to download?", "Download", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }
             
                if (File.Exists(SourceFile))
                {
                
                    m_atc.ReceivedEventHandle = this.FileTransferHandle;
                    m_atc.SetBlockMax(BlockSize);
                    m_atc.Putfile(SourceFile,TargetFile);
                    m_IsDownLoad = true;
                    
                    return true;
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
            return false;

        }
        
        protected bool ReadBackFile()
        {
            if (IsConnected()/*m_telnet.IsOpenConnection()*/)
            {
                m_atc.ReceivedEventHandle = this.FileTransferHandle;
                m_atc.Getfile(SourceFile,TargetFile);
                m_IsDownLoad = false;
                return true;
            }
            else
            {
                MessageBox.Show("Please connect!");
            }
            return false;
        }
		
        protected void ConnectTarget( )
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
                m_connection = ConnectionType.NONE;

                }
                else
                {

                    IPSettings dlg = new IPSettings();
                    if (m_connection == ConnectionType.NETWORK)
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
                                while(m_connection != ConnectionType.NETWORK )
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
                                        m_cmdClient.Stop();
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
                                    //#if Test
                                    m_atc.ReceivedEventHandle = ReceivedConnectReponse;
                                    m_atc.SendExit();
                                    int count =0;
                                    while(m_connection != ConnectionType.SERIAL_PORT)
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
                                    //#endif
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
                            m_connection = ConnectionType.SERIAL_PORT;

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
             
        }
        protected void RebootTarget(TargetFileType filetype)
	{
            if (IsConnected()/*m_telnet.IsOpenConnection()*/)
            {
                if (filetype == TargetFileType.HMI)
                {
                    if (MessageBox.Show("Sure you want to reboot hmi board?", "Reboot", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                    m_atc.ReceivedEventHandle = ReceivedRebootHandle;
                    m_atc.SendReboot("HMI");
                }
                else if (filetype == TargetFileType.CORE)
                {
                    if (MessageBox.Show("Sure you want to reboot core board?", "Reboot", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                    m_atc.ReceivedEventHandle = ReceivedRebootHandle;
                    m_atc.SendReboot("CORE");
                }
                else
                {
                    MessageBox.Show("Unknown file type!");
                }
            }
            else
            {
                MessageBox.Show("Please connect!");
            }	
	}
        protected bool isInner(long clientIp, long begin, long end)
        {
            return (clientIp >= begin) && (clientIp <= end);
        } 

        protected long getIpBaseNum(String ipSubnet,String ipAddress)
        {
            String[] ip = ipAddress.Split(new char[] { '.' });
            String[] subnet = ipSubnet.Split(new char[] { '.' });
            long a = (long.Parse(ip[0]) & long.Parse(subnet[0])) * 256 * 256 * 256;
            long b = (long.Parse(ip[1]) & long.Parse(subnet[1])) * 256 * 256;
            long c = (long.Parse(ip[2]) & long.Parse(subnet[2])) * 256;
            long d = (long.Parse(ip[3]) & long.Parse(subnet[3]));
            return a + b + c + d;
        }
    
            
        protected void ReceivedConnectReponse(atResult result)
        {
            if(result.ResultType == LYC.Common.ATResultType.Message && result.ResultText.Length >=2)
            {
             if((result.ResultText)[0] == 'O' && (result.ResultText)[1] == 'K')
             {
                if(m_pre_net)
                    m_connection = ConnectionType.NETWORK;
                else
                    m_connection = ConnectionType.SERIAL_PORT;
                
             }
            }
        }
        protected virtual void FileTransmittingCallback(atResult result)
        {
            return;
        }
        protected void FileTransmitting(atResult result)
        {
            if(result.ResultType == LYC.Common.ATResultType.Success)
            {
                if(m_IsDownLoad)
                {
                    MessageBox.Show("DownLoad Successed!");
                }
                else
                {
                    MessageBox.Show("ReadBack Successed!");
                }
            }
            
            else if(result.ResultType == LYC.Common.ATResultType.Failure)
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
            else if(result.ResultType == LYC.Common.ATResultType.TimeOut)
            {
                MessageBox.Show("Time Out!");
            }
            FileTransmittingCallback(result);
        }
        public void FileTransferHandle(object data)
        {
             ReceiveCallBack d = new ReceiveCallBack(FileTransmitting);
             this.Invoke(d, new object[] { data });
        }
        
        protected virtual void RebootingCallback( )
        {
            return;
        }

        protected void Rebooting(atResult result)
        {

            if(result.ResultType == LYC.Common.ATResultType.Message && result.ResultText.Length >=2)
            {
            if (result.ResultText.IndexOf("OK")!=-1)
            {
                    RebootingCallback();
                    if (m_spPort.IsOpen)
                    {
                        m_spPort.Close();
                    }
                    if (m_cmdClient.IsRun)
                    {
                        m_cmdClient.Stop();
                    }
                m_connection = ConnectionType.NONE;
            }
            }
        }      
        protected void ReceivedRebootHandle(object data)
        {
             ReceiveCallBack d = new ReceiveCallBack(Rebooting);
             this.Invoke(d, new object[] { data });
        }
        }
}
