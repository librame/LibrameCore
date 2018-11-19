﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

namespace Librame.Builders
{
    using AspNetCore.Identity;
    using Extensions.Data;

    /// <summary>
    /// 身份构建器静态扩展。
    /// </summary>
    public static class IdentityBuilderExtensions
    {

        /// <summary>
        /// 添加身份。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{IdentityBuilderOptions}"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddIdentity(this IBuilder builder,
            IConfiguration configuration = null, Action<IdentityBuilderOptions> configureOptions = null)
        {
            builder.PreConfigureBuilder(configuration, configureOptions);

            // 引入数据扩展
            var dataBuilder = builder.AddData(configureOptions: data => configureOptions.Invoke(data));

            var identityBuilder = dataBuilder.AsIdentityBuilder();

            return identityBuilder;
        }


        /// <summary>
        /// 转换为内部身份构建器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDataBuilder"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AsIdentityBuilder(this IDataBuilder builder)
        {
            return new InternalIdentityBuilder(builder);
        }

        /// <summary>
        /// 添加核心与角色。
        /// </summary>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TDbContext">指定的数据库上下文类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{IdentityOptions}"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder AddCore<TUser, TRole, TDbContext>(this IIdentityBuilder builder, Action<IdentityOptions> configureOptions = null)
            where TUser : class
            where TRole : class
            where TDbContext : DbContext
        {
            builder.RegisterCore<TUser>(configureOptions);

            builder.Core.AddRoles<TRole>()
                .AddEntityFrameworkStores<TDbContext>();

            return builder;
        }

        /// <summary>
        /// 使用用户界面。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilder"/>。</param>
        /// <param name="configureAction">给定的 <see cref="Action{IIdentityBuilder}"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public static IIdentityBuilder WithUI(this IIdentityBuilder builder, Action<IIdentityBuilder> configureAction)
        {
            builder.ConfigureCore(identity =>
            {
                identity.AddSignInManager();
            });

            configureAction.Invoke(builder);

            builder.ConfigureCore(identity =>
            {
                identity.AddDefaultTokenProviders();
            });

            return builder;
        }

    }
}