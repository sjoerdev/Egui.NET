#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    /// <summary>
    /// Keyboard keys.
    ///
    /// egui usually uses logical keys, i.e. after applying any user keymap.\
    /// </summary>
    public enum Key {
            ArrowDown = 0,
            ArrowLeft = 1,
            ArrowRight = 2,
            ArrowUp = 3,
            Escape = 4,
            Tab = 5,
            Backspace = 6,
            Enter = 7,
            Space = 8,
            Insert = 9,
            Delete = 10,
            Home = 11,
            End = 12,
            PageUp = 13,
            PageDown = 14,
            Copy = 15,
            Cut = 16,
            Paste = 17,
            /// <summary>
            /// <c>:</c>
            /// </summary>
            Colon = 18,
            /// <summary>
            /// <c>,</c>
            /// </summary>
            Comma = 19,
            /// <summary>
            /// <c>\</c>
            /// </summary>
            Backslash = 20,
            /// <summary>
            /// <c>/</c>
            /// </summary>
            Slash = 21,
            /// <summary>
            /// <c>|</c>, a vertical bar
            /// </summary>
            Pipe = 22,
            /// <summary>
            /// <c>?</c>
            /// </summary>
            Questionmark = 23,
            Exclamationmark = 24,
            OpenBracket = 25,
            CloseBracket = 26,
            OpenCurlyBracket = 27,
            CloseCurlyBracket = 28,
            /// <summary>
            /// Also known as "backquote" or "grave"
            /// </summary>
            Backtick = 29,
            /// <summary>
            /// <c></c>
            /// </summary>
            Minus = 30,
            /// <summary>
            /// <c>.</c>
            /// </summary>
            Period = 31,
            /// <summary>
            /// <c>+</c>
            /// </summary>
            Plus = 32,
            /// <summary>
            /// <c>=</c>
            /// </summary>
            Equals = 33,
            /// <summary>
            /// <c>;</c>
            /// </summary>
            Semicolon = 34,
            /// <summary>
            /// <c>'</c>
            /// </summary>
            Quote = 35,
            /// <summary>
            /// <c>0</c> (from main row or numpad)
            /// </summary>
            Num0 = 36,
            /// <summary>
            /// <c>1</c> (from main row or numpad)
            /// </summary>
            Num1 = 37,
            /// <summary>
            /// <c>2</c> (from main row or numpad)
            /// </summary>
            Num2 = 38,
            /// <summary>
            /// <c>3</c> (from main row or numpad)
            /// </summary>
            Num3 = 39,
            /// <summary>
            /// <c>4</c> (from main row or numpad)
            /// </summary>
            Num4 = 40,
            /// <summary>
            /// <c>5</c> (from main row or numpad)
            /// </summary>
            Num5 = 41,
            /// <summary>
            /// <c>6</c> (from main row or numpad)
            /// </summary>
            Num6 = 42,
            /// <summary>
            /// <c>7</c> (from main row or numpad)
            /// </summary>
            Num7 = 43,
            /// <summary>
            /// <c>8</c> (from main row or numpad)
            /// </summary>
            Num8 = 44,
            /// <summary>
            /// <c>9</c> (from main row or numpad)
            /// </summary>
            Num9 = 45,
            A = 46,
            B = 47,
            C = 48,
            D = 49,
            E = 50,
            F = 51,
            G = 52,
            H = 53,
            I = 54,
            J = 55,
            K = 56,
            L = 57,
            M = 58,
            N = 59,
            O = 60,
            P = 61,
            Q = 62,
            R = 63,
            S = 64,
            T = 65,
            U = 66,
            V = 67,
            W = 68,
            X = 69,
            Y = 70,
            Z = 71,
            F1 = 72,
            F2 = 73,
            F3 = 74,
            F4 = 75,
            F5 = 76,
            F6 = 77,
            F7 = 78,
            F8 = 79,
            F9 = 80,
            F10 = 81,
            F11 = 82,
            F12 = 83,
            F13 = 84,
            F14 = 85,
            F15 = 86,
            F16 = 87,
            F17 = 88,
            F18 = 89,
            F19 = 90,
            F20 = 91,
            F21 = 92,
            F22 = 93,
            F23 = 94,
            F24 = 95,
            F25 = 96,
            F26 = 97,
            F27 = 98,
            F28 = 99,
            F29 = 100,
            F30 = 101,
            F31 = 102,
            F32 = 103,
            F33 = 104,
            F34 = 105,
            F35 = 106,
    }
    internal static class KeyExtensions {

        internal static void Serialize(this Key value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static Key Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(Key), index))
                throw new Serde.DeserializationException("Unknown variant index for Key: " + index);
            Key value = (Key)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
