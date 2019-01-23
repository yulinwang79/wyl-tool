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
using System.Security.Cryptography;

namespace LYC.Common.Security
{
    public static class SecurityCommon
    {
        /// <summary>
        /// �������һָ�����ȵ��ֽ�����
        /// </summary>
        /// <param name="byteLength"></param>
        /// <returns></returns>
        public static byte[] GenerateBytes(int byteLength)
        {
            byte[] buff = new Byte[byteLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // ��������ʹ��������ǿ������ֽڽ������
            rng.GetBytes(buff);
            return buff;
        }

        public static string Md5( string org)
        {
            byte[] bytes = MD5CryptoServiceProvider.Create().ComputeHash(Encoding.Unicode.GetBytes(org));
            return Convert.ToBase64String(bytes);
        }
    }
}
