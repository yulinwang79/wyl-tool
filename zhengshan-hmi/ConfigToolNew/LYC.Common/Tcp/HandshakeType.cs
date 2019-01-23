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
    public enum HandshakeType
    {
        ClientHello,   //�ͻ���Hello 
        ServerHello,  //������Hello

        ServerKeyExchange, //��������������
        ClientKeyExchange, //�����ͻ��˹���

        ClientSymmetricKey,//�ԳƼ��ܷ���˽��
        ServerGetSymmetricKey, //�������յ��ͻ��˶ԳƼ��ܷ���˽�׵Ļظ�

        ClientSymmetricIV, //�ԳƼ��ܷ�����ʼ������
        ServerGetSymmetricIV,//�������յ��ͻ��˶ԳƼ��ܷ�����ʼ�������Ļظ�

        ServerFinished, ��//��������֤����
        ClientFinished,����//�ͻ�����֤����

        OK//���ֳɹ�
    }
}
