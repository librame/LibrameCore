﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameStandard
{
    using Authentication;

    /// <summary>
    /// Librame 构建器 MVC 适配静态扩展。
    /// </summary>
    public static class LibrameBuilderMvcAdaptationExtensions
    {
        /// <summary>
        /// 获取认证适配器。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="tokenOptions">给定的令牌选项（可选）。</param>
        /// <returns>返回认证适配器。</returns>
        public static IAuthenticationAdapter GetAuthenticationAdapter(this ILibrameBuilder builder,
            TokenHandlerSettings tokenOptions = null)
        {
            var adapter = builder.GetAdapter<IAuthenticationAdapter>();

            // 增强令牌
            adapter.BuildUpToken(tokenOptions);

            return adapter;
        }

    }
}
