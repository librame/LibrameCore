﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Themepacks.Simple
{
    using Extensions.Core.Localizers;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class ThemepackInfoResource_zh_CN : ResourceDictionary
    {
        public ThemepackInfoResource_zh_CN()
            : base()
        {
            AddOrUpdate("DisplayName", "简约", (key, value) => "简约");
            AddOrUpdate("PrivacyAndCookiePolicy", PrivacyAndCookiePolicy, (key, value) => PrivacyAndCookiePolicy);
            AddOrUpdate("PrivacyAndCookiePolicyButton", "接受", (key, value) => "接受");
        }


        private static string PrivacyAndCookiePolicy
            => "我们致力于保护您的隐私。阅读我们的客户隐私政策，了解我们如何收集、使用、披露、传输和存储您的个人信息。";
    }
}
