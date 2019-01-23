using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace LYC.Common
{
    
    public delegate void ATSend(string text);
    public delegate bool ReceivedCallback(string text);
    public delegate void EventCallback(atResult obj);
    public enum ATResultType{
                Success,Failure,SingleSuccess, TimeOut,Message,Unknown
    };
//                    TransferThread = new Thread(OnBuildDataConnection);
//                    TransferThread.Start();
    
//    Thread TransferThread;
    
    public class atResult
    {
    
        protected ATResultType m_resultType =ATResultType.Unknown;
        protected int m_parameter1 =0;
        protected int m_parameter2 =0;
        protected string m_resultText=null;

        
        public atResult(string resultText)
        {
            ResultText = resultText;
            ResultType = ATResultType.Message;
        }
        public atResult(int p1, int p2)
        {
            Parameter1 = p1;
            Parameter2 = p2;
            ResultType = ATResultType.SingleSuccess;
        }
        public atResult(ATResultType resultType)
        {
            ResultType = resultType;
        }

        public ATResultType ResultType
        {
            get { return m_resultType;}
            set { m_resultType = value;}
        }
        public int Parameter1
        {
            get { return m_parameter1;}
            set { m_parameter1 = value;}
        }
        public int Parameter2
        {
            get { return m_parameter2;}
            set { m_parameter2 = value;}
        }
        public string ResultText
        {
            get { return m_resultText;}
            set { m_resultText = value;}
        }

    }
    public class atCommand
    {
        #region send command
        
        public const string FRAME_ENDMARK_ENTER = ";\n";
        public const string FRAME_ENDMARK = ";";
        public const string ATC_MARK = "AT+";
        
        public const string ATC_FGET = "AT+FGET";
        public const string ATC_FRD = "AT+FRD";
        public const string ATC_FPUT = "AT+FPUT";
        public const string ATC_FWR = "AT+FWR";
        public const string ATC_OK = "AT+OK";
        public const string ATC_FAIL = "AT+FAIL";



        string m_received = "";
        string m_filename = "";
        string m_realname = "";
        int m_fileOffset=0;
        int m_fileLen = 0;
        int m_fileBlockLen=0;
//        int m_getBlockMax=128;
        int m_putBlockMax=128;
        
        string m_fileID;
        string m_fileContent;
        ReceivedCallback m_fReceived;
        EventCallback m_fEventCB;
        ATSend m_fSend;
        
        int m_resend_times=0;
        
        protected Timer checkTimer =null;

        protected void TimerOnStart()
        {
            TimerOnStop();
            checkTimer = new Timer(new TimerCallback(CheckTimerCallBack), null,
                10000, 0);
        }

        protected void TimerOnStop()
        {
            if (checkTimer != null)
            {
                lock (checkTimer)
                {
                    checkTimer.Dispose();
                    checkTimer = null;
                }
            }
        }
        protected void CheckTimerCallBack(object o)
        {
            if(ReceivedEventHandle != null)
            {
                ReceivedEventHandle(new atResult(ATResultType.TimeOut));
            }

        }
        
        public void SetBlockMax(int max)
        {
            m_putBlockMax = (max >450 || max <=0)? 450: max;
        }
        public void SendLog(string port,string type)
        {
            m_fReceived = reponse_log_content;
            m_fSend("AT+LOG,"+port+",TYPE="+ type+FRAME_ENDMARK_ENTER);
        }
        
        public void SendLog(string port)
        {
            m_fReceived = reponse_log_content;
            m_fSend("AT+LOG,"+port+FRAME_ENDMARK_ENTER);
        }
        public void SendTelnet()
        {
            m_fReceived = null;
            m_fSend("AT+TELNET"+FRAME_ENDMARK_ENTER);
        }

        public void SendLogc()
        {
            m_fReceived = response_message;
            m_fSend( "AT+LOGC"+FRAME_ENDMARK_ENTER);
        }

        public void SendReadDb(string addr,bool loop)
        {
            m_fReceived = reponse_readDb_content;
            if(loop)
                m_fSend( "AT+RDB,ADDR="+addr+",LOOP"+FRAME_ENDMARK_ENTER);
            else
                m_fSend(  "AT+RDB,ADDR="+addr+FRAME_ENDMARK_ENTER);  
        }

       public void SendUpdateMbCfg()
        {
           m_fReceived = null;
            m_fSend( "AT+UPDATE_MBCFG"+FRAME_ENDMARK_ENTER);
        }
       
       public void SendExit()
        {
           m_fReceived = response_message;
            m_fSend( "AT+EXIT"+FRAME_ENDMARK_ENTER);
        }
        
        public void SendReboot(string target)
       {
            m_fReceived = null;
           m_fSend( "AT+REBOOT,TARGET="+target+FRAME_ENDMARK_ENTER);
       }     

         public void SendOK()
        {
             m_fReceived = null;
            m_fSend( "AT+OK"+FRAME_ENDMARK_ENTER);
        }     
    
         public void SendFail()
        {
             m_fReceived = null;
            m_fSend( "AT+FAIL"+FRAME_ENDMARK_ENTER);
        }     
        
         public void Getfile(string filename,string realName)
         {
             m_filename = filename;
             m_realname = realName;
             m_fReceived = response_getfile_id;
             m_resend_times =0;
             m_fSend( "AT+FGET,NAME="+realName+FRAME_ENDMARK_ENTER);
             TimerOnStart();
         }

        public void Putfile(string filename,string realName)
        {
            FileStream fs;
            m_filename = filename;
            m_realname = realName;
            m_fileContent = "";
            m_resend_times =0;
            using (fs = File.OpenRead(m_filename)) 
            {
                byte[] b = new byte[fs.Length];
                byte[] tmp = new byte[fs.Length<<1];
                byte bt;
                if (fs.Read(b, 0, (int)fs.Length) == fs.Length)
                {
                    for (UInt32 i = 0; i < b.Length; i++)
                    {
                        bt = (byte)((b[i] >> 4) & 0xF);
                        tmp[i<<1] = (byte)((bt >= 10) ? (bt - 10) + (byte)'A' : bt + (byte)'0');
                        bt = (byte)(b[i] & 0xF);
                        tmp[(i << 1) + 1] = (byte)((bt >= 10) ? (bt - 10) + (byte)'A' : bt + (byte)'0');
                        //m_fileContent += b[i].ToString("X2");
                    }
                    UTF8Encoding temp = new UTF8Encoding(true);
                    m_fileContent = temp.GetString(tmp);
                    m_fileLen = (int)fs.Length;
                    m_fReceived = response_putfile_id;
                    m_fSend( "AT+FPUT,NAME="+realName+",LEN="+((int)b.Length)+FRAME_ENDMARK_ENTER);
                }
            }
            TimerOnStart();
        }

        #endregion
        
        #region received command
        public void ReceivedFunc(string buf)
        {
            if(m_received.Length==0)
            {
                m_received = buf;
            }
            else
            {
                m_received+= buf;
            }
            
            if(m_received.Length>0)
            {
                char[] stringSeparators = new char[] { '\n'  };
                string[] cmd = m_received.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                int count;
                if (cmd!=null && cmd.Length > 0)
                {
                    if (cmd[cmd.Length - 1].IndexOf(FRAME_ENDMARK) > 0)
                    {
                        count = cmd.Length;
                        m_received = "";
                    }
                    else
                    {
                        count = cmd.Length - 1;
                        m_received = cmd[cmd.Length - 1];
                    }

                    for (int i = 0; i < count; i++)
                    {

                        if (cmd[i].IndexOf("AT+") >= 0 && cmd[i].IndexOf(FRAME_ENDMARK) > 0)
                        {
                            string cmd_item = cmd[i].Substring(cmd[i].IndexOf("AT+"));
                            //if(cmd_item.IndexOf(ATC_FAIL)>=0)
                            //{
                            //resend
                            //}
                            //else
                            {
                                if (m_fReceived != null)
                                {
                                    m_fReceived(cmd_item);
                                }
                            }
                        }
                    }
                }
            }

        }
        
        public void ReceivedFunc(byte[] buf)
        {
            UTF8Encoding temp = new UTF8Encoding(true);
            ReceivedFunc(temp.GetString(buf));
        }
        #endregion

        #region func
        bool response_message(string buf)
        {
            if(buf.IndexOf(ATC_FAIL)>=0)
            {
                //resend
                return false;
            }
            //AT+OK;
            if(ReceivedEventHandle!=null)
            {
                //ReceivedEventHandle(buf.Replace(ATC_MARK,"").Replace(";",""));
                ReceivedEventHandle(new atResult(buf.Replace(ATC_MARK,"").Replace(";","")));
            }
            return true;
        }
        bool reponse_log_content(string buf)
        {
            if(buf.IndexOf(ATC_FAIL)>=0)
            {
                //resend
                return false;
            }
            //AT+LOG,ID=;
            if(ReceivedEventHandle!=null)
            {
                //ReceivedEventHandle(buf.Replace(ATC_MARK,"").Replace(";",""));
                ReceivedEventHandle(new atResult(buf.Replace(ATC_MARK,"").Replace(";","")));
            }

            return true;
        }

        bool reponse_readDb_content(string buf)
        {
            if(buf.IndexOf(ATC_FAIL)>=0)
            {
                //resend
                return false;
            }
            //AT+LOG,ID=;
            if(ReceivedEventHandle!=null)
            {
                //ReceivedEventHandle(buf.Replace(ATC_MARK, ""));
                ReceivedEventHandle(new atResult(buf.Replace(ATC_MARK, "")));
            }

            return true;
        }


        bool response_getfile_id(string buf)
        {
            //AT+OK,ID=,LEN=;
            char[] charSeparators = new char[] { ',', '=', ';' };
            string[] item = buf.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            TimerOnStart();

            if(buf.IndexOf(ATC_FAIL)<0&&item.Length >=3)
            {
                m_resend_times =0;
                m_fileID = item[2];
                m_fReceived = response_getfile_content;
                m_fileLen = int.Parse(item[4]);
                m_fileOffset =0;
                m_fileContent = "";
                m_fSend( "AT+FRD,ID="+m_fileID+",OFF="+m_fileOffset+FRAME_ENDMARK_ENTER);
                return true;
            }
            if(m_resend_times++<10)
            {
                int resend_times=m_resend_times;
                Getfile(m_filename,m_realname);
                m_resend_times = resend_times;
            }
            else
            {
                if(ReceivedEventHandle != null)
                {
                    //int[] number = new int[3]{0, 0,2};
                    //ReceivedEventHandle(number);
                    TimerOnStop();
                    ReceivedEventHandle(new atResult(ATResultType.Failure));
                }
            }
            return false;
        }
        bool response_getfile_content(string buf)
        {
            //AT+OK,LEN=,file content;
            char[] charSeparators = new char[] { ',', '=', ';' };
            string[] item = buf.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            TimerOnStart();

            if(buf.IndexOf(ATC_FAIL)<0 && item.Length >=3)
            {
                int len = int.Parse(item[2]);
                m_resend_times =0;
                if(len!=0 && len == item[3].Length)
                {
                    m_fileOffset += (len/2);
                    m_fileContent += item[3];
                    if(ReceivedEventHandle != null)
                    {
                        //int[] number = new int[3]{m_fileOffset, m_fileLen,0};
                        //ReceivedEventHandle(number);
                        ReceivedEventHandle(new atResult(m_fileOffset,m_fileLen));
                    }
                    m_fSend( "AT+FRD,ID="+m_fileID+",OFF="+m_fileOffset+FRAME_ENDMARK_ENTER);
                    return true;
                }
                else if(len ==0)
                {
                    //save file
                    byte[] b= new byte[m_fileContent.Length/2];
                    byte[] tmp = System.Text.Encoding.ASCII.GetBytes(m_fileContent);
                    byte bt_low;
                    byte bt_hight;
                    for(int i=0;i<b.Length; i++)
                    {
                        bt_hight = (byte)((tmp[i << 1] >= (byte)'A') ? (tmp[i << 1] - 'A' + 10) : (tmp[i << 1] - (byte)'0'));
                        bt_low = (byte)((tmp[(i << 1) + 1] >= (byte)'A') ? (tmp[(i << 1)+1] - 'A' + 10) : (tmp[(i << 1)+1] - (byte)'0'));
                        b[i] = (byte)((bt_hight << 4)|bt_low);
                    }
                    File.WriteAllBytes(m_filename, b);
                    m_fReceived = null;
                    m_fileContent ="";
                    m_fileID ="";
                    if(ReceivedEventHandle != null)
                    {
                    
                        Thread.Sleep(50);
                        //int[] number = new int[3]{m_fileOffset, m_fileLen,1};
                        //ReceivedEventHandle(number);
                        
                        TimerOnStop();
                        ReceivedEventHandle(new atResult(ATResultType.Success));
                    }
                    
                    return true;
                }
            }
            if(m_resend_times++<10)
            {
                m_fSend( "AT+FRD,ID="+m_fileID+",OFF="+m_fileOffset+FRAME_ENDMARK_ENTER);
            }
            else
            {
                if(ReceivedEventHandle != null)
                {
                    //int[] number = new int[3]{0, 0,2};
                    //ReceivedEventHandle(number);
                    TimerOnStop();
                    ReceivedEventHandle(new atResult(ATResultType.Failure));
                }
            }
            return false;
        }
        bool response_putfile_id(string buf)
        {
            //AT+OK,ID=;
            char[] charSeparators = new char[] { ',', '=', ';' };
            string[] item = buf.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            TimerOnStart();

            if(buf.IndexOf(ATC_FAIL)<0&& item.Length >=3)
            {
                m_fileID = item[2];
                m_resend_times =0;
                m_fReceived = response_putfile_content;
                m_fileOffset =0;
                m_fileBlockLen = (m_fileContent.Length > m_putBlockMax) ? m_putBlockMax : m_fileContent.Length;
                m_fSend( "AT+FWR,ID="+m_fileID+",OFF=0,LEN="+m_fileBlockLen+","+m_fileContent.Substring(m_fileOffset,m_fileBlockLen)+FRAME_ENDMARK_ENTER);
                return true;
            }
            if(m_resend_times++<10)
            {
                int resend_times=m_resend_times;
                Putfile(m_filename,m_realname);
                m_resend_times = resend_times;
            }
            else
            {
                if(ReceivedEventHandle != null)
                {
                    //int[] number = new int[3]{0, 0,2};
                    //ReceivedEventHandle(number);
                    TimerOnStop();
                    ReceivedEventHandle(new atResult(ATResultType.Failure));
                }
            }
            return false;
        }
        bool response_putfile_content(string buf)
        {
            //AT+OK,
            //AT+FWR,ID=,OFF=,LEN=,file content;\n
            TimerOnStart();
            if(buf.IndexOf(ATC_FAIL)<0&&buf.IndexOf(ATC_OK)>=0)
            {
                m_resend_times =0;
                if(m_fileOffset != m_fileContent.Length)
                {
                    m_fileOffset+=m_fileBlockLen;
                    m_fileBlockLen = ((m_fileContent.Length-m_fileOffset) > m_putBlockMax) ? m_putBlockMax : m_fileContent.Length-m_fileOffset;
                    if(ReceivedEventHandle != null)
                    {
                        //int[] number = new int[3]{m_fileOffset, m_fileLen*2,0};
                        //ReceivedEventHandle(number);
                        ReceivedEventHandle(new atResult(m_fileOffset, m_fileLen*2));
                    }
                    m_fSend( "AT+FWR,ID="+m_fileID+",OFF="+(m_fileOffset/2)+",LEN="+m_fileBlockLen+","+m_fileContent.Substring(m_fileOffset,m_fileBlockLen)+FRAME_ENDMARK_ENTER);
                }
                else
                {
                    if(ReceivedEventHandle != null)
                    {
                        //int[] number = new int[3]{m_fileOffset, m_fileLen*2,1};
                        //ReceivedEventHandle(number);
                        TimerOnStop();
                        ReceivedEventHandle(new atResult(ATResultType.Success));
                    }

                }
                return true;
            }
            if(m_resend_times++<10)
            {
                m_fSend( "AT+FWR,ID="+m_fileID+",OFF="+(m_fileOffset/2)+",LEN="+m_fileBlockLen+","+m_fileContent.Substring(m_fileOffset,m_fileBlockLen)+FRAME_ENDMARK_ENTER);
            }
            else
            {
                if(ReceivedEventHandle != null)
                {
                    //int[] number = new int[3]{0, 0,2};
                    //ReceivedEventHandle(number);
                    TimerOnStop();
                    ReceivedEventHandle(new atResult(ATResultType.Failure));
                }
            }
            return false;
        }

        #endregion

       	public ATSend SendCmd
	{
            get
            {
                return m_fSend;
            }
            set
            {
                m_fSend = value;
            }
	}
	
       	public EventCallback ReceivedEventHandle
	{
            get
            {
                return m_fEventCB;
            }
            set
            {
                m_fEventCB = value;
            }
	}
    }
}
