using System;
using System.Collections.Generic;
using System.Text;

namespace LYC.Common.Ftp
{
    enum FtpOptionResult
    {
        AccessDenied,
        NotFindPath,
        OK,
    }

    public enum FtpSessionStatue
    {
        /// <summary>
        /// �Ѿ���½������״̬
        /// </summary>
        Wait,
        /// <summary>
        /// δ��½
        /// </summary>
        NotLogin,
        /// <summary>
        /// ����ִ��List����
        /// </summary>
        List,
        /// <summary>
        /// �����ϴ��ļ�
        /// </summary>
        Upload,
        /// <summary>
        /// ���������ļ�
        /// </summary>
        Download,
    }

    enum FtpOption
    {
        List,
        Create,
        Upload,
        Download,
        Delete
    }
}
