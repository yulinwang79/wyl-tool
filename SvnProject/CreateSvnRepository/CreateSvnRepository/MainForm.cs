using System;
using System.Text.RegularExpressions;
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
using System.Diagnostics;
using System.Management;
namespace CreateSvnRepository
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
       
        private void CraeteRepository(string strIn)
        {
            Process p = new Process();
            string sPath = "";
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            
            p.Start();
            if (tbPath.TextLength > 0)
            {
                sPath = tbPath.Text;
            }
            else
            {
                sPath = "D:\\SVNROOT";
            }
            sPath.TrimEnd('\\');
            if (Directory.Exists(sPath + "\\" + strIn))
            {
                MessageBox.Show("Repository already exists!", "Create Repository");
                return;
            }

            p.StandardInput.WriteLine("copy " + sPath + "\\" + "conf\\authz " + sPath + "\\" + "conf\\Zauthz" + (UInt32)(DateTime.Now.Ticks / 10000000));
            p.StandardInput.WriteLine("svnadmin create " + sPath + "\\" + strIn);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("svnadmin:") != -1)
            {
                MessageBox.Show(strRst, "Create Repository");
            }
            else
            {
                FileStream fs;
                string svnserve_filename = sPath + "\\" + strIn + "\\" + "conf" + "\\" + "svnserve.conf";
                string svnserve_content = "";
                using (fs = File.OpenRead(svnserve_filename))
                {
                    byte[] b = new byte[fs.Length];
                    if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                    {
                        UTF8Encoding temp = new UTF8Encoding(true);
                        svnserve_content = temp.GetString(b);
                        svnserve_content = svnserve_content.Replace("# anon-access = read", "anon-access = none");
                        svnserve_content = svnserve_content.Replace("# auth-access = write", "auth-access = write");
                        svnserve_content = svnserve_content.Replace("# password-db = passwd", "password-db = ../../conf/passwd");
                        svnserve_content = svnserve_content.Replace("# authz-db = authz", "authz-db = ../../conf/authz");
                     }
                }
                File.WriteAllBytes(svnserve_filename, System.Text.Encoding.ASCII.GetBytes(svnserve_content)); 
                string authz_filename = sPath + "\\" + "conf" + "\\" + "authz";
                string authz_content = "";
                using (fs = File.OpenRead(authz_filename))
                {
                    byte[] b = new byte[fs.Length];
                    if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                    {
                        UTF8Encoding temp = new UTF8Encoding(true);
                        authz_content = temp.GetString(b);
                        authz_content += "\r\n";
                        authz_content += "[" + strIn + ":/]\r\n";
                        authz_content += "@g_admin = rw\r\n";
                        if (radioButton1.Checked)
                        {
                            authz_content += "@g_rd_1 = rw\r\n";
                            authz_content += "@g_rd_2 = r\r\n";
                        }
                        else
                        {
                            authz_content += "@g_rd_1 = r\r\n";
                            authz_content += "@g_rd_2 = rw\r\n";
                        }
                        authz_content += "#* = r\r\n";
                    }
                }
                File.WriteAllBytes(authz_filename, System.Text.Encoding.ASCII.GetBytes(authz_content));
                
            }
            p.Close();
            //return pingrst;
        }

        private void RepositoryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '[' || e.KeyChar == ']' || e.KeyChar == ':'
            ||e.KeyChar == '*' || e.KeyChar == '\\' || e.KeyChar == '/'
                ||e.KeyChar == '?' || e.KeyChar == '<' || e.KeyChar == '>'
                ||e.KeyChar == '|' ))
            {
                e.Handled = true;
            }
        }

        private void PathTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '[' || e.KeyChar == ']' 
            || e.KeyChar == '*' ||  e.KeyChar == '/'
                || e.KeyChar == '?' || e.KeyChar == '<' || e.KeyChar == '>'
                || e.KeyChar == '|'))
            {
                e.Handled = true;
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (tbRepository.TextLength > 0)
            {
                string message = "Are you sure you want to create?";
                string caption = "Create Repository";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    // Closes the parent form.
                    CraeteRepository(tbRepository.Text);
                    this.Close();

                }              
            }
            else
            {
                string message = "You did not enter a repository name. Cancel this operation?";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    // Closes the parent form.

                    this.Close();

                }


            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
