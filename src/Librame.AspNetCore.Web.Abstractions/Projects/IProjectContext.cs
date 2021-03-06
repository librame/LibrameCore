﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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
        /// 页面。
        /// </summary>
        IEnumerable<IProjectNavigation> Navigations { get; }

        /// <summary>
        /// 信息字典集合。
        /// </summary>
        IReadOnlyDictionary<string, IProjectInfo> Infos { get; }


        /// <summary>
        /// 当前项目。
        /// </summary>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        ProjectDescriptor Current { get; }


        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="area">给定的区域（请确保项目信息名称与给定的区域保持一致）。</param>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        ProjectDescriptor SetCurrent(string area);


        /// <summary>
        /// 查找指定名称的项目信息。
        /// </summary>
        /// <param name="name">给定的项目名称。</param>
        /// <returns>返回 <see cref="IProjectInfo"/>。</returns>
        IProjectInfo FindInfo(string name);

        /// <summary>
        /// 查找指定区域的项目导航。
        /// </summary>
        /// <param name="area">给定的区域。</param>
        /// <returns>返回 <see cref="IProjectNavigation"/>。</returns>
        IProjectNavigation FindNavigation(string area);
    }
}
