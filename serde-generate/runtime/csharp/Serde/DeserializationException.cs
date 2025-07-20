// Copyright (c) Facebook, Inc. and its affiliates
// SPDX-License-Identifier: MIT OR Apache-2.0

using System;

namespace Serde
{
    internal sealed class DeserializationException : Exception
    {
        public DeserializationException(string message) : base(message) { }
    }
}
