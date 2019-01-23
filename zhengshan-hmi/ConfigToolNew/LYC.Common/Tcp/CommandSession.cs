// ***************************************************************
// version:  2.0    date: 04/08/2008
//  -------------------------------------------------------------
// 
//  -------------------------------------------------------------
// previous version:  1.4    date: 05/11/2006
//  -------------------------------------------------------------
//  (C) 2006-2008  LYC.Common All Rights Reserved
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace LYC.Common.Tcp
{
    /// <summary>
    /// �����ı�����Ļػ�����
    /// </summary>
    public class CommandSession:TcpSession
    {
        Encoding encoding;
        string[] newLines;

        int maxCommandLength = 64*1024;

        public int MaxCommandLength
        {
            get { return maxCommandLength; }
            set { maxCommandLength = value; }
        }
        
        DataBlock cmdData = new DataBlock();

        /// <summary>
        /// �ı���ʹ�õı���
        /// </summary>
        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        /// <summary>
        /// �����зָ��ַ���
        /// </summary>
        public string[] NewLines
        {
            get
            {
                if (newLines == null || newLines.Length == 0)
                {
                    throw new TcpException("û�����������зָ��ַ���");
                }
                return newLines;
            }

            set 
            {
                if (value == null || value.Length == 0)
                {
                    throw new TcpException("�ָ��ַ������鲻��Ϊ�ջ��߳��Ȳ���Ϊ0");
                }

                newLines = value; 
            }
        }

        public event EventHandler<TEventArgs<string>> ReceivedCommand;

        protected internal override void ReceivedData(int readCount)
        {
            //�̰߳�ȫ���ѣ������ڶ��߳��е���
            this.Buffer.WriteIndex += readCount;
            //����ϴ�Ϊʹ�õ����ݣ����½���
            cmdData += Buffer;
            //����������
            Buffer.Reset();

            //ʹ�ù涨���ַ��������ַ�
            string rawCmd = Encoding.GetString(cmdData.Buffer, cmdData.ReadIndex, cmdData.DataLength);

            //��ֹ����������ַ������������Ƶ�����
            if(rawCmd.Length > MaxCommandLength)
            {
                throw new NetException("����̫��");
            }

            int length;
            string[] cmdArray = StringEx.Split(rawCmd, NewLines,out length);

            foreach (string cmd in cmdArray)
            {
                //�̰߳�ȫ
                EventHandler<TEventArgs<string>> temp = ReceivedCommand;
                if (temp != null)
                {
                    temp(this, new TEventArgs<string>(cmd));
                }
            }

            //����Ѿ�ʹ�õ��ַ��������ݳ��ȣ�Ȼ������Ϊ�Ѿ���ȡ
            cmdData.ReadIndex += Encoding.GetByteCount(rawCmd.Substring(0, length));
        }
       
    }
}
