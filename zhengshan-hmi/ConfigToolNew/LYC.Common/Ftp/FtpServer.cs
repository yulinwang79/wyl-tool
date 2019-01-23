using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using LYC.Common.Tcp;

namespace LYC.Common.Ftp
{
    public class FtpServer:CommandServer<FtpSession>
    {
        Dictionary<string, FtpUser> users = new Dictionary<string, FtpUser>();
        string serverIp;
        string byeMessage;
        string welcomeMessage;
        bool allowAnonymous;
        List<string> ipFilter = new List<string>();
        FtpUser anonymousUser;

        public FtpServer()
        {
            EnableCheckHeartBeat = true;

            //1�����ڿͻ���û�з�Ӧ�����߳��ͻ���
            HeartBeatPeriod = 60000;
 
            //Ĭ��FTP�����˿�
            Port = 21;
            
            //FTPЭ��ʹ�õ�����ָ�����
            NewLines = new string[]{ "\r\n" };

            WelcomeMessage = "220 LYC.Common Village FTP Server V2.1 ready.";
            ByeMessage = "Bye.";

            //Ĭ��ʹ�������ַ���������������������������ԣ�������ָ����
            Encoding = Encoding.GetEncoding("gb18030");

            //Ĭ������������½
            AllowAnonymous = true;

            //Ĭ�����������û�����Ϣ
            anonymousUser = new FtpUser("anonymous");

            anonymousUser.AllowWrite = true;
            
        }

        public FtpUser AnonymousUser
        {
            get { return anonymousUser; }
        }

        /// <summary>
        /// IP������
        /// </summary>
        public List<string> IPFilter
        {
            get { return ipFilter; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        public Dictionary<string,FtpUser> Users
        {
            get { return users; }
        }
        
        /// <summary>
        /// ָ��PASV������Ҫָ��FTP Server��IP��ַ�������ָ����ϵͳ���Զ�ѡ��һ����������IP��ַ
        /// </summary>
        public string PasvIPSetting
        {
            get { return serverIp; }
            set { serverIp = value; }
        }
      
        /// <summary>
        /// �ټ���Ϣ
        /// </summary>
        public string ByeMessage
        {
            get { return byeMessage; }
            set { byeMessage = value; }
        }
        
        /// <summary>
        /// ��ӭ��Ϣ
        /// </summary>
        public string WelcomeMessage
        {
            get { return welcomeMessage; }
            set {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length > 75)
                {
                    throw new NetException("Welcom message is too long");
                }
                welcomeMessage = value; 
            }
        }

        /// <summary>
        /// �Ƿ�����������½
        /// </summary>
        public bool AllowAnonymous
        {
            get { return allowAnonymous; }
            set { allowAnonymous = value; }
        }

        protected override bool OnCreateSession(FtpSession newSession)
        {
            if (!base.OnCreateSession(newSession))
                return false;
            
            //����IP���Ƽ��
            if (!CheckClientIP(newSession))
                return false;

            newSession.ftpServer = this;
            newSession.Response(WelcomeMessage);

            //ͨ���̷����ж����еĻ���
            if ( AnonymousUser.HomeDir[1] == ':') //windows OS Path()
                newSession.LocalPathSpliter = '\\';
            else
                newSession.LocalPathSpliter = '/';  //Unix OS Path

            return true;
        }

        /// <summary>
        /// ����Ƿ�����ЧIP
        /// </summary>
        /// <param name="newSession"></param>
        /// <returns>����ͻ��˵�IP�����Ʒ���false,���򷵻�true</returns>
        protected virtual bool CheckClientIP(FtpSession newSession)
        {
            IPEndPoint remote = newSession.Socket.RemoteEndPoint as IPEndPoint;
            string remoteIP = remote.Address.ToString();
            foreach (string ip in IPFilter)
            {
                if (remoteIP == ip)
                {
                    NetDebuger.PrintDebugMessage(newSession,"Server rejected the client:"+remoteIP);
                    return false;
                }
            }
            NetDebuger.PrintDebugMessage(newSession, "Server accepted the client:" + remoteIP);
            return true;
        }
       
        protected override void OnReceivedCommand(FtpSession session, string cmdText)
        {
            session.SwitchFtpRequest(cmdText);
        }

        /// <summary>
        /// �Ƿ���һ���Ϸ����û�
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        protected internal virtual bool ValidateUser(FtpSession session)
        {
            if (AllowAnonymous && session.UserName == AnonymousUser.UserName)
            {
                session.user = AnonymousUser;
                return true;
            }

            try
            {
                FtpUser user = Users[session.UserName];
                if (user.Password == session.Password)
                {
                    session.user = user;
                   
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        protected override void OnStart()
        {
            if (AllowAnonymous && string.IsNullOrEmpty(AnonymousUser.HomeDir))
                throw new NetException("Not set anonymous user's home directory");

            //���Ϊ��������һ��IP,����Ҫ�Զ���ȡһ��IP��ַ
            if (string.IsNullOrEmpty(PasvIPSetting))
            {
                string hostName = Dns.GetHostName();
                IPAddress[] ips = Dns.GetHostAddresses(hostName);
                foreach (IPAddress ip in ips)
                {
                    //ֻ��ʹ��IPV4���磬��֧��IPV6
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        PasvIPSetting = ip.ToString();
                        break;
                    }
                    continue;
                }

                if (string.IsNullOrEmpty(PasvIPSetting))
                    throw new NetException("No available network interface");
            }

            base.OnStart();
        }

        protected internal override void ReportError(FtpSession session, Exception e)
        {
            NetDebuger.PrintErrorMessage(e.ToString());
        }

        /// <summary>
        /// ���һ���û�
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(FtpUser user)
        {
            Users.Add(user.UserName, user);
        }

        /// <summary>
        /// ɾ��һ���û�
        /// </summary>
        /// <param name="user"></param>
        public void RemoveUser(FtpUser user)
        {
            Users.Remove(user.UserName);
        }
    }
}
