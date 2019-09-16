﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Api
{
    class GraphApiQuery : ObjectGraphType, IGraphApiQuery
    {
        public GraphApiQuery()
        {
            Name = nameof(ISchema.Query);

            Field<StringGraphType>
            (
                name: "hello",
                resolve: context => "Librame"
            );
        }

    }
}