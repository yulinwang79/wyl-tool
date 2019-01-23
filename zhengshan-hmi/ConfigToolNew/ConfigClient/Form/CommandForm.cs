using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LYC.Common.Tcp;
using System.Threading;
using System.IO.Ports;
using LYC.Common;
using LYC.Common.Serial;

namespace Config
{
    
    public partial class CommandForm : Form
    {
        MainForm m_mainform;
        int m_addr;
        //System.IO.StreamWriter m_sw;
        delegate void SetTextCallback(string text);
        //Command Client
        //private TextCmdClient m_cmdClient = null;

        public atCommand m_atc;
        public SerialPortData m_spPort;

        public CommandForm(MainForm mainform)
        {
            m_mainform = mainform;
            m_atc = mainform.m_atc;
            m_spPort = mainform.m_spPort;
            //m_cmdClient = mainform.m_cmdClient;
            InitializeComponent();
            m_atc.ReceivedEventHandle = OnReceivedCommand;
            //try
            //{
                //if(m_mainform.IsNetworkConnected())
                //{
                    //m_cmdClient = new TextCmdClient();
                    
                    //m_cmdClient.Host = m_mainform.m_address;// "localhost";
                    
                    //m_cmdClient.Port = MainForm.COMMANDS_PORT;
                    
                    //m_cmdClient.OnReceivedEventHandle = m_atc.ReceivedFunc;

                    //m_atc.SendCmd = m_cmdClient.Send;

                    //m_atc.ReceivedEventHandle = OnReceivedCommand;
                    
                    //m_cmdClient.Start();
                //}
                //else
                //{
                    //m_atc.SendCmd = m_spPort.SendData;

                    //m_spPort.ReceivedDataHandle = m_atc.ReceivedFunc;
                    //m_atc.ReceivedEventHandle = OnReceivedCommand;
                    //m_mainform.m_spPort.ReceivedDataHandle = OnReceivedCommand;
                //}
                //m_sw = new System.IO.StreamWriter(System.Windows.Forms.Application.StartupPath + "\\Debug" + System.DateTime.Now.ToShortDateString() + ".log", true, Encoding.ASCII);
            //}
            //catch
            //{
            //    MessageBox.Show("Connect Error!");
            //    this.Close();
            //}

        }

        private void CommandForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_atc.ReceivedEventHandle = null;
            m_atc.SendExit();
        }

        private void SendDataBaseOffsetCommand(int addr)
        {
            m_addr = addr;
            m_atc.SendReadDb(addr.ToString(),cbLoop.Checked);
        }
       
        private void btStart_Click(object sender, EventArgs e)
        {
            if (tbSend.Text.Length > 0)
            {
                if (tbSend.Text.Trim().IndexOf("PORT=") == 0)
                {
                    m_atc.SendLog(tbSend.Text,"FRAME");
                }
                else
                {
                    m_atc.SendLog(tbSend.Text);
                }
                btStop.Enabled = true;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            lbRecived.Items.Clear();
        }

        private void tbSend_Enter(object sender, EventArgs e)
        {
            btStart_Click(sender,e);
        }

        private void cbPort_CheckedChanged(object sender, EventArgs e)
        {
            CommandChanged();
        }
        
        private void cbLoop_CheckedChanged(object sender, EventArgs e)
        {
            //CommandChanged();
        }

        private void tbSend_TextChanged(object sender, EventArgs e)
        {
            if (tbSend.Text.Trim().Length > 0)
            {
                btStart.Enabled = true;
            }
            else
            {
                btStart.Enabled = false;
            }
        }
 
        private void btStop_Click(object sender, EventArgs e)
        {
            btStop.Enabled = false;
            
            m_atc.SendLogc();
        }

        private void btFresh_Click(object sender, EventArgs e)
        {
            if (tbAddr.Text.Length > 0)
            {
                int addr = Int32.Parse(tbAddr.Text);
                tbAddr.Text = addr.ToString();
                if (addr > 20000)
                {
                    MessageBox.Show("DB Offset too large!");
                    return;
                }
                SendDataBaseOffsetCommand(addr);
            }
        }

        private void btPrevPage_Click(object sender, EventArgs e)
        {
            SendDataBaseOffsetCommand((m_addr - 100) < 0 ? 0 : (m_addr - 100));
        }

        private void btNextPage_Click(object sender, EventArgs e)
        {
            SendDataBaseOffsetCommand((m_addr + 100) > 20000 ? 20000 : (m_addr + 100));
        }

        private void CommandChanged()
        {
            CheckBox cbox;
            int i = 0;
            string sPort = "";
            tbSend.Text = "";
            for (i = 0; i < gbPort.Controls.Count; i++)
            {
                if (gbPort.Controls[i].GetType() == cbG1A.GetType())
                {
                    //... 
                    cbox = (CheckBox)gbPort.Controls[i];
                    if (cbox.Checked)
                    {
                        if (sPort.Length > 0)
                            sPort += "+";
                        sPort += cbox.Tag.ToString();
                    }
                }
            }

            if (sPort.Length > 0)
            {
                tbSend.Text += "PORT=" + sPort;
            }

        }        
        private void OnReceivedCommand(atResult result)
        {
            SetTextCallback d = new SetTextCallback(SetText);
            if(result.ResultType == LYC.Common.ATResultType.Message)
                this.Invoke(d, new object[] { result.ResultText});
        }  
/*
        private void OnReceivedCommand(object command)
        {
            SetTextCallback d = new SetTextCallback(SetText);
            this.Invoke(d, new object[] { command });
        }
        private void OnReceivedCommand(byte[] command)
        {
            SetTextCallback d = new SetTextCallback(SetText);
            UTF8Encoding temp = new UTF8Encoding(true);
            string content = temp.GetString(command);
            this.Invoke(d, new object[] { content });
        }
*/
        private void SetText(string text)
        {
            if (text != null && text.Length > 0)
            {
                lbRecived.BeginUpdate();
                if (text.IndexOf("Database Monitor:") != -1)
                {
                    lbRecived.Items.Clear();
                }
                if (lbRecived.Items.Count > 10000)
                {
                    lbRecived.Items.RemoveAt(0);
                }
                lbRecived.Items.Add(text);
                lbRecived.TopIndex = lbRecived.Items.Count - 1;
                lbRecived.EndUpdate();
            }
        }

        private void tbAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || ((int)e.KeyChar == (int)System.Windows.Forms.Keys.Back) ||
                (e.KeyChar == 3 || e.KeyChar==22)))//Ctrl C  & Ctrl V
            {
                e.Handled = true;
            }   
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbRecived.BeginUpdate();
            for (int i = 0; i < lbRecived.Items.Count; i++)
            {
                lbRecived.SetSelected(i,true);
            }
            lbRecived.EndUpdate();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbRecived.SelectedIndex != -1)
            {
                string tmp = "";
                for (int i = 0; i < lbRecived.Items.Count; i++)
                {
                    if (lbRecived.GetSelected(i))
                    {
                        tmp += lbRecived.Items[i].ToString()+ "\r\n";
                    }
                   
                }
                Clipboard.SetDataObject(tmp);
            }
        }
    }
}
