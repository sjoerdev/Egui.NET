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

    public static void Call(EguiFn func, nuint handle)
    {
        Call<NoArgument>(func, handle, default);
    }

    public static R Call<R>(EguiFn func, nuint handle)
    {
        return Call<NoArgument, R>(func, handle, default);
    }

    public static void Call<A0>(EguiFn func, nuint handle, A0 arg0)
    {
        Call<A0, NoArgument>(func, handle, arg0, default);
    }

    public static R Call<A0, R>(EguiFn func, nuint handle, A0 arg0)
    {
        return Call<A0, NoArgument, R>(func, handle, arg0, default);
    }

    public static void Call<A0, A1>(EguiFn func, nuint handle, A0 arg0, A1 arg1)
    {
        Call<A0, A1, NoArgument>(func, handle, arg0, arg1, default);
    }

    public static R Call<A0, A1, R>(EguiFn func, nuint handle, A0 arg0, A1 arg1)
    {
        return Call<A0, A1, NoArgument, R>(func, handle, arg0, arg1, default);
    }

    public static void Call<A0, A1, A2>(EguiFn func, nuint handle, A0 arg0, A1 arg1, A2 arg2)
    {
        Call<A0, A1, A2, NoArgument>(func, handle, arg0, arg1, arg2, default);
    }

    public static R Call<A0, A1, A2, R>(EguiFn func, nuint handle, A0 arg0, A1 arg1, A2 arg2)
    {
        return Call<A0, A1, A2, NoArgument, R>(func, handle, arg0, arg1, arg2, default);
    }

    public static void Call<A0, A1, A2, A3>(EguiFn func, nuint handle, A0 arg0, A1 arg1, A2 arg2, A3 arg3)
    {
        Call<A0, A1, A2, A3, NoArgument>(func, handle, arg0, arg1, arg2, arg3, default);
    }

    public static R Call<A0, A1, A2, A3, R>(EguiFn func, nuint handle, A0 arg0, A1 arg1, A2 arg2, A3 arg3)
    {
        return Call<A0, A1, A2, A3, NoArgument, R>(func, handle, arg0, arg1, arg2, arg3, default);
    }

    public static void Call<A0, A1, A2, A3, A4>(EguiFn func, nuint handle, A0 arg0, A1 arg1, A2 arg2, A3 arg3, A4 arg4)
    {
        unsafe
        {
            var serializer = GetSerializer();
            SerializerCache<A0>.Serializer(serializer, arg0);
            SerializerCache<A1>.Serializer(serializer, arg1);
            SerializerCache<A2>.Serializer(serializer, arg2);
            SerializerCache<A3>.Serializer(serializer, arg3);

            var bytes = serializer.get_bytes();
            fixed (byte* ptr = bytes)
            {
                var result = EguiBindings.egui_invoke(func, handle, new EguiSliceU8
                {
                    ptr = ptr,
                    len = (nuint)bytes.Length
                });

                AssertSuccess(result);
            }
        }
    }

    public static R Call<A0, A1, A2, A3, A4, R>(EguiFn func, nuint handle, A0 arg0, A1 arg1, A2 arg2, A3 arg3, A4 arg4)
    {
        unsafe
        {
            var serializer = GetSerializer();
            SerializerCache<A0>.Serializer(serializer, arg0);
            SerializerCache<A1>.Serializer(serializer, arg1);
            SerializerCache<A2>.Serializer(serializer, arg2);
            SerializerCache<A3>.Serializer(serializer, arg3);
            SerializerCache<A4>.Serializer(serializer, arg4);

            var bytes = serializer.get_bytes();
            fixed (byte* ptr = bytes)
            {
                var result = EguiBindings.egui_invoke(func, handle, new EguiSliceU8
                {
                    ptr = ptr,
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
        return SerializerCache<R>.Deserializer(deserializer);
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
    /// Caches serialization and deserialization methods for a type.
    /// </summary>
    /// <typeparam name="T">The type to cache.</typeparam>
    private static class SerializerCache<T>
    {
        /// <summary>
        /// The serialization function to use.
        /// </summary>
        public static readonly Action<ISerializer, T> Serializer;

        /// <summary>
        /// The deserialization function to use.
        /// </summary>
        public static readonly Func<IDeserializer, T> Deserializer;

        /// <summary>
        /// Initializes the serialization methods.
        /// </summary>
        static SerializerCache()
        {
            if (typeof(T) == typeof(NoArgument))
            {
                Serializer = (_, _) => { };
                Deserializer = _ => default!;
            }
            else if (typeof(T).IsEnum)
            {
                Serializer = (serializer, value) =>
                {
                    serializer.increase_container_depth();
                    serializer.serialize_variant_index((int)(object)value!);
                    serializer.decrease_container_depth();
                };
                Deserializer = deserializer =>
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
            else if (typeof(T) == typeof(bool))
            {
                Serializer = (serializer, value) => serializer.serialize_bool((bool)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_bool();
            }
            else if (typeof(T) == typeof(char))
            {
                Serializer = (serializer, value) => serializer.serialize_char((char)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_char();
            }
            else if (typeof(T) == typeof(byte))
            {
                Serializer = (serializer, value) => serializer.serialize_u8((byte)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_u8();
            }
            else if (typeof(T) == typeof(ushort))
            {
                Serializer = (serializer, value) => serializer.serialize_u16((ushort)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_u16();
            }
            else if (typeof(T) == typeof(uint))
            {
                Serializer = (serializer, value) => serializer.serialize_u32((uint)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_u32();
            }
            else if (typeof(T) == typeof(ulong))
            {
                Serializer = (serializer, value) => serializer.serialize_u64((ulong)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_u64();
            }
            else if (typeof(T) == typeof(UInt128))
            {
                Serializer = (serializer, value) => serializer.serialize_u128((UInt128)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_u128();
            }
            else if (typeof(T) == typeof(sbyte))
            {
                Serializer = (serializer, value) => serializer.serialize_i8((sbyte)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_i8();
            }
            else if (typeof(T) == typeof(short))
            {
                Serializer = (serializer, value) => serializer.serialize_i16((short)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_i16();
            }
            else if (typeof(T) == typeof(int))
            {
                Serializer = (serializer, value) => serializer.serialize_i32((int)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_i32();
            }
            else if (typeof(T) == typeof(long))
            {
                Serializer = (serializer, value) => serializer.serialize_i64((long)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_i64();
            }
            else if (typeof(T) == typeof(Int128))
            {
                Serializer = (serializer, value) => serializer.serialize_i128((Int128)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_i128();
            }
            else if (typeof(T) == typeof(float))
            {
                Serializer = (serializer, value) => serializer.serialize_f32((float)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_f32();
            }
            else if (typeof(T) == typeof(double))
            {
                Serializer = (serializer, value) => serializer.serialize_f64((double)(object)value!);
                Deserializer = deserializer => (T)(object)deserializer.deserialize_f64();
            }
            else
            {
                if (typeof(T).GetMethod("Serialize", BindingFlags.Static | BindingFlags.NonPublic) == null)
                {
                    Console.WriteLine($"hit it for ty {typeof(T)}");
                }
                Serializer = (Action<ISerializer, T>)Delegate.CreateDelegate(typeof(Action<ISerializer, T>), typeof(T).GetMethod("Serialize", BindingFlags.Static | BindingFlags.NonPublic)!);
                Deserializer = (Func<IDeserializer, T>)Delegate.CreateDelegate(typeof(Func<IDeserializer, T>), typeof(T).GetMethod("Deserialize", BindingFlags.Static | BindingFlags.NonPublic)!);
            }
        }
    }

    /// <summary>
    /// Marker struct indicating that this is an extra argument.
    /// </summary>
    private struct NoArgument
    {

    }
}