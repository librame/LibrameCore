﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份服务器构建器依赖选项。
    /// </summary>
    public class IdentityServerBuilderDependency : AbstractExtensionBuilderDependency<IdentityServerBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerBuilderDependency"/>。
        /// </summary>
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public IdentityServerBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(nameof(IdentityServerBuilderDependency), parentDependency)
        {
        }


        /// <summary>
        /// <see cref="IdentityServerOptions"/> 配置动作。
        /// </summary>
        public Action<IdentityServerOptions> IdentityServer { get; set; }
            = _ => { };

        /// <summary>
        /// <see cref="ConfigurationStoreOptions"/> 配置动作。
        /// </summary>
        public Action<ConfigurationStoreOptions> ConfigurationAction { get; set; }
            = options =>
            {
                // Client
                options.Client = TableConfigurationHelper.Create<Client>();
                options.ClientClaim = TableConfigurationHelper.Create<ClientClaim>();
                options.ClientCorsOrigin = TableConfigurationHelper.Create<ClientCorsOrigin>();
                options.ClientGrantType = TableConfigurationHelper.Create<ClientGrantType>();
                options.ClientIdPRestriction = TableConfigurationHelper.Create<ClientIdPRestriction>();
                options.ClientPostLogoutRedirectUri = TableConfigurationHelper.Create<ClientPostLogoutRedirectUri>();
                options.ClientProperty = TableConfigurationHelper.Create<ClientProperty>();
                options.ClientRedirectUri = TableConfigurationHelper.Create<ClientRedirectUri>();
                options.ClientScopes = TableConfigurationHelper.Create<ClientScope>();
                options.ClientSecret = TableConfigurationHelper.Create<ClientSecret>();

                // Resource
                options.IdentityResource = TableConfigurationHelper.Create<IdentityResource>();
                options.IdentityResourceClaim = TableConfigurationHelper.Create<IdentityResourceClaim>();
                options.IdentityResourceProperty = TableConfigurationHelper.Create<IdentityResourceProperty>();
                options.ApiResource = TableConfigurationHelper.Create<ApiResource>();
                options.ApiResourceClaim = TableConfigurationHelper.Create<ApiResourceClaim>();
                options.ApiResourceProperty = TableConfigurationHelper.Create<ApiResourceProperty>();
                options.ApiResourceScope = TableConfigurationHelper.Create<ApiResourceScope>();
                options.ApiResourceSecret = TableConfigurationHelper.Create<ApiResourceSecret>();
                options.ApiScope = TableConfigurationHelper.Create<ApiScope>();
                options.ApiScopeClaim = TableConfigurationHelper.Create<ApiScopeClaim>();
                options.ApiScopeProperty = TableConfigurationHelper.Create<ApiScopeProperty>();
            };

        /// <summary>
        /// <see cref="OperationalStoreOptions"/> 配置动作。
        /// </summary>
        public Action<OperationalStoreOptions> OperationalAction { get; set; }
            = options =>
            {
                options.PersistedGrants = TableConfigurationHelper.Create<PersistedGrant>();
                options.DeviceFlowCodes = TableConfigurationHelper.Create<DeviceFlowCodes>();
            };

    }
}
