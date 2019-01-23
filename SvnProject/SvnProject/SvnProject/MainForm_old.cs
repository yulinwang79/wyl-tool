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
        string[] m_projectList;
        string[] m_logList;
        ArrayList m_nameList = new ArrayList();
        ArrayList m_vernoList = new ArrayList();
        ArrayList m_projectNameList = new ArrayList();
        ArrayList m_customersList = new ArrayList(); 
        int[] m_nameID;
        int[] m_vernoID;
        public DisplayType m_type = SvnProject.DisplayType.NAME;
        bool m_loaded = false;
        public MainForm()
        {
            InitializeComponent();
            FileStream fs;

            using (fs = File.OpenRead("project.lis")) 
            {
                byte[] b = new byte[fs.Length];
                if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                {
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string content = temp.GetString(b);
                    char[] charSeparators = new char[] { '\n', '\r' };
                    m_projectList = content.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    //for (int i = 0; i < m_projectList.Length; i++)
                    {
                        //tscbProject.Items.Add(m_projectList[i].Substring(0, m_projectList[i].IndexOf('/', 16)));
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
                bool[] m_vernoIDMark = new bool[m_vernoList.Count];
                int index=0;
                for (int j = 0; j < m_logList.Length; j++)
                {
                    if (m_nameID[j] == i && !m_vernoIDMark[m_vernoID[j]])
                    {
                        tvVernoInfo.Nodes[0].Nodes[i].Nodes.Add((string)m_vernoList[m_vernoID[j]]);
                        tvVernoInfo.Nodes[0].Nodes[i].Nodes[index++].Tag = m_vernoID[j];
                        
                        m_vernoIDMark[m_vernoID[j]] = true;
                     }
                }
            }
            }
            else
            {
                for (int i = 0; i < m_vernoList.Count; i++)
                {
                    tvVernoInfo.Nodes[0].Nodes.Add((string)m_vernoList[i]);
                    tvVernoInfo.Nodes[0].Nodes[i].Tag = i;
                    bool[] m_nameIDMark = new bool[m_nameList.Count];
                    int index=0;
                    for (int j = 0; j < m_logList.Length; j++)
                    {
                        if (m_vernoID[j] == i && !m_nameIDMark[m_nameID[j]])
                        {
                            tvVernoInfo.Nodes[0].Nodes[i].Nodes.Add((string)m_nameList[m_nameID[j]]);
                            tvVernoInfo.Nodes[0].Nodes[i].Nodes[index++].Tag = m_nameID[j];

                            m_nameIDMark[m_nameID[j]] = true;
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
            p.StandardInput.WriteLine("svn log "+m_projectList[0] + " >tmp.txt");
            for (int i = 1; i < m_projectList.Length; i++)
            {
                p.StandardInput.WriteLine("svn log " + m_projectList[i] + " >>tmp.txt");        
            }
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
                        string content = fs.ReadToEnd(); //temp.GetString(b);
                        string[] stringSeparators = new string[] { "------------------------------------------------------------------------\r\n" };
                        m_logList = content.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        
                        m_nameID = new int[m_logList.Length];
                        m_vernoID = new int[m_logList.Length];


                        for(int i=0; i< m_logList.Length; i++)
                        {
                            m_nameID[i] = m_vernoID[i] = -1;
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

                                        index = m_vernoList.IndexOf(verno[0] +"_"+ verno[1]);
                                        if   (m_vernoList.IndexOf( verno[0]  +"_"+ verno[1]) == -1) 
                                        { 
                                              index = m_vernoList.Add( verno[0]  +"_"+ verno[1]); 
                                        }

                                        m_vernoID[i] = index;
                                    }
                                }

                            }
                        }
                        m_loaded = true;
                        SetVernoTreeDisplay();
                    }
                }


        }

        private void tscbProject_TextChanged(object sender, EventArgs e)
        {
            if(tscbProject.Text == "verno")
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
            
            if ((tvVernoInfo.SelectedNode.FullPath.Split('\\')).Length == 3)
            {
                int nameId;
                int vernoID;
                if (m_type == SvnProject.DisplayType.NAME)
                {
                    nameId = (int)e.Node.Parent.Tag;
                    vernoID = (int)e.Node.Tag;
                }
                else
                {
                    vernoID = (int)e.Node.Parent.Tag;
                    nameId = (int)e.Node.Tag;
                }
                rtbVernoInfo.Text = "------------------------------------------------------------------------\r\n";
                for (int j = 0; j < m_logList.Length; j++)
                {
                    if (m_vernoID[j] == vernoID)
                    if (m_nameID[j] == nameId)
                    {
                        rtbVernoInfo.Text += m_logList[j] + "------------------------------------------------------------------------\r\n";
                    }
                }
            }
        }

    }
}
