﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Data;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 配置数据库上下文访问器。
    /// </summary>
    public class ConfigurationDbContextAccessor : ConfigurationDbContextAccessor<Guid, int, Guid>
    {
        /// <summary>
        /// 构造一个配置数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        public ConfigurationDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }

    }


    /// <summary>
    /// 配置数据库上下文访问器。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class ConfigurationDbContextAccessor<TGenId, TIncremId, TCreatedBy>
        : DataDbContextAccessor<TGenId, TIncremId, TCreatedBy>, IConfigurationAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个配置数据库上下文访问器实例。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        protected ConfigurationDbContextAccessor(DbContextOptions options)
            : base(options)
        {
        }


        /// <summary>
        /// 客户端数据集。
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// 客户端跨域数据集。
        /// </summary>
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        /// <summary>
        /// 身份资源数据集。
        /// </summary>
        public DbSet<IdentityResource> IdentityResources { get; set; }

        /// <summary>
        /// API 资源数据集。
        /// </summary>
        public DbSet<ApiResource> ApiResources { get; set; }

        /// <summary>
        /// API 范围数据集。
        /// </summary>
        public DbSet<ApiScope> ApiScopes { get; set; }


        /// <summary>
        /// 客户端数据集管理器。
        /// </summary>
        public DbSetManager<Client> ClientsManager
            => Clients.AsManager();

        /// <summary>
        /// 客户端跨域数据集管理器。
        /// </summary>
        public DbSetManager<ClientCorsOrigin> ClientCorsOriginsManager
            => ClientCorsOrigins.AsManager();

        /// <summary>
        /// 身份资源数据集管理器。
        /// </summary>
        public DbSetManager<IdentityResource> IdentityResourcesManager
            => IdentityResources.AsManager();

        /// <summary>
        /// API 资源数据集管理器。
        /// </summary>
        public DbSetManager<ApiResource> ApiResourcesManager
            => ApiResources.AsManager();

        /// <summary>
        /// API 范围数据集管理器。
        /// </summary>
        public DbSetManager<ApiScope> ApiScopesManager
            => ApiScopes.AsManager();


        /// <summary>
        /// 异步保存更改。
        /// </summary>
        /// <returns>返回一个包含整数的异步操作。</returns>
        public virtual Task<int> SaveChangesAsync()
            => base.SaveChangesAsync();


        /// <summary>
        /// 配置模型构建器核心。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreatingCore(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingCore(modelBuilder);

            var storeOptions = this.GetService<IOptions<ConfigurationStoreOptions>>().Value;
            modelBuilder.ConfigureClientContext(storeOptions);
            modelBuilder.ConfigureResourcesContext(storeOptions);
        }

    }
}
