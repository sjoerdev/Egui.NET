#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui;

/// <summary>
/// Information about a <c>Ui</c> and its parents.
///
/// <c>UiStack</c> serves to keep track of the current hierarchy of <c>Ui</c>s, such
/// that nested widgets or user code may adapt to the surrounding context or obtain layout information
/// from a <c>Ui</c> that might be several steps higher in the hierarchy.
///
/// Note: since <c>UiStack</c> contains a reference to its parent, it is both a stack, and a node within
/// that stack. Most of its methods are about the specific node, but some methods walk up the
/// hierarchy to provide information about the entire stack.
/// </summary>
public partial struct UiStack : IEquatable<UiStack> {
    public Id Id;
    public UiStackInfo Info;
    public Direction LayoutDirection;
    public Rect MinRect;
    public Rect MaxRect;
    public ReadOnlyBox<UiStack>? Parent;


    internal static void Serialize(Serde.ISerializer serializer, UiStack value) => value.Serialize(serializer);

    internal void Serialize(Serde.ISerializer serializer) {
        serializer.increase_container_depth();
        Id.Serialize(serializer);
        Info.Serialize(serializer);
        LayoutDirection.Serialize(serializer);
        MinRect.Serialize(serializer);
        MaxRect.Serialize(serializer);
        TraitHelpers.serialize_option_UiStack(Parent, serializer);
        serializer.decrease_container_depth();
    }

    internal static UiStack Deserialize(Serde.IDeserializer deserializer) {
        deserializer.increase_container_depth();
        UiStack obj = default;
            obj.Id = Id.Deserialize(deserializer);
            obj.Info = UiStackInfo.Deserialize(deserializer);
            obj.LayoutDirection = DirectionSerdeExtensions.Deserialize(deserializer);
            obj.MinRect = Rect.Deserialize(deserializer);
            obj.MaxRect = Rect.Deserialize(deserializer);
            obj.Parent = TraitHelpers.deserialize_option_UiStack(deserializer);
            
        deserializer.decrease_container_depth();
        return obj;
    }
    public override bool Equals(object? obj) => obj is UiStack other && Equals(other);

    public static bool operator ==(UiStack left, UiStack right) => Equals(left, right);

    public static bool operator !=(UiStack left, UiStack right) => !Equals(left, right);

    public bool Equals(UiStack other) {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (!Id.Equals(other.Id)) return false;
        if (!Info.Equals(other.Info)) return false;
        if (!LayoutDirection.Equals(other.LayoutDirection)) return false;
        if (!MinRect.Equals(other.MinRect)) return false;
        if (!MaxRect.Equals(other.MaxRect)) return false;
        if (!Parent.Equals(other.Parent)) return false;
        return true;
    }

    public override int GetHashCode() {
        unchecked {
            int value = 7;
            value = 31 * value + Id.GetHashCode();
            value = 31 * value + Info.GetHashCode();
            value = 31 * value + LayoutDirection.GetHashCode();
            value = 31 * value + MinRect.GetHashCode();
            value = 31 * value + MaxRect.GetHashCode();
            value = 31 * value + Parent.GetHashCode();
            return value;
        }
    }

}