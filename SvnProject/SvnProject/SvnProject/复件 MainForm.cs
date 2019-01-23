using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;



namespace SvnProject
{
        public enum DisplayType{
                NAME,
                VERNO,
    };

    public partial class MainForm : Form
    {
        const string LogSeparators = "------------------------------------------------------------------------\r\n";
        string[] m_depositoryList;
        string[] m_logList;
        ArrayList m_nameList = new ArrayList();
        //ArrayList m_vernoList = new ArrayList();
        ArrayList m_projectList = new ArrayList();
        ArrayList m_customerList = new ArrayList(); 
        int[] m_nameID;
        //int[] m_vernoID;
        int[] m_projectID;
        int[] m_customerID;
        int[] m_depositoryOffset;
        int[] m_depositoryAddr;
        public DisplayType m_type = SvnProject.DisplayType.NAME;
        bool m_loaded = false;
        public MainForm()
        {
            InitializeComponent();
            FileStream fs;
            string filename = System.Windows.Forms.Application.StartupPath + "\\depository.lis";
            if (!File.Exists(filename))
            {
                File.WriteAllBytes(filename, SvnProject.Properties.Resources.depository);
            }
            using (fs = File.OpenRead(filename)) 
                {
                    byte[] b = new byte[fs.Length];
                    if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                    {
                        UTF8Encoding temp = new UTF8Encoding(true);
                        string content = temp.GetString(b);
                        char[] charSeparators = new char[] { '\n', '\r' };
                        m_depositoryList = content.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        m_depositoryOffset = new int[m_depositoryList.Length+1];
                        //for (int i = 0; i < m_depositoryList.Length; i++)
                        {
                            //tscbProject.Items.Add(m_depositoryList[i].Substring(0, m_depositoryList[i].IndexOf('/', 16)));
                        }
                        //tscbProject.Items.Add("ALL");
                    }
                }


            //tscbProject.Select(0);
            //tscbProject.Items.Clear();
            //Array.Sort(ports);
            //tscbProject.Items.AddRange(ports);
        }
        

        private void SetVernoTreeDisplay()
        {
 
            if(m_loaded)
            {
            if (tvVernoInfo.Nodes.Count > 0)
                tvVernoInfo.Nodes.Clear();
            tvVernoInfo.BeginUpdate();
            tvVernoInfo.Nodes.Add("Name");
            //tvPortInfo.Nodes[0].ContextMenuStrip = cmsPortConfig;
            if(m_type == SvnProject.DisplayType.NAME)
            {
            for (int i = 0; i < m_nameList.Count; i++)
            {
                tvVernoInfo.Nodes[0].Nodes.Add((string)m_nameList[i]);
                tvVernoInfo.Nodes[0].Nodes[i].Tag = i;
                //bool[] m_vernoIDMark = new bool[m_vernoList.Count];
                bool[] m_projectIDMark = new bool[m_projectList.Count];
                int index_p=0;
                for (int j = 0; j < m_logList.Length; j++)
                { 
                    if (m_nameID[j] == i && !m_projectIDMark[m_projectID[j]])
                    {
                        tvVernoInfo.Nodes[0].Nodes[i].Nodes.Add((string)m_projectList[m_projectID[j]]);
                        tvVernoInfo.Nodes[0].Nodes[i].Nodes[index_p].Tag = m_projectID[j];
                        m_projectIDMark[m_projectID[j]] = true;
                        bool[] m_customerIDMark = new bool[m_customerList.Count];
                        int index_c=0;
                        for (int k = 0; k < m_logList.Length; k++)
                        {
                            if (m_nameID[k] == i && m_projectID[k] == m_projectID[j] && !m_customerIDMark[m_customerID[k]])
                            {
                                tvVernoInfo.Nodes[0].Nodes[i].Nodes[index_p].Nodes.Add((string)m_customerList[m_customerID[k]]);
                                tvVernoInfo.Nodes[0].Nodes[i].Nodes[index_p].Nodes[index_c].Tag = m_customerID[k];
                                m_customerIDMark[m_customerID[k]] = true;
                                index_c ++;
                             }
                        }
                        index_p ++;
                     }
                }
            }
            }
            else
            {
                for (int i = 0; i < m_projectList.Count; i++)
                {
                tvVernoInfo.Nodes[0].Nodes.Add((string)m_projectList[i]);
                tvVernoInfo.Nodes[0].Nodes[i].Tag = i;
                bool[] m_customerIDMark = new bool[m_customerList.Count];
                int index_c=0;
                for (int j = 0; j < m_logList.Length; j++)
                {
                    if (m_projectID[j] == i && !m_customerIDMark[m_customerID[j]])
                    {
                        tvVernoInfo.Nodes[0].Nodes[i].Nodes.Add((string)m_customerList[m_customerID[j]]);
                        tvVernoInfo.Nodes[0].Nodes[i].Nodes[index_c].Tag = m_customerID[j];
                        m_customerIDMark[m_customerID[j]] = true;
                        bool[] m_nameIDMark = new bool[m_nameList.Count];
                        int index_n=0;
                        for (int k = 0; k < m_logList.Length; k++)
                        {
                            if (m_projectID[k] == i && m_customerID[k] == m_customerID[j] && !m_nameIDMark[m_nameID[k]])
                            {
                                tvVernoInfo.Nodes[0].Nodes[i].Nodes[index_c].Nodes.Add((string)m_nameList[m_nameID[k]]);
                                tvVernoInfo.Nodes[0].Nodes[i].Nodes[index_c].Nodes[index_n].Tag = m_nameID[k];
                                m_nameIDMark[m_nameID[k]] = true;
                                index_n ++;
                             }
                        }
                        index_c ++;
                     }
                }
            }

            }
            tvVernoInfo.Nodes[0].Expand();
            tvVernoInfo.EndUpdate();
            
            }
         }

        private void tsUpdate_Click(object sender, EventArgs e)
        {

        }

        private void tsLoad_Click(object sender, EventArgs e)
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

            p.StandardInput.WriteLine("svn log --username yulin.wang --password 1qa2ws "+m_depositoryList[0] + " >tmp.txt");

            for (int i = 1; i < m_depositoryList.Length; i++)
            {
                p.StandardInput.WriteLine("svn log --username yulin.wang --password 1qa2ws  " + m_depositoryList[i] + " >>tmp.txt");
            }
            Thread.Sleep(500);

            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            p.Close();

                ;
                //FileStream fs;
                
                using (StreamReader fs = new StreamReader("tmp.txt", System.Text.Encoding.Default, false)) 
                {
                    //byte[] b = new byte[fs.Length];
                    //if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                    {
                        //UTF8Encoding temp = new UTF8Encoding(true);
                        int offset =0;
                        int addrId =0;
                        string content = fs.ReadToEnd(); //temp.GetString(b);
                        string[] stringSeparators = new string[] { LogSeparators };
                        m_depositoryOffset[0] = 0;
                        m_depositoryOffset[m_depositoryList.Length] = content.Length;
                        for (int q = 1; q < m_depositoryList.Length; q++)
                        {
                            m_depositoryOffset[q] = content.IndexOf(LogSeparators + LogSeparators, m_depositoryOffset[q-1]) + LogSeparators.Length;
                        }
                        m_logList = content.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        
                        m_nameID = new int[m_logList.Length];
                        //m_vernoID = new int[m_logList.Length];
                        m_projectID = new int[m_logList.Length];
                        m_customerID = new int[m_logList.Length];
                        m_depositoryAddr = new int[m_logList.Length];


                        for(int i=0; i< m_logList.Length; i++)
                        {
                            //m_nameID[i] = m_vernoID[i] = -1;

                            m_depositoryAddr[i] = m_nameID[i] = m_projectID[i] =m_customerID[i] = -1;
                            char[] charSeparators = new char[]{'|'};
                            string[] log_info =(m_logList[i].Substring(0,m_logList[i].IndexOf('\n'))).Split(charSeparators, StringSplitOptions.RemoveEmptyEntries) ;
                            if(log_info.Length >= 3)
                            {
                                char[] msgCharS = new char[]{'=',' ','\r','\n'};
                                string[] log_msg = (m_logList[i].Substring(m_logList[i].IndexOf('\n')+1)).Split(msgCharS, StringSplitOptions.RemoveEmptyEntries) ;
                                
                                for(int j=0; j< log_msg.Length; j++)
                                {
                                    
                                    string[] verno = log_msg[j].Split('_');
                                    if(verno.Length>2)
                                    {
                                        int index = m_nameList.IndexOf( log_info[1]);
                                        if (index == -1) 
                                        { 
                                             index =  m_nameList.Add( log_info[1]); 
                                        }
                                        m_nameID[i] = index;

                                        index = m_projectList.IndexOf( verno[0] );
                                        if (index == -1) 
                                        { 
                                             index =  m_projectList.Add( verno[0] ); 
                                        }
                                        m_projectID[i] = index;

                                        index = m_customerList.IndexOf( verno[1] );
                                        if (index == -1) 
                                        { 
                                             index =  m_customerList.Add( verno[1] ); 
                                        }
                                        m_customerID[i] = index;

                                        //index = m_vernoList.IndexOf(verno[0] +"_"+ verno[1]);
                                        //if   (m_vernoList.IndexOf( verno[0]  +"_"+ verno[1]) == -1) 
                                        //{ 
                                        //      index = m_vernoList.Add( verno[0]  +"_"+ verno[1]); 
                                        //}

                                        //m_vernoID[i] = index;
                                        m_depositoryAddr[i] = addrId;
                                    }
                                }

                            }
                            offset +=m_logList[i].Length+ LogSeparators.Length;
                            if(offset+ LogSeparators.Length>= m_depositoryOffset[addrId+1])
                            {
                                offset +=LogSeparators.Length;
                                addrId ++;
                            }

                        }
                        m_loaded = true;
                        SetVernoTreeDisplay();
                    }
                }


        }

        private void tscbProject_TextChanged(object sender, EventArgs e)
        {
            if(tscbProject.Text == "project")
            {
                m_type = SvnProject.DisplayType.VERNO;
            }
            else
            {
                m_type = SvnProject.DisplayType.NAME;
            }
            
            SetVernoTreeDisplay();
        }

        private void tvVernoInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //tvVernoInfo.ind
            //if (e.Node.Tag != null && e.Node.Tag.GetType() == typeof(xValveData))
            {
                //SetValveId(e.Node.Parent.Index,e.Node.Index);
                //UpdateDisplay();
            }
            int nameId;
            int projectID;
            int customerID;
            
            if ((tvVernoInfo.SelectedNode.FullPath.Split('\\')).Length == 4)
            {
                if (m_type == SvnProject.DisplayType.NAME)
                {
                    nameId = (int)e.Node.Parent.Parent.Tag;
                    projectID = (int)e.Node.Parent.Tag;
                    customerID = (int)e.Node.Tag;
                }
                else
                {
                    projectID = (int)e.Node.Parent.Parent.Tag;
                    customerID = (int)e.Node.Parent.Tag;
                    nameId = (int)e.Node.Tag;
                }
                rtbVernoInfo.Text = LogSeparators;
                for (int j = 0; j < m_logList.Length; j++)
                {
                    //if (m_vernoID[j] == vernoID)
                    if (m_nameID[j] == nameId && m_projectID[j]  == projectID && m_customerID[j]== customerID)
                    {
                        rtbVernoInfo.Text += m_logList[j] + LogSeparators;
                    }
                }
            }
            else if ((tvVernoInfo.SelectedNode.FullPath.Split('\\')).Length == 3)
            {
                if (m_type == SvnProject.DisplayType.VERNO)
                {
                    projectID = (int)e.Node.Parent.Tag;
                    customerID = (int)e.Node.Tag;
                rtbVernoInfo.Text = LogSeparators;
                for (int j = 0; j < m_logList.Length; j++)
                {
                    //if (m_vernoID[j] == vernoID)
                    if ( m_projectID[j]  == projectID && m_customerID[j]== customerID)
                    {
                        string item = m_depositoryList[m_depositoryAddr[j]].Substring(0, m_depositoryList[m_depositoryAddr[j]].IndexOf('/', 16));
                        if(rtbVernoInfo.Text.IndexOf(item) == -1)
                        {
                            rtbVernoInfo.Text += item + "\r\n" + LogSeparators;
                        }
                    }
                }     
                
                }
            }
        }

    }
}
