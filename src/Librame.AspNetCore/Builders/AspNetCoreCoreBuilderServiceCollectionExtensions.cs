﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Builders;
using Librame.Extensions.Core.Builders;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="CoreBuilder"/> for ASP.NET Core 服务集合静态扩展。
    /// </summary>
    public static class AspNetCoreCoreBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<AspNetCoreCoreBuilderDependency> configureDependency = null,
            Func<IServiceCollection, AspNetCoreCoreBuilderDependency, ICoreBuilder> builderFactory = null)
            => services.AddLibrameCore<AspNetCoreCoreBuilderDependency>(configureDependency, builderFactory);

        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore<TDependency>(this IServiceCollection services,
            Action<TDependency> configureDependency = null,
            Func<IServiceCollection, TDependency, ICoreBuilder> builderFactory = null)
            where TDependency : AspNetCoreCoreBuilderDependency, new()
            => services
                .AddLibrame(configureDependency, builderFactory)
                .AddInternalAspNetCoreServices();


        private static ICoreBuilder AddInternalAspNetCoreServices(this ICoreBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return builder;
        }

    }
}
