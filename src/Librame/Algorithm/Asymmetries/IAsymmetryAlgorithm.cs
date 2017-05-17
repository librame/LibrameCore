﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Security.Cryptography;

namespace Librame.Algorithm.Asymmetries
{
    /// <summary>
    /// 非对称算法接口。
    /// </summary>
    public interface IAsymmetryAlgorithm
    {
        /// <summary>
        /// 字节转换器接口。
        /// </summary>
        IByteConverter ByteConverter { get; }

        
        /// <summary>
        /// 对指定字节数组进行签名。
        /// </summary>
        /// <param name="privateKey">给定的私钥。</param>
        /// <param name="bytes">给定要签名的字节数组。</param>
        /// <returns>返回签名后的字节数组。</returns>
        byte[] Signature(string privateKey, byte[] bytes);


        /// <summary>
        /// 转换为 RSA。
        /// </summary>
        /// <param name="str">给定待加密的字符串。</param>
        /// <param name="padding">给定的最优非对称加密填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="publicKeyString">给定的公钥字符串（可选）。</param>
        /// <returns>返回加密字符串。</returns>
        string ToRsa(string str, RSAEncryptionPadding padding = null, string publicKeyString = null);

        /// <summary>
        /// 还原 RSA。
        /// </summary>
        /// <param name="encrypt">给定的加密字符串。</param>
        /// <param name="padding">给定的最优非对称加密填充方式（可选；默认为 Pkcs1，支持 OpenSSL）。</param>
        /// <param name="privateKeyString">给定的私钥字符串（可选）。</param>
        /// <returns>返回原始字符串。</returns>
        string FromRsa(string encrypt, RSAEncryptionPadding padding = null, string privateKeyString = null);
    }
}