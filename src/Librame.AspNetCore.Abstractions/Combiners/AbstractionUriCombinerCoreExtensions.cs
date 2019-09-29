﻿#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// 抽象 URI 组合器核心静态扩展。
    /// </summary>
    public static class AbstractionUriCombinerCoreExtensions
    {
        /// <summary>
        /// 转换为 URI 组合器核心。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <param name="newScheme">给定的新协议（可选）。</param>
        /// <param name="newHost">给定可能包含端口号的新主机（可选）。</param>
        /// <param name="newPath">给定以 / 开始的新路径（可选）。</param>
        /// <param name="newQuery">给定以 ? 开始的新查询（可选）。</param>
        /// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore AsUriCombinerCore(this string uriString, string newScheme = null,
            HostString newHost = default, PathString newPath = default, QueryString newQuery = default,
            string newAnchor = null)
        {
            var locator = (UriCombinerCore)uriString;

            return locator.ChangeParameters(newScheme, newHost, newPath,
                newQuery, queriesAction: null, newAnchor);
        }

        /// <summary>
        /// 转换为 URI 组合器核心。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <param name="queriesAction">给定的改变查询参数集合动作。</param>
        /// <param name="newScheme">给定的新协议（可选）。</param>
        /// <param name="newHost">给定可能包含端口号的新主机（可选）。</param>
        /// <param name="newPath">给定以 / 开始的新路径（可选）。</param>
        /// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore AsUriCombinerCore(this string uriString,
            Action<ConcurrentDictionary<string, string>> queriesAction, string newScheme = null,
            HostString newHost = default, PathString newPath = default, string newAnchor = null)
        {
            var locator = (UriCombinerCore)uriString;

            return locator.ChangeParameters(newScheme, newHost, newPath,
                newQuery: default, queriesAction, newAnchor);
        }


        /// <summary>
        /// 转换为 URI 组合器核心。
        /// </summary>
        /// <param name="uri">给定的 URI 字符串。</param>
        /// <param name="newScheme">给定的新协议（可选）。</param>
        /// <param name="newHost">给定可能包含端口号的新主机（可选）。</param>
        /// <param name="newPath">给定以 / 开始的新路径（可选）。</param>
        /// <param name="newQuery">给定以 ? 开始的新查询（可选）。</param>
        /// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore AsUriCombinerCore(this Uri uri, string newScheme = null,
            HostString newHost = default, PathString newPath = default, QueryString newQuery = default,
            string newAnchor = null)
        {
            var locator = (UriCombinerCore)uri;

            return locator.ChangeParameters(newScheme, newHost, newPath,
                newQuery, queriesAction: null, newAnchor);
        }

        /// <summary>
        /// 转换为 URI 组合器核心。
        /// </summary>
        /// <param name="uri">给定的 URI 字符串。</param>
        /// <param name="queriesAction">给定的改变查询参数集合动作。</param>
        /// <param name="newScheme">给定的新协议（可选）。</param>
        /// <param name="newHost">给定可能包含端口号的新主机（可选）。</param>
        /// <param name="newPath">给定以 / 开始的新路径（可选）。</param>
        /// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore AsUriCombinerCore(this Uri uri,
            Action<ConcurrentDictionary<string, string>> queriesAction, string newScheme = null,
            HostString newHost = default, PathString newPath = default, string newAnchor = null)
        {
            var locator = (UriCombinerCore)uri;

            return locator.ChangeParameters(newScheme, newHost, newPath,
                newQuery: default, queriesAction, newAnchor);
        }


        private static UriCombinerCore ChangeParameters(this UriCombinerCore locator, string newScheme = null,
            HostString newHost = default, PathString newPath = default, QueryString newQuery = default,
            Action<ConcurrentDictionary<string, string>> queriesAction = null, string newAnchor = null)
        {
            if (newScheme.IsNotNullOrEmpty())
                locator.ChangeScheme(newScheme);

            if (newHost.HasValue)
                locator.ChangeHost(newHost);

            if (newPath.HasValue)
                locator.ChangePath(newPath);

            if (newQuery.HasValue)
                locator.ChangeQuery(newQuery);

            if (queriesAction.IsNotNull())
                locator.ChangeQueries(queriesAction);

            if (newAnchor.IsNotNullOrEmpty())
                locator.ChangeAnchor(newAnchor);

            return locator;
        }


        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <param name="newHostFactory">给定的新主机工厂方法。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore ChangeHost(this UriCombinerCore locator, Func<HostString, HostString> newHostFactory)
        {
            locator.NotNull(nameof(locator));
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.HostString);

            return locator.ChangeHost(newHost);
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <param name="newPathFactory">给定的新路径工厂方法。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore ChangePath(this UriCombinerCore locator, Func<PathString, PathString> newPathFactory)
        {
            locator.NotNull(nameof(locator));
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.PathString);

            return locator.ChangePath(newPath);
        }

        /// <summary>
        /// 改变查询字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <param name="newQueryFactory">给定的新查询工厂方法。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore ChangeQuery(this UriCombinerCore locator, Func<QueryString, QueryString> newQueryFactory)
        {
            locator.NotNull(nameof(locator));
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.QueryString);

            return locator.ChangeQuery(newQuery);
        }


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="UriCombinerCore"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <param name="newHostFactory">给定的新主机工厂方法。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore NewHost(this UriCombinerCore locator, Func<HostString, HostString> newHostFactory)
        {
            locator.NotNull(nameof(locator));
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.HostString);

            return locator.NewHost(newHost);
        }

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="UriCombinerCore"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <param name="newPathFactory">给定的新路径工厂方法。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore NewPath(this UriCombinerCore locator, Func<PathString, PathString> newPathFactory)
        {
            locator.NotNull(nameof(locator));
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.PathString);

            return locator.NewPath(newPath);
        }

        /// <summary>
        /// 使用指定的新查询字符串新建一个 <see cref="UriCombinerCore"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <param name="newQueryFactory">给定的新查询工厂方法。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public static UriCombinerCore NewQuery(this UriCombinerCore locator, Func<QueryString, QueryString> newQueryFactory)
        {
            locator.NotNull(nameof(locator));
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.QueryString);

            return locator.NewQuery(newQuery);
        }

    }
}