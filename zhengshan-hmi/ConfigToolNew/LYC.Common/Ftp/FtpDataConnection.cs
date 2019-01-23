using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using LYC.Common.Tcp;
using System.IO;

namespace LYC.Common.Ftp
{
    class FtpDataConnection : TcpServerBase<TcpSession>
    {
        public FtpDataConnection()
        {
            Capacity = 1; //ֻ����һ������
        }

        AsyncHelper async = new AsyncHelper();

        public AsyncHelper AsyncHelper
        {
            get { return async; }
            set { async = value; }
        }

        long maxLengthOfUpload = long.MaxValue;

        public long MaxLengthOfUpload
        {
            get { return maxLengthOfUpload; }
            set { maxLengthOfUpload = value; }
        }

        protected override void OnStart()
        {
            base.OnStart();
            //��ʼ���յȴ����ӵ��첽����
            async.BeginAsyncOperation();
        }

        protected override void OnCloseSession(TcpSession session)
        {
            base.OnCloseSession(session);

            //����Է���Socket�رգ���ʾ�������ӶϿ��������첽������ɵı�־��
            async.EndAsyncOperation();
        }

        protected override bool OnCreateSession(TcpSession newSession)
        {
            //�����������ӵ��첽�������
            async.EndAsyncOperation();
            return true;
        }

        public void Accept(int timeOut)
        {
            async.WaitAsyncResult(timeOut);
        }

        protected override void OnSendEnd(TcpSession session, int sendCount)
        {
            //�����������
            async.EndAsyncOperation();
        }

        public bool Send(DataBlock data,int timeOut)
        {
            //û�����ӣ����ܷ�������
            if (Sessions.Count != 0)
            {
                async.BeginAsyncOperation();
                Send(Sessions[0], data);
                async.WaitAsyncResult(timeOut);
                return true;
            }

            return false;
        }

        protected internal override void ReportError(TcpSession session, Exception e)
        {
            base.ReportError(session, e);

            //��������������¼�
            async.EndAsyncOperationWithError(e);
        }

        int recvFileLength = 0;

        protected override void OnReceivedData(TcpSession session, DataBlock dataBlock)
        {
            try
            {
                recvFile.Write(dataBlock.Buffer, dataBlock.ReadIndex, dataBlock.DataLength);
                recvFileLength += dataBlock.DataLength;
            }
            catch (System.Exception e)
            {
                AsyncHelper.EndAsyncOperationWithError(e);
            }
            
            if(recvFileLength > MaxLengthOfUpload)
            {
                AsyncHelper.EndAsyncOperationWithError(new NetException("upload file's length limit"));
            }

            dataBlock.Reset();
        }

        public bool SendFile(string localPath, long restartPos)
        {
            using (FileStream file = new FileStream(localPath, FileMode.Open, FileAccess.Read))
            {
                file.Seek(restartPos, SeekOrigin.Begin);
                DataBlock data = new DataBlock(4096);
                bool continueSend = true;
                long sendCount = 0;
                long sendLength = file.Length - restartPos;

                while (continueSend)
                {
                    data.Reset();
                    int count = file.Read(data.Buffer, 0, data.WritableLength);
                    if (count == 0)
                        break; //read end
                    data.WriteIndex = count;

                    continueSend = Send(data, 60000); //60�뷢�����8K���ݣ�������ܷ��ͣ������˴η��͡�
                    if (continueSend)
                        sendCount += count;
                }

                if (sendCount == sendLength)
                    return true;
                else
                    return false; //������һ���֣�û����ȫ����
            }
        }

        FileStream recvFile;

        public FileStream RecvFile
        {
            get { return recvFile; }
            set { recvFile = value; }
        }

        public bool ReceiveFile(string localPath, long restartPos)
        {
            try
            {
                recvFileLength = 0;
                async.BeginAsyncOperation();
                AsyncHelper.WaitAsyncResult(6 * 60 * 60 * 1000); //������6��Сʱ�����һ���ļ��Ĵ���
            }
            catch (System.Exception)
            {
                recvFile.Close();

                try
                {
                    File.Delete(localPath);
                }
                catch (Exception)
                {
                }
                
                return false;
            }
            finally
            {
                recvFile.Close();
            }
        
            return true;
        }
    }
}
