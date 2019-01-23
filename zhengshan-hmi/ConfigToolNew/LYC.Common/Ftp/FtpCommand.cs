using System;
using System.Collections.Generic;
using System.Text;

namespace LYC.Common.Ftp
{
    class FtpCommand
    {
        public FtpCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new FtpException("empty ftp command");

            this.Command = command;
        }

        string command;

        /// <summary>
        /// FTP����
        /// </summary>
        public string Command
        {
            get { return command; }
            set { command = value; }
        }

        List<string> parameters = new List<string>();

        /// <summary>
        /// �����б�
        /// </summary>
        public List<string> Parameters
        {
            get{return parameters ; }
        }
    }
}
