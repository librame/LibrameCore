﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.Web.Projects
{
    using Extensions.Core.Services;

    /// <summary>
    /// 项目上下文接口。
    /// </summary>
    public interface IProjectContext
    {
        /// <summary>
        /// 服务工厂。
        /// </summary>
        ServiceFactory ServiceFactory { get; }

        /// <summary>
        /// 导航集合。
        /// </summary>
        IEnumerable<IProjectNavigation> Navigations { get; }

        /// <summary>
        /// 信息字典集合。
        /// </summary>
        IReadOnlyDictionary<string, IProjectInfo> Infos { get; }

        /// <summary>
        /// 当前项目。
        /// </summary>
        (IProjectInfo Info, IProjectNavigation Navigation) Current { get; }


        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="area">给定的区域（请确保项目信息名称与给定的区域保持一致）。</param>
        /// <returns>返回包含 <see cref="IProjectInfo"/> 与 <see cref="IProjectNavigation"/> 的元组。</returns>
        (IProjectInfo Info, IProjectNavigation Navigation) SetCurrent(string area);
    }
}