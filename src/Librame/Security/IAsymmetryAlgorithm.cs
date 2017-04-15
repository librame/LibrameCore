﻿// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Security
{
    /// <summary>
    /// 非对称算法接口。
    /// </summary>
    /// <author>Librame Pang</author>
    public interface IAsymmetryAlgorithm : IAlgorithm
    {
        /// <summary>
        /// 获取或设置是否要包括私有参数。
        /// </summary>
        /// <value>
        /// True 表示公私参数对；False 表示公有参数（默认为 False）。
        /// </value>
        bool IncludePrivateParameters { get; set; }

        /// <summary>
        /// 获取或设置算法参数。
        /// </summary>
        /// <value>
        /// 如果需要生成，则由属性 <see cref="IncludePrivateParameters"/> 控制此参数为公有还是公私参数对。
        /// </value>
        object Parameters { set; get; }

        /// <summary>
        /// 解码字符串。
        /// </summary>
        /// <param name="str">给定要解密的字符串。</param>
        /// <returns>返回解码后的字符串。</returns>
        string Decode(string str);

        /// <summary>
        /// 计算解码。
        /// </summary>
        /// <param name="buffer">给定的字节数组。</param>
        /// <returns>返回计算后的原始字符串。</returns>
        byte[] ComputeDecoding(byte[] buffer);
    }
}