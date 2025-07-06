#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {
    static class TraitHelpers {
        public static void serialize_array2_Align_array(ReadOnlyMemory<Align> value, Serde.ISerializer serializer) {
            if (value.Length != 2) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 2);
            }
            foreach (var item in value) {
                item.Serialize(serializer);
            }
        }

        public static ReadOnlyMemory<Align> deserialize_array2_Align_array(Serde.IDeserializer deserializer) {
            Align[] obj = new Align[2];
            for (int i = 0; i < 2; i++) {
                obj[i] = AlignExtensions.Deserialize(deserializer);
            }
            return new ReadOnlyMemory<Align>(obj);
        }

        public static void serialize_array2_f64_array(ReadOnlyMemory<double> value, Serde.ISerializer serializer) {
            if (value.Length != 2) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 2);
            }
            foreach (var item in value) {
                serializer.serialize_f64(item);
            }
        }

        public static ReadOnlyMemory<double> deserialize_array2_f64_array(Serde.IDeserializer deserializer) {
            double[] obj = new double[2];
            for (int i = 0; i < 2; i++) {
                obj[i] = deserializer.deserialize_f64();
            }
            return new ReadOnlyMemory<double>(obj);
        }

        public static void serialize_array2_i8_array(ReadOnlyMemory<sbyte> value, Serde.ISerializer serializer) {
            if (value.Length != 2) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 2);
            }
            foreach (var item in value) {
                serializer.serialize_i8(item);
            }
        }

        public static ReadOnlyMemory<sbyte> deserialize_array2_i8_array(Serde.IDeserializer deserializer) {
            sbyte[] obj = new sbyte[2];
            for (int i = 0; i < 2; i++) {
                obj[i] = deserializer.deserialize_i8();
            }
            return new ReadOnlyMemory<sbyte>(obj);
        }

        public static void serialize_array2_option_ScrollingToTarget_array(ReadOnlyMemory<ScrollingToTarget?> value, Serde.ISerializer serializer) {
            if (value.Length != 2) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 2);
            }
            foreach (var item in value) {
                TraitHelpers.serialize_option_ScrollingToTarget(item, serializer);
            }
        }

        public static ReadOnlyMemory<ScrollingToTarget?> deserialize_array2_option_ScrollingToTarget_array(Serde.IDeserializer deserializer) {
            ScrollingToTarget?[] obj = new ScrollingToTarget?[2];
            for (int i = 0; i < 2; i++) {
                obj[i] = TraitHelpers.deserialize_option_ScrollingToTarget(deserializer);
            }
            return new ReadOnlyMemory<ScrollingToTarget?>(obj);
        }

        public static void serialize_array2_option_f32_array(ReadOnlyMemory<float?> value, Serde.ISerializer serializer) {
            if (value.Length != 2) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 2);
            }
            foreach (var item in value) {
                TraitHelpers.serialize_option_f32(item, serializer);
            }
        }

        public static ReadOnlyMemory<float?> deserialize_array2_option_f32_array(Serde.IDeserializer deserializer) {
            float?[] obj = new float?[2];
            for (int i = 0; i < 2; i++) {
                obj[i] = TraitHelpers.deserialize_option_f32(deserializer);
            }
            return new ReadOnlyMemory<float?>(obj);
        }

        public static void serialize_array2_u64_array(ReadOnlyMemory<ulong> value, Serde.ISerializer serializer) {
            if (value.Length != 2) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 2);
            }
            foreach (var item in value) {
                serializer.serialize_u64(item);
            }
        }

        public static ReadOnlyMemory<ulong> deserialize_array2_u64_array(Serde.IDeserializer deserializer) {
            ulong[] obj = new ulong[2];
            for (int i = 0; i < 2; i++) {
                obj[i] = deserializer.deserialize_u64();
            }
            return new ReadOnlyMemory<ulong>(obj);
        }

        public static void serialize_array4_u8_array(ReadOnlyMemory<byte> value, Serde.ISerializer serializer) {
            if (value.Length != 4) {
                throw new Serde.SerializationException("Invalid length for fixed-size array: " + value.Length + " instead of " + 4);
            }
            foreach (var item in value) {
                serializer.serialize_u8(item);
            }
        }

        public static ReadOnlyMemory<byte> deserialize_array4_u8_array(Serde.IDeserializer deserializer) {
            byte[] obj = new byte[4];
            for (int i = 0; i < 4; i++) {
                obj[i] = deserializer.deserialize_u8();
            }
            return new ReadOnlyMemory<byte>(obj);
        }

        public static void serialize_map_TextStyle_to_FontId(Serde.ValueDictionary<TextStyle, FontId> value, Serde.ISerializer serializer) {
            serializer.serialize_len(value.Length);
            int[] offsets = new int[value.Length];
            int count = 0;
            foreach (KeyValuePair<TextStyle, FontId> entry in value) {
                offsets[count++] = serializer.get_buffer_offset();
                entry.Key.Serialize(serializer);
                entry.Value.Serialize(serializer);
            }
            serializer.sort_map_entries(offsets);
        }

        public static Serde.ValueDictionary<TextStyle, FontId> deserialize_map_TextStyle_to_FontId(Serde.IDeserializer deserializer) {
            long length = deserializer.deserialize_len();
            var obj = new Dictionary<TextStyle, FontId>();
            int previous_key_start = 0;
            int previous_key_end = 0;
            for (long i = 0; i < length; i++) {
                int key_start = deserializer.get_buffer_offset();
                var key = TextStyle.Deserialize(deserializer);
                int key_end = deserializer.get_buffer_offset();
                if (i > 0) {
                    deserializer.check_that_key_slices_are_increasing(
                        new Serde.Range(previous_key_start, previous_key_end),
                        new Serde.Range(key_start, key_end));
                }
                previous_key_start = key_start;
                previous_key_end = key_end;
                var value = FontId.Deserialize(deserializer);
                obj[key] = value;
            }
            return new Serde.ValueDictionary<TextStyle, FontId>(obj);
        }

        public static void serialize_option_Align(Align? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static Align? deserialize_option_Align(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return AlignExtensions.Deserialize(deserializer);
            }
        }

        public static void serialize_option_CCursorRange(CCursorRange? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static CCursorRange? deserialize_option_CCursorRange(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return CCursorRange.Deserialize(deserializer);
            }
        }

        public static void serialize_option_Color32(Color32? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static Color32? deserialize_option_Color32(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return Color32.Deserialize(deserializer);
            }
        }

        public static void serialize_option_CursorIcon(CursorIcon? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static CursorIcon? deserialize_option_CursorIcon(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return CursorIconExtensions.Deserialize(deserializer);
            }
        }

        public static void serialize_option_CursorRange(CursorRange? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static CursorRange? deserialize_option_CursorRange(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return CursorRange.Deserialize(deserializer);
            }
        }

        public static void serialize_option_FontId(FontId? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static FontId? deserialize_option_FontId(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return FontId.Deserialize(deserializer);
            }
        }

        public static void serialize_option_IMEOutput(IMEOutput? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static IMEOutput? deserialize_option_IMEOutput(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return IMEOutput.Deserialize(deserializer);
            }
        }

        public static void serialize_option_OpenUrl(OpenUrl? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static OpenUrl? deserialize_option_OpenUrl(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return OpenUrl.Deserialize(deserializer);
            }
        }

        public static void serialize_option_Pos2(Pos2? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static Pos2? deserialize_option_Pos2(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return Pos2.Deserialize(deserializer);
            }
        }

        public static void serialize_option_RangeInclusive(RangeInclusive? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static RangeInclusive? deserialize_option_RangeInclusive(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return RangeInclusive.Deserialize(deserializer);
            }
        }

        public static void serialize_option_Rect(Rect? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static Rect? deserialize_option_Rect(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return Rect.Deserialize(deserializer);
            }
        }

        public static void serialize_option_ScrollingToTarget(ScrollingToTarget? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static ScrollingToTarget? deserialize_option_ScrollingToTarget(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return ScrollingToTarget.Deserialize(deserializer);
            }
        }

        public static void serialize_option_SystemTime(SystemTime? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static SystemTime? deserialize_option_SystemTime(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return SystemTime.Deserialize(deserializer);
            }
        }

        public static void serialize_option_TextStyle(TextStyle? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static TextStyle? deserialize_option_TextStyle(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return TextStyle.Deserialize(deserializer);
            }
        }

        public static void serialize_option_TextWrapMode(TextWrapMode? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static TextWrapMode? deserialize_option_TextWrapMode(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return TextWrapModeExtensions.Deserialize(deserializer);
            }
        }

        public static void serialize_option_Vec2(Vec2? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static Vec2? deserialize_option_Vec2(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return Vec2.Deserialize(deserializer);
            }
        }

        public static void serialize_option_ViewportId(ViewportId? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                value.Value.Serialize(serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static ViewportId? deserialize_option_ViewportId(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return ViewportId.Deserialize(deserializer);
            }
        }

        public static void serialize_option_bool(bool? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                serializer.serialize_bool(value.Value);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static bool? deserialize_option_bool(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return deserializer.deserialize_bool();
            }
        }

        public static void serialize_option_f32(float? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                serializer.serialize_f32(value.Value);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static float? deserialize_option_f32(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return deserializer.deserialize_f32();
            }
        }

        public static void serialize_option_f64(double? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                serializer.serialize_f64(value.Value);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static double? deserialize_option_f64(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return deserializer.deserialize_f64();
            }
        }

        public static void serialize_option_str(string? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                serializer.serialize_str(value.Value);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static string? deserialize_option_str(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return deserializer.deserialize_str();
            }
        }

        public static void serialize_option_tuple2_Rot2_Vec2((Rot2, Vec2)? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                TraitHelpers.serialize_tuple2_Rot2_Vec2(value.Value, serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static (Rot2, Vec2)? deserialize_option_tuple2_Rot2_Vec2(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return TraitHelpers.deserialize_tuple2_Rot2_Vec2(deserializer);
            }
        }

        public static void serialize_option_vector_u8(ReadOnlyMemory<byte>? value, Serde.ISerializer serializer) {
            if (value is not null) {
                serializer.serialize_option_tag(true);
                TraitHelpers.serialize_vector_u8(value.Value, serializer);
            } else {
                serializer.serialize_option_tag(false);
            }
        }

        public static ReadOnlyMemory<byte>? deserialize_option_vector_u8(Serde.IDeserializer deserializer) {
            bool tag = deserializer.deserialize_option_tag();
            if (!tag) {
                return null;
            } else {
                return TraitHelpers.deserialize_vector_u8(deserializer);
            }
        }

        public static void serialize_tuple2_Rot2_Vec2((Rot2, Vec2) value, Serde.ISerializer serializer) {
            value.Item1.Serialize(serializer);
            value.Item2.Serialize(serializer);
        }

        public static (Rot2, Vec2) deserialize_tuple2_Rot2_Vec2(Serde.IDeserializer deserializer) {
            return (
                Rot2.Deserialize(deserializer),
                Vec2.Deserialize(deserializer)
            );
        }

        public static void serialize_vector_Color32(ReadOnlyMemory<Color32> value, Serde.ISerializer serializer) {
            serializer.serialize_len(value.Length);
            foreach (var item in value) {
                item.Serialize(serializer);
            }
        }

        public static ReadOnlyMemory<Color32> deserialize_vector_Color32(Serde.IDeserializer deserializer) {
            long length = deserializer.deserialize_len();
            Color32[] obj = new Color32[length];
            for (int i = 0; i < length; i++) {
                obj[i] = Color32.Deserialize(deserializer);
            }
            return new ReadOnlyMemory<Color32>(obj);
        }

        public static void serialize_vector_OutputCommand(ReadOnlyMemory<OutputCommand> value, Serde.ISerializer serializer) {
            serializer.serialize_len(value.Length);
            foreach (var item in value) {
                item.Serialize(serializer);
            }
        }

        public static ReadOnlyMemory<OutputCommand> deserialize_vector_OutputCommand(Serde.IDeserializer deserializer) {
            long length = deserializer.deserialize_len();
            OutputCommand[] obj = new OutputCommand[length];
            for (int i = 0; i < length; i++) {
                obj[i] = OutputCommand.Deserialize(deserializer);
            }
            return new ReadOnlyMemory<OutputCommand>(obj);
        }

        public static void serialize_vector_OutputEvent(ReadOnlyMemory<OutputEvent> value, Serde.ISerializer serializer) {
            serializer.serialize_len(value.Length);
            foreach (var item in value) {
                item.Serialize(serializer);
            }
        }

        public static ReadOnlyMemory<OutputEvent> deserialize_vector_OutputEvent(Serde.IDeserializer deserializer) {
            long length = deserializer.deserialize_len();
            OutputEvent[] obj = new OutputEvent[length];
            for (int i = 0; i < length; i++) {
                obj[i] = OutputEvent.Deserialize(deserializer);
            }
            return new ReadOnlyMemory<OutputEvent>(obj);
        }

        public static void serialize_vector_ViewportEvent(ReadOnlyMemory<ViewportEvent> value, Serde.ISerializer serializer) {
            serializer.serialize_len(value.Length);
            foreach (var item in value) {
                item.Serialize(serializer);
            }
        }

        public static ReadOnlyMemory<ViewportEvent> deserialize_vector_ViewportEvent(Serde.IDeserializer deserializer) {
            long length = deserializer.deserialize_len();
            ViewportEvent[] obj = new ViewportEvent[length];
            for (int i = 0; i < length; i++) {
                obj[i] = ViewportEventExtensions.Deserialize(deserializer);
            }
            return new ReadOnlyMemory<ViewportEvent>(obj);
        }

        public static void serialize_vector_u8(ReadOnlyMemory<byte> value, Serde.ISerializer serializer) {
            serializer.serialize_len(value.Length);
            foreach (var item in value) {
                serializer.serialize_u8(item);
            }
        }

        public static ReadOnlyMemory<byte> deserialize_vector_u8(Serde.IDeserializer deserializer) {
            long length = deserializer.deserialize_len();
            byte[] obj = new byte[length];
            for (int i = 0; i < length; i++) {
                obj[i] = deserializer.deserialize_u8();
            }
            return new ReadOnlyMemory<byte>(obj);
        }

    }


} // end of namespace Egui
