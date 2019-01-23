using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LYC.Common.Tcp
{
    public class TextCmdClient : CommandClient<CommandSession>
    {
        public delegate void ReceivedEventHandle(string command);
        public event ReceivedEventHandle Received=null;
        public TextCmdClient()
        {
            this.NewLines = new string[] { "AT+LOG,","\n" };
            this.Encoding = Encoding.ASCII;
        }

        protected override void OnBuildDataConnection()
        {
            //while (IsConnected)
            //{
                //组合在一起发送，其中发送了一个空字符命令
                //Send("命令:LIST" + NewLines[2] + "命令:USER" + NewLines[1]);
                //Send("命令:OPEN" + NewLines[2] + "" + NewLines[0]);
            //    Thread.Sleep(1000);
            //}
            //if (IsConnected)
            //{
            //    Send("AT+TELNET\n");
            //    Thread.Sleep(1000);
            //}
        }

        protected override void OnDropLine()
        {
            //NetDebuger.PrintDebugMessage("Drop Line!!!");
        }

        protected override void OnReceivedCommand(CommandSession session, string command)
        {
            //NetDebuger.PrintDebugMessage(command);
            if (Received != null)
            {
                Received(command);
            }
        }
        public ReceivedEventHandle OnReceivedEventHandle
        {
            get
            {
                return Received;
            }
            set
            {
                Received = value;
            }
        }
    }
}
