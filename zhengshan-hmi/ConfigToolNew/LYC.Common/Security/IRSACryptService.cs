// ***************************************************************
//  ICryptService.cs   version:  1.4    date: 06/06/2006
//  -------------------------------------------------------------
//	author:		Yangjun Deng
// 	email:		LYC.Commonsoft@gmail.com
// 	purpose:	
//  -------------------------------------------------------------
//  Copyright (C) 2006 - LYC.Commonsoft All Rights Reserved
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace LYC.Common.Security
{
    public interface IRSACryptService
    {
        byte[] Decrypt(byte[] inBuffer, string pvk);
        byte[] Encrypt(byte[] inBuffer, string puk);

    }
}
