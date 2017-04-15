﻿// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Cryptography;
using System.Text;

namespace Librame.Security.Hash
{
    class SHA384HashAlgorithm : HashAlgorithmBase, IAlgorithm
    {
        public SHA384HashAlgorithm(Encoding encoding = null)
            : base(encoding)
        {
        }


        protected override HashAlgorithm CreateAlgorithm()
        {
            return SHA384.Create();
        }

    }
}