﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Options
{
    using Extensions.Data.Options;

    /// <summary>
    /// 身份存储选项。
    /// </summary>
    public class IdentityStoreOptions : AbstractStoreOptions
    {
        /// <summary>
        /// 初始化选项。
        /// </summary>
        public IdentityStoreInitializationOptions Initialization { get; set; }
            = new IdentityStoreInitializationOptions();
    }
}
