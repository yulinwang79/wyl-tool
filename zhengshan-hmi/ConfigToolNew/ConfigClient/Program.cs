/********************************************************************
	created:	2008/04/16
	created:	16:4:2008   11:07
	filename: 	F:\Workspace\LYC.Common\Src\Net\OurFtpServer\Program.cs
	file path:	F:\Workspace\LYC.Common\Src\Net\OurFtpServer
	file base:	Program
	file ext:	cs
	author:		Deng.Yangjun@Gmail.com
	
	purpose:	
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using LYC.Common.Ftp;
using LYC.Common;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Config
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Console.WriteLine("********************************************************************");
            Console.WriteLine("LanYu Config Tool Debug");
            Console.WriteLine("");
            Console.WriteLine("");

  //          try
  //          {
  //              FtpServer server = new FtpServer();
  //              //using (FtpServer server = new FtpServer())
  //              {
 //                   string ftpHomeDir;
 //                   /*
 //                    * 服务器的最大连接数
 //                    */
 //                   server.Capacity = 1000;
//
 //                    /*
 //                    * 连接超时时间
 //                    */ 
 //                   server.HeartBeatPeriod = 120000;  //120秒
 //                   
 //                   /*
 //                     * 创建一个使用FTP的用户，
//                     */
//                    FtpUser user = new FtpUser("ftp");
//                    user.Password = "123";
//                    user.AllowWrite = true;
//                    ftpHomeDir = System.Windows.Forms.Application.StartupPath + "\\configfile";
//                     DirectoryInfo dir = new DirectoryInfo(ftpHomeDir);
//                    dir.Create();
//                    
//                    user.HomeDir = ftpHomeDir;
//
//                    /*
//                      * 限制该帐号的用户的连接服务器的最大连接数
//                     * 也就是限制该使用该帐号的FTP同时连接服务器的数量。
//                     */
//                    user.MaxConnectionCount = 2;
//                    
//                    /*
//                      * 限制用户的最大上传文件为20M，超过这个值上传文件会失败。
//                     * 默认不限制该值，可以传输大文件。
//                     */ 
//                    user.MaxUploadFileLength = 1024 * 1024 * 20; 
//                    server.AddUser(user);
//
//                     //把当前目录作为匿名用户的目录，测试目的(必须指定)
//                    server.AnonymousUser.HomeDir= System.Windows.Forms.Application.StartupPath + "\\configfile";
//                    
//                    server.Start();
//                    Console.WriteLine("Press enter to exit...");
//                    Console.ReadLine();
                    Application.EnableVisualStyles();
                   Application.SetCompatibleTextRenderingDefault(false);
                   Application.Run(new MainForm());
//
//                    server.Stop();
//                }
//             }
//            catch (System.Exception e)
//            {
//                NetDebuger.PrintErrorMessage("FATAL ERROR:"+e.Message);
//            }
//            
         }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            NetDebuger.PrintErrorMessage("UNHANDLED ERROR:"+e.ExceptionObject.ToString());
        }
    }
}
