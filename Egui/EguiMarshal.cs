using System.Reflection;
using Bincode;
using Serde;

namespace Egui;

/// <summary>
/// Manages C#-Rust interop and facilitates calling <c>egui</c> functions.
/// </summary>
internal static class EguiMarshal
{
    /// <summary>
    /// The serializer to use for temporary operations.
    /// </summary>
    [ThreadStatic]
    private static BincodeSerializer? _serializer;

    public static void Call(EguiFn func, nuint ptr)
    {
        Call<NoArgument>(func, ptr, default);
    }

    public static R Call<R>(EguiFn func, nuint ptr)
    {
        return Call<NoArgument, R>(func, ptr, default);
    }

    public static void Call<A0>(EguiFn func, nuint ptr, A0 arg0)
    {
        Call<A0, NoArgument>(func, ptr, arg0, default);
    }

    public static R Call<A0, R>(EguiFn func, nuint ptr, A0 arg0)
    {
        return Call<A0, NoArgument, R>(func, ptr, arg0, default);
    }

    public static void Call<A0, A1>(EguiFn func, nuint ptr, A0 arg0, A1 arg1)
    {
        Call<A0, A1, NoArgument>(func, ptr, arg0, arg1, default);
    }

    public static R Call<A0, A1, R>(EguiFn func, nuint ptr, A0 arg0, A1 arg1)
    {
        return Call<A0, A1, NoArgument, R>(func, ptr, arg0, arg1, default);
    }

    public static void Call<A0, A1, A2>(EguiFn func, nuint ptr, A0 arg0, A1 arg1, A2 arg2)
    {
        Call<A0, A1, A2, NoArgument>(func, ptr, arg0, arg1, arg2, default);
    }

    public static R Call<A0, A1, A2, R>(EguiFn func, nuint ptr, A0 arg0, A1 arg1, A2 arg2)
    {
        return Call<A0, A1, A2, NoArgument, R>(func, ptr, arg0, arg1, arg2, default);
    }

    public static void Call<A0, A1, A2, A3>(EguiFn func, nuint ptr, A0 arg0, A1 arg1, A2 arg2, A3 arg3)
    {
        Call<A0, A1, A2, A3, NoArgument>(func, ptr, arg0, arg1, arg2, arg3, default);
    }

    public static R Call<A0, A1, A2, A3, R>(EguiFn func, nuint ptr, A0 arg0, A1 arg1, A2 arg2, A3 arg3)
    {
        return Call<A0, A1, A2, A3, NoArgument, R>(func, ptr, arg0, arg1, arg2, arg3, default);
    }

    public static void Call<A0, A1, A2, A3, A4>(EguiFn func, nuint ptr, A0 arg0, A1 arg1, A2 arg2, A3 arg3, A4 arg4)
    {
        unsafe
        {
            var serializer = GetSerializer();
            SerializerCache<A0>.Serialize(serializer, arg0);
            SerializerCache<A1>.Serialize(serializer, arg1);
            SerializerCache<A2>.Serialize(serializer, arg2);
            SerializerCache<A3>.Serialize(serializer, arg3);

            var bytes = serializer.get_bytes();
            fixed (byte* bytePtr = bytes)
            {
                var result = EguiBindings.egui_invoke(func, ptr, new EguiSliceU8
                {
                    ptr = bytePtr,
                    len = (nuint)bytes.Length
                });

                AssertSuccess(result);
            }
        }
    }

    public static R Call<A0, A1, A2, A3, A4, R>(EguiFn func, nuint ptr, A0 arg0, A1 arg1, A2 arg2, A3 arg3, A4 arg4)
    {
        unsafe
        {
            var serializer = GetSerializer();
            SerializerCache<A0>.Serialize(serializer, arg0);
            SerializerCache<A1>.Serialize(serializer, arg1);
            SerializerCache<A2>.Serialize(serializer, arg2);
            SerializerCache<A3>.Serialize(serializer, arg3);
            SerializerCache<A4>.Serialize(serializer, arg4);

            var bytes = serializer.get_bytes();
            fixed (byte* bytePtr = bytes)
            {
                var result = EguiBindings.egui_invoke(func, ptr, new EguiSliceU8
                {
                    ptr = bytePtr,
                    len = (nuint)bytes.Length
                });

                return DeserializeResult<R>(result);
            }
        }
    }

    private unsafe static R DeserializeResult<R>(EguiInvokeResult result)
    {
        AssertSuccess(result);
        var deserializer = new BincodeDeserializer(new ReadOnlySpan<byte>(result.return_value.ptr, (int)result.return_value.len).ToArray());
        return SerializerCache<R>.Deserialize(deserializer);
    }

    private unsafe static void AssertSuccess(EguiInvokeResult result)
    {
        if (!result.success)
        {
            throw new InvalidOperationException(new string((char*)result.return_value.ptr, 0, (int)result.return_value.len / sizeof(char)));
        }
    }

    /// <summary>
    /// Obtains a serializer to use for temporary operations.
    /// The returned object is only valid until the next call to this function
    /// (because the underlying buffer is reused).
    /// </summary>
    private static BincodeSerializer GetSerializer()
    {
        if (_serializer is null)
        {
            _serializer = new BincodeSerializer();
        }

        _serializer.Reset();
        return _serializer;
    }

    /// <summary>
    /// Serializers for generic types.
    /// </summary>
    private static Dictionary<Type, (string, string)> SerializerPrototypes = new Dictionary<Type, (string, string)> {
        { typeof(ReadOnlyMemory<>), ("ReadOnlyMemorySerializer", "ReadOnlyMemoryDeserializer") },
        { typeof(Tuple<,>), ("TupleSerializer", "TupleDeserializer") }
    };

    /// <summary>
    /// Caches serialization and deserialization methods for a type.
    /// </summary>
    /// <typeparam name="T">The type to cache.</typeparam>
    private static class SerializerCache<T>
    {
        /// <summary>
        /// The serialization function to use.
        /// </summary>
        public static readonly Action<ISerializer, T> Serialize;

        /// <summary>
        /// The deserialization function to use.
        /// </summary>
        public static readonly Func<IDeserializer, T> Deserialize;

        /// <summary>
        /// Initializes the serialization methods.
        /// </summary>
        static SerializerCache()
        {
            if (typeof(T) == typeof(NoArgument))
            {
                Serialize = (_, _) => { };
                Deserialize = _ => default!;
            }
            else if (typeof(T).IsEnum)
            {
                Serialize = (serializer, value) =>
                {
                    serializer.increase_container_depth();
                    serializer.serialize_variant_index((int)(object)value!);
                    serializer.decrease_container_depth();
                };
                Deserialize = deserializer =>
                {
                    deserializer.increase_container_depth();
                    int index = deserializer.deserialize_variant_index();
                    if (!Enum.IsDefined(typeof(T), index))
                    {
                        throw new InvalidDataException($"Unknown variant index for {typeof(T)}: {index}");
                    }

                    deserializer.decrease_container_depth();
                    return (T)(object)index;
                };
            }
            else if (typeof(T) == typeof(string))
            {
                Serialize = (serializer, value) => serializer.serialize_str((string)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_str();
            }
            else if (typeof(T) == typeof(bool))
            {
                Serialize = (serializer, value) => serializer.serialize_bool((bool)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_bool();
            }
            else if (typeof(T) == typeof(char))
            {
                Serialize = (serializer, value) => serializer.serialize_char((char)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_char();
            }
            else if (typeof(T) == typeof(byte))
            {
                Serialize = (serializer, value) => serializer.serialize_u8((byte)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_u8();
            }
            else if (typeof(T) == typeof(ushort))
            {
                Serialize = (serializer, value) => serializer.serialize_u16((ushort)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_u16();
            }
            else if (typeof(T) == typeof(uint))
            {
                Serialize = (serializer, value) => serializer.serialize_u32((uint)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_u32();
            }
            else if (typeof(T) == typeof(ulong))
            {
                Serialize = (serializer, value) => serializer.serialize_u64((ulong)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_u64();
            }
            else if (typeof(T) == typeof(UInt128))
            {
                Serialize = (serializer, value) => serializer.serialize_u128((UInt128)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_u128();
            }
            else if (typeof(T) == typeof(sbyte))
            {
                Serialize = (serializer, value) => serializer.serialize_i8((sbyte)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_i8();
            }
            else if (typeof(T) == typeof(short))
            {
                Serialize = (serializer, value) => serializer.serialize_i16((short)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_i16();
            }
            else if (typeof(T) == typeof(int))
            {
                Serialize = (serializer, value) => serializer.serialize_i32((int)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_i32();
            }
            else if (typeof(T) == typeof(long))
            {
                Serialize = (serializer, value) => serializer.serialize_i64((long)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_i64();
            }
            else if (typeof(T) == typeof(Int128))
            {
                Serialize = (serializer, value) => serializer.serialize_i128((Int128)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_i128();
            }
            else if (typeof(T) == typeof(float))
            {
                Serialize = (serializer, value) => serializer.serialize_f32((float)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_f32();
            }
            else if (typeof(T) == typeof(double))
            {
                Serialize = (serializer, value) => serializer.serialize_f64((double)(object)value!);
                Deserialize = deserializer => (T)(object)deserializer.deserialize_f64();
            }
            else if (typeof(T).IsGenericType && SerializerPrototypes.TryGetValue(typeof(T).GetGenericTypeDefinition(), out var methods))
            {
                var genericArgs = typeof(T).GenericTypeArguments;
                var serializer = typeof(EguiMarshal).GetMethod(methods.Item1, BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(genericArgs);
                var deserializer = typeof(EguiMarshal).GetMethod(methods.Item2, BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(genericArgs);

                Serialize = (Action<ISerializer, T>)Delegate.CreateDelegate(typeof(Action<ISerializer, T>), serializer);
                Deserialize = (Func<IDeserializer, T>)Delegate.CreateDelegate(typeof(Func<IDeserializer, T>), deserializer);
            }
            else
            {
                var serializer = typeof(T).GetMethod("Serialize", BindingFlags.Static | BindingFlags.NonPublic)!;
                var deserializer = typeof(T).GetMethod("Deserialize", BindingFlags.Static | BindingFlags.NonPublic)!;

                if (serializer == null || deserializer == null)
                {
                    throw new Exception($"Missing serializers for {typeof(T)}");
                }
                Serialize = (Action<ISerializer, T>)Delegate.CreateDelegate(typeof(Action<ISerializer, T>), serializer);
                Deserialize = (Func<IDeserializer, T>)Delegate.CreateDelegate(typeof(Func<IDeserializer, T>), deserializer);
            }
        }
    }

    /// <summary>
    /// Serializes read-only memory.
    /// </summary>
    private static void ReadOnlyMemorySerializer<T>(ISerializer serializer, ReadOnlyMemory<T> value)
    {
        serializer.serialize_len(value.Length);
        foreach (var item in value.Span)
        {
            SerializerCache<T>.Serialize(serializer, item);
        }
    }

    /// <summary>
    /// Deserializes read-only memory.
    /// </summary>
    private static ReadOnlyMemory<T> ReadOnlyMemoryDeserializer<T>(IDeserializer deserializer)
    {
        var length = deserializer.deserialize_len();
        T[] obj = new T[length];
        for (int i = 0; i < length; i++)
        {
            obj[i] = SerializerCache<T>.Deserialize(deserializer);
        }
        return obj;
    }

    /// <summary>
    /// Serializes a tuple.
    /// </summary>
    private static void TupleSerializer<A0, A1>(ISerializer serializer, (A0, A1) value)
    {
        SerializerCache<A0>.Serialize(serializer, value.Item1);
        SerializerCache<A1>.Serialize(serializer, value.Item2);
    }

    /// <summary>
    /// Deserializes a tuple.
    /// </summary>
    private static (A0, A1) TupleDeserializer<A0, A1>(IDeserializer deserializer)
    {
        return (SerializerCache<A0>.Deserialize(deserializer), SerializerCache<A1>.Deserialize(deserializer));
    }

    /// <summary>
    /// Marker struct indicating that this is an extra argument.
    /// </summary>
    private struct NoArgument
    {

    }
}