﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Api.Tests
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Stores;
    using Extensions;
    using Extensions.Core.Identifiers;

    internal static class TestServiceProvider
    {
        static TestServiceProvider()
        {
            Current = Current.EnsureSingleton(() =>
            {
                var services = new ServiceCollection();

                services.AddAuthentication(options =>
                {
                    // SignInManager.SignOutAsync
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    // SignInManager.SignInWithClaimsAsync
                    //options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
                });

                services.AddLibrameCore(dependency =>
                {
                    dependency.Options.Identifier.GuidIdentifierGenerator = CombIdentityGenerator.MySQL;
                })
                .AddData(dependency =>
                {
                    dependency.Options.DefaultTenant.DefaultConnectionString
                        = "Data Source=.;Initial Catalog=librame_identity_default;Integrated Security=True";
                    dependency.Options.DefaultTenant.WritingConnectionString
                        = "Data Source=.;Initial Catalog=librame_identity_writing;Integrated Security=True";

                    dependency.Options.DefaultTenant.WritingSeparation = true;
                    dependency.Options.DefaultTenant.DataSynchronization = true;
                    dependency.Options.DefaultTenant.StructureSynchronization = true;
                })
                .AddAccessorCore<IdentityDbContextAccessor>((tenant, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(tenant.DefaultConnectionString,
                        sql => sql.MigrationsAssembly(typeof(IdentityDbContextAccessor).GetAssemblyDisplayName()));
                })
                .AddDatabaseDesignTime<SqlServerDesignTimeServices>()
                .AddStoreHub<IdentityStoreHub>()
                .AddStoreIdentifierGenerator<GuidIdentityStoreIdentityGenerator>()
                .AddStoreInitializer<IdentityStoreInitializer>()
                .AddIdentity<IdentityDbContextAccessor>(dependency =>
                {
                    dependency.Identity.Options.Stores.MaxLengthForKeys = 128;
                })
                .AddIdentityApi(dependency =>
                {
                    dependency.SupportsQueryRoles = true;
                    dependency.SupportsQueryUsers = true;
                    dependency.SupportsQueryUserLogins = true;
                    dependency.SupportsQueryUserTokens = true;
                });

                return services.BuildServiceProvider();
            });
        }

        public static IServiceProvider Current { get; }
    }
}