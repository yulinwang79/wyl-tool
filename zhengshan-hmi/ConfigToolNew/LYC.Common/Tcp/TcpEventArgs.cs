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
    /// ���ݿ��¼�������
    /// </summary>
    public class DataBlockArgs : System.EventArgs
   {
        public DataBlockArgs(DataBlock dataBlock)
        {
            this.dataBlock = dataBlock;
        }

       DataBlock dataBlock;

       public DataBlock DataBlock
       {
           get { return dataBlock; }
       }

   };

    /// <summary>
    /// ��Ϣ���¼�������
    /// </summary>
    public class MessageBlockArgs:System.EventArgs
    {
        MessageBlock messageBlock;

        public MessageBlock MessageBlock
        {
            get { return messageBlock; }
        }

        public MessageBlockArgs(MessageBlock messageBlock)
        {
            this.messageBlock = messageBlock;
        }

    };
     
}
