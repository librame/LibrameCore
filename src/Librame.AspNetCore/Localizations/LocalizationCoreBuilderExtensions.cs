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
using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    static class LocalizationCoreBuilderExtensions
    {
        public static ICoreBuilder AddLocalizations(this ICoreBuilder builder)
        {
            builder.Services.TryReplace<IStringLocalizerFactory, ExpressionStringLocalizerFactoryCore>();

            return builder;
        }

    }
}