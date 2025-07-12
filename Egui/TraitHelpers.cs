namespace Egui;

/// <summary>
/// Method definitions for serialization.
/// </summary>
static partial class TraitHelpers
{
    public static void serialize_option_UiStack(UiStack? value, Serde.ISerializer serializer) {
        if (value is not null) {
            serializer.serialize_option_tag(true);
            (value ?? default).Serialize(serializer);
        } else {
            serializer.serialize_option_tag(false);
        }
    }

    public static UiStack? deserialize_option_UiStack(Serde.IDeserializer deserializer) {
        bool tag = deserializer.deserialize_option_tag();
        if (!tag) {
            return null;
        } else {
            return UiStack.Deserialize(deserializer);
        }
    }
}