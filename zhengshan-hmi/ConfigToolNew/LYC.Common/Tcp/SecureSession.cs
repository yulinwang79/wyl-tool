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
using LYC.Common.Security;

namespace LYC.Common.Tcp
{
    /// <summary>
    /// ��ȫͨ�ŻỰ��
    /// </summary>
    public class SecureSession:MessageBlockSession
    {
        public SecureSession()
        {
            SymmetricCryptService = new SymmetricCryptServiceBase();
        }

        HandshakeType handshake;
        public HandshakeType Handshake
        {
            get { return handshake; }
            set { handshake = value; }
        }

        /// <summary>
        /// �����ֵ�ʱ��ʹ�÷ǶԳƼ��ܷ�����ͨ��Զ�˵Ĺ���
        /// </summary>
        string publicKey;

        public string RemotePublicKey
        {
            get { return publicKey; }
            set { publicKey = value; }
        }

        /// <summary>
        /// �Ƿ��Ѿ�������ȫ����
        /// </summary>
        public bool IsBuildSecureConnection
        {
            get
            {
                return Handshake == HandshakeType.OK;
            }
        }

        public void CheckPrePhase(HandshakeType type)
        {
            if (Handshake != type)
            {
                throw new NetSecureException("����İ�ȫͨ�Ž���˳��");
            }
        }

        ISymmetricCryptService symmetricCryptService;

        /// <summary>
        /// ͨ�Ŷ�ʹ�õĶԳƼ��ܷ���(����ڷ�������ʹ�ã�Ӧ���ÿͻ��˵�key��IV��������)
        /// </summary>
        public ISymmetricCryptService SymmetricCryptService
        {
            get { return symmetricCryptService; }
            set { symmetricCryptService = value; }
        }

        
    }
}
