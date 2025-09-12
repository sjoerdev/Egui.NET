// Copyright (c) Facebook, Inc. and its affiliates
// SPDX-License-Identifier: MIT OR Apache-2.0

using System;

namespace Egui
{
    ///<summary>
    /// Analogous to Rust's unit type.
    ///</summary>
    public readonly record struct Unit : IEquatable<Unit>
    {
    }
}
