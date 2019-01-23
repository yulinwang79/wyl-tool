#define RELEASE_LOAD
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
//using System.Web;
//using System.Web.UI;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;



namespace SvnProject
{
        public enum DisplayType{
                NAME,
                VERNO,
    };
        public enum DisplayTime{
                WITHIN_A_MONTH,
                WITHIN_TWO_MONTHS,
                ALL
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
        public DisplayType m_type = SvnProject.DisplayType.VERNO;
        public DisplayTime m_time = SvnProject.DisplayTime.ALL;
        bool m_loaded = false;
        TreeNode m_node;
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
#if RELEASE_LOAD
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
#endif                
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
            bool flag = false;
            rtbVernoInfo.Text = "";
            if (m_type == SvnProject.DisplayType.VERNO)
            {
                switch ((tvVernoInfo.SelectedNode.FullPath.Split('\\')).Length)
                {
                    case 4:
                    {
                        projectID = (int)e.Node.Parent.Parent.Tag;
                        customerID = (int)e.Node.Parent.Tag;
                        nameId = (int)e.Node.Tag;
                        
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
                    break;
                    
                    case 3:
                     {
                        projectID = (int)e.Node.Parent.Tag;
                        customerID = (int)e.Node.Tag;
                        rtbVernoInfo.Text = LogSeparators;
                        for (int j = 0; j < m_logList.Length; j++)
                        {
                            //if (m_vernoID[j] == vernoID)
                            if (m_projectID[j] == projectID && m_customerID[j] == customerID)
                            {
                                string item = m_depositoryList[m_depositoryAddr[j]].Substring(0, m_depositoryList[m_depositoryAddr[j]].IndexOf('/', 16));
                                if (rtbVernoInfo.Text.IndexOf(item) == -1)
                                {
                                    rtbVernoInfo.Text += item + "\r\n" + LogSeparators;
                                }
                            }
                        }

                        for (int i = 0; i < e.Node.GetNodeCount(false); i++)
                        {
                            nameId = (int)e.Node.Nodes[i].Tag;

                            for (int j = 0; j < m_logList.Length; j++)
                            {
                                //if (m_vernoID[j] == vernoID)
                                if (m_nameID[j] == nameId && m_projectID[j] == projectID && m_customerID[j] == customerID)
                                {
                                    rtbVernoInfo.Text += m_logList[j] + LogSeparators;
                                    break;
                                }
                            }
                        }
                    }                   
                    break;
                    case 2:
                    {
                        projectID = (int)e.Node.Tag;
                        for (int i = 0; i < e.Node.GetNodeCount(false); i++)
                        {
                            customerID = (int)e.Node.Nodes[i].Tag;

                            for (int j = 0; j < m_logList.Length; j++)
                            {
                                //if (m_vernoID[j] == vernoID)
                                if (m_projectID[j] == projectID && m_customerID[j] == customerID)
                                {
                                    rtbVernoInfo.Text += m_logList[j] + LogSeparators;
                                    break;
                                }
                            }
                        }
                    }                    
                    break;
                    default:
                    break;
                }
            }
            else
            {
                switch ((tvVernoInfo.SelectedNode.FullPath.Split('\\')).Length)
                {
                    case 4:
                    {
                        nameId = (int)e.Node.Parent.Parent.Tag;
                        projectID = (int)e.Node.Parent.Tag;
                        customerID = (int)e.Node.Tag;
                        
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
                    break;
                    
                    case 3:
                                        
                    break;
                    case 2:
                    {
                        for (int i = 0; i < e.Node.GetNodeCount(false); i++)
                        {
                            for (int j = 0; j < e.Node.Nodes[i].GetNodeCount(false); j++)
                            {
                                    rtbVernoInfo.Text +=e.Node.Nodes[i].Text+"_"+e.Node.Nodes[i].Nodes[j].Text+ "\r\n";
                            }
                        }
                    }                    
                                        
                    break;
                    
                    case 1:
                    {
                        flag = true;
                        //for (int i = 0; i < e.Node.GetNodeCount(false); i++)
                        //{
                        //    rtbVernoInfo.Text += LogSeparators;
                        //    rtbVernoInfo.Text +=e.Node.Nodes[i].Text+":\r\n";
                 
                        //    for (int j = 0; j < e.Node.Nodes[i].GetNodeCount(false); j++)
                        //    {
                        //        for (int k = 0; k < e.Node.Nodes[i].Nodes[j].GetNodeCount(false); k++)
                        //        {
                        //                rtbVernoInfo.Text +=e.Node.Nodes[i].Nodes[j].Text+"_"+e.Node.Nodes[i].Nodes[j].Nodes[k].Text+ "\r\n";
                        //        }
                        //    }
                        //}
                        m_node = e.Node;
                        DisplayTable();
                        
                    }                    
                                        
                    break;
                    default:
                    break;
                }
            }
            
            tscbDays.Visible = tsbExport.Visible=dgvName.Visible = flag;
        }
        public void DisplayTable( )
        {
            System.Data.DataTable dtPortCommards = new System.Data.DataTable("DataTable");
            //eg:
            //Enable                          : Disable
            string[] item_value = new string[m_node.GetNodeCount(false) * 2];
            for (int i = 0; i < m_node.GetNodeCount(false); i++)
            {
                dtPortCommards.Columns.Add(new DataColumn(m_node.Nodes[i].Text));
                dtPortCommards.Columns.Add(new DataColumn("Last time " + i));
                item_value[i] = " ";
            }
            for (int i = 0; i < m_node.GetNodeCount(false); i++)
            {
                int count = 0;
                for (int j = 0; j < m_node.Nodes[i].GetNodeCount(false); j++)
                {
                    for (int k = 0; k < m_node.Nodes[i].Nodes[j].GetNodeCount(false); k++)
                    {
                        TimeSpan   ts;
                        bool b_show = false;
                        if(m_time == SvnProject.DisplayTime.WITHIN_TWO_MONTHS)
                            ts=new   TimeSpan(60,0,0,0);
                        else if (m_time == SvnProject.DisplayTime.WITHIN_A_MONTH)
                            ts = new TimeSpan(30, 0, 0, 0);
                        else
                        {
                            ts = new TimeSpan(30, 0, 0, 0);
                            b_show = true;
                        }
                        DateTime dt1 = DateTime.Parse(GetLastTime((int)m_node.Nodes[i].Tag, (int)m_node.Nodes[i].Nodes[j].Tag, (int)m_node.Nodes[i].Nodes[j].Nodes[k].Tag)); 

                       if(!b_show)
                       {
                            DateTime   dt2=DateTime.Now.Subtract(ts);

                        if (DateTime.Compare(dt1, dt2) > 0)
                            b_show = true;
                        }
                        if(b_show)
                        {
                            for (int q = dtPortCommards.Rows.Count; q < count + 1; q++)
                            {

                                dtPortCommards.Rows.Add(item_value);
                            }
                            dtPortCommards.Rows[count][i * 2] = m_node.Nodes[i].Nodes[j].Text + "_" + m_node.Nodes[i].Nodes[j].Nodes[k].Text;
                            dtPortCommards.Rows[count][i * 2 + 1] =dt1.ToShortDateString();
                            count++;                             //rtbVernoInfo.Text +=node.Nodes[i].Nodes[j].Text+"_"+node.Nodes[i].Nodes[j].Nodes[k].Text+ "\r\n";
                        }
                    }
                }
            }
            DataSet dataset = (DataSet)(dgvName.DataSource);
            if (dataset != null)
                dataset.Dispose();
            dataset = new DataSet();
            dataset.Tables.Add(dtPortCommards);
            dgvName.DataSource = dataset;
            dgvName.DataMember = "DataTable";


        }

        public string GetLastTime(int nameId,int projectID, int customerID)
        {
            for (int j = 0; j < m_logList.Length; j++)
            {
                //if (m_vernoID[j] == vernoID)
                if (m_nameID[j] == nameId && m_projectID[j]  == projectID && m_customerID[j]== customerID)
                {
                    char[] charSeparators = new char[]{'|'};
                    string[] log_info =(m_logList[j].Substring(0,m_logList[j].IndexOf('\n'))).Split(charSeparators, StringSplitOptions.RemoveEmptyEntries) ;
                    if(log_info.Length >= 3)
                    {
                        char[] charSeparators1 = new char[]{' '};
                        string[] timer =log_info[2].Split(charSeparators1, StringSplitOptions.RemoveEmptyEntries) ;
                        return timer[0] + " " + timer[1];
                    }
                }
            }
            return null;
        }
        public void ToExcel1(DataGridView gridView)
        {
            //try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                
                saveFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                if (gridView.Rows.Count == 0)
                {
                    MessageBox.Show("没有数据可供导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    /*
                     * 获取保存EXCEL的路径
                     */
                    saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 0;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.CreatePrompt = true;
                    saveFileDialog.Title = "导出文件保存路径";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //strName储存保存EXCEL路径
                        string strName = saveFileDialog.FileName;
                        if (strName.Length != 0)
                        {
                            System.Reflection.Missing miss = System.Reflection.Missing.Value;
                            Microsoft.Office.Interop.Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                            excel.Application.Workbooks.Add(true); ;
                            //若是true，则在导出的时候会显示EXcel界面。
                            excel.Visible = false;
                            if (excel == null)
                            {
                                MessageBox.Show("EXCEL无法启动！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            Microsoft.Office.Interop.Excel.Workbooks books = (Microsoft.Office.Interop.Excel.Workbooks)excel.Workbooks;
                            Microsoft.Office.Interop.Excel.Workbook book = (Microsoft.Office.Interop.Excel.Workbook)(books.Add(miss));
                            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.ActiveSheet;
                            sheet.Name = "test";
                            //给进度条赋最大值，为gridView的行数                                             
                            //progressBar1.Maximum = gridView.RowCount;
                            //清零计数并开始计数
                            //TimeP = new System.DateTime(0);
                            //timer1.Start();
                            //label1.Text = TimeP.ToString("HH:mm:ss");
                            //生成字段名称，逐条写，无效率
                            for (int i = 0; i < gridView.ColumnCount; i++)
                            {
                                excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText.ToString();
                            }
                            //以下为填充数据关键代码，逐条写，无效率
                            for (int i = 0; i < gridView.RowCount; i++)
                            {
                                for (int j = 0; j < gridView.ColumnCount; j++)
                                {

                                    if (gridView[j, i]!=null && gridView[j, i].Value != null)
                                    {
                                        if (gridView[j, i].Value == typeof(string))
                                        {
                                            excel.Cells[i + 2, j + 1] = "" + gridView[j, i].Value.ToString();
                                        }
                                        else
                                        {
                                            excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                                        }
                                    }
                                    else
                                    {
                                        excel.Cells[i + 2, j + 1] = "";
                                    }
                                }
                                //进度条加1
                                //progressBar1.Value++;
                                /*
                                 * 注意此Application.DoEvents()，如果无此句，当切换窗口后回到本程序无法重绘窗体会出现假死状态
                                 * 此处我试过用委托和线程异步调用的方法，但效果没有这句效果好
                                 */
                                System.Windows.Forms.Application.DoEvents();

                            }

                            //保存EXCEL并释放资源
                            sheet.SaveAs(strName, miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss);
                            book.Close(false, miss, miss);
                            books.Close();
                            excel.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(books);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                            GC.Collect();
                            //timer1.Stop();
                            MessageBox.Show("数据已经成功导出到：" + saveFileDialog.FileName.ToString(), "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            //catch (Exception er)
            {
               // MessageBox.Show(er.Message);
                //timer1.Stop();
                return;
            }

        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            ToExcel1(dgvName);

        }

        private void tscbDays_TextChanged(object sender, EventArgs e)
        {
            if(tscbDays.Text == "All")
            {
                m_time = SvnProject.DisplayTime.ALL;
            }
            else if(tscbDays.Text == "Within two months")
            {
                m_time = SvnProject.DisplayTime.WITHIN_TWO_MONTHS;
            }
            else
            {
                m_time = SvnProject.DisplayTime.WITHIN_A_MONTH;
            }
            
            DisplayTable();

        }
     }
}
