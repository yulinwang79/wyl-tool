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
    /// ����δ�������쳣��
    /// </summary>
    public class NetBuildConnectionException:NetException
    {
        public NetBuildConnectionException(string message)
            :base(message)
        {
        }

        public NetBuildConnectionException()
            : base("������δ����ͨ�����ӵ�Զ��ͨ�š�")
        {
        }
    }
}