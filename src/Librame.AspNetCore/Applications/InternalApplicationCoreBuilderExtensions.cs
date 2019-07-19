﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// 内部应用程序核心构建器静态扩展。
    /// </summary>
    internal static class InternalApplicationCoreBuilderExtensions
    {
        /// <summary>
        /// 添加应用程序集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="ICoreBuilder"/>。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddApplications(this ICoreBuilder builder)
        {
            builder.Services.AddSingleton<IApplicationLocalization, InternalApplicationLocalization>();
            builder.Services.AddSingleton<IApplicationPrincipal, InternalApplicationPrincipal>();

            return builder;
        }

    }
}