﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace LibrameCore.Abstractions
{
    using Extensions.Authentication;
    using Extensions.Authentication.Descriptors;
    
    /// <summary>
    /// <see cref="IExtensionBuilder"/> 认证静态扩展。
    /// </summary>
    public static class AuthenticationExtensionBuilderExtensions
    {

        /// <summary>
        /// 使用认证扩展。
        /// </summary>
        /// <typeparam name="TRole">指定的角色类型（整数型主键）。</typeparam>
        /// <typeparam name="TUser">指定的用户类型（整数型主键）。</typeparam>
        /// <typeparam name="TUserRole">指定的用户角色类型（整数型主键）。</typeparam>
        /// <param name="extension">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <returns>返回 <see cref="IExtensionBuilder"/>。</returns>
        public static IExtensionBuilder UseAuthenticationExtension<TRole, TUser, TUserRole>(this IExtensionBuilder extension)
            where TRole : class, IRoleDescriptor<int>
            where TUser : class, IUserDescriptor<int>
            where TUserRole : class, IUserRoleDescriptor<int, int, int>
        {
            return extension.UseAuthenticationExtension<TRole, TUser, TUserRole, int, int, int>();
        }
        /// <summary>
        /// 使用认证扩展。
        /// </summary>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
        /// <typeparam name="TRoleId">指定的角色主键类型。</typeparam>
        /// <typeparam name="TUserId">指定的用户主键类型。</typeparam>
        /// <typeparam name="TUserRoleId">指定的用户角色主键类型。</typeparam>
        /// <param name="extension">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <returns>返回 <see cref="IExtensionBuilder"/>。</returns>
        public static IExtensionBuilder UseAuthenticationExtension<TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId>(this IExtensionBuilder extension)
            where TRole : class, IRoleDescriptor<TRoleId>
            where TUser : class, IUserDescriptor<TUserId>
            where TUserRole : class, IUserRoleDescriptor<TUserRoleId, TUserId, TRoleId>
        {
            extension.Builder.UseMiddleware<AuthenticationExtensionMiddleware<TRole, TUser, TUserRole, TRoleId, TUserId, TUserRoleId>>();
            extension.Builder.UseAuthentication();

            return extension;
        }


        //private static void UseLibrameJwtBearerAuthentication(this IApplicationBuilder app,
        //    AuthenticationOptions options, Action<JwtBearerOptions> jwtBearerOptionsAction = null)
        //{
        //    // 默认以授权编号为密钥
        //    var algorithmOptions = app.ApplicationServices.GetOptions<AlgorithmOptions>();
        //    var secretKeyBytes = algorithmOptions.FromAuthIdAsBytes();

        //    var parameters = new TokenValidationParameters
        //    {
        //        AuthenticationType = AuthenticationOptions.DEFAULT_SCHEME,

        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                
        //        ValidateIssuer = true,
        //        ValidIssuer = TokenOptions.DefaultIssuer,
                
        //        ValidateAudience = true,
        //        ValidAudience = TokenOptions.DefaultAudience,

        //        // Validate the token expiry
        //        ValidateLifetime = true,

        //        // If you want to allow a certain amount of clock drift, set that here:
        //        ClockSkew = TimeSpan.Zero
        //    };

        //    var jwtBearerOptions = new JwtBearerOptions
        //    {
        //        AuthenticationScheme = AuthenticationOptions.DEFAULT_SCHEME,
        //        // 是否自动启用验证，如果不启用，则即便客服端传输了Cookie信息，服务端也不会主动解析。
        //        // 除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
        //        //AutomaticAuthenticate = true,
        //        //AutomaticChallenge = true,
        //        TokenValidationParameters = parameters
        //    };

        //    // Custom Configure Options
        //    jwtBearerOptionsAction?.Invoke(jwtBearerOptions);

        //    // Use Options
        //    app.UseJwtBearerAuthentication(jwtBearerOptions);
        //}

    }
}