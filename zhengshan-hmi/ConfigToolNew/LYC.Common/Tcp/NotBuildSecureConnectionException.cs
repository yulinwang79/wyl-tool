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
    ///  ��ȫ����δ�������쳣
    /// </summary>
    public class NotBuildSecureConnectionException:NetSecureException
    {
        public NotBuildSecureConnectionException(string message)
            :base(message)
        {
        }

        public NotBuildSecureConnectionException()
            :base("������δ������ȫͨ�����ӵ�Զ��ͨ�š�")
        { 
        }
    }
}
