using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Egui;

/// <summary>
/// A callback that may be passed to unmanaged code.
/// </summary>
internal unsafe partial struct EguiCallback : IDisposable
{
    /// <summary>
    /// The last exception that occurred.
    /// </summary>
    [ThreadStatic]
    private static ExceptionDispatchInfo? _lastException;

    /// <summary>
    /// Creates a new object for invoking the given callback.
    /// </summary>
    /// <param name="callback">The callback to invoke.</param>
    public EguiCallback(Action<nuint> callback)
    {
        func = &InvokeCallback;
        data = (void*)(nint)GCHandle.Alloc(callback);
    }

    /// <inheritdoc/>
    void IDisposable.Dispose()
    {
        GCHandle.FromIntPtr((nint)data).Free();

        if (_lastException is not null)
        {
            var last = _lastException;
            _lastException = null;
            last.Throw();
        }
    }

    /// <summary>
    /// Invokes a C# callback.
    /// </summary>
    /// <param name="callback">A GC handle to the callback that should be invoked.</param>
    /// <param name="data">The data to provide to the callback.</param>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void InvokeCallback(void* argument, void* data)
    {
        try
        {
            var action = (Action<nuint>)GCHandle.FromIntPtr((nint)data).Target!;
            action((nuint)argument);
        }
        catch (Exception e)
        {
            _lastException = ExceptionDispatchInfo.Capture(e);
        }
    }

    /// <summary>
    /// Serializes an instance of this value.
    /// </summary>
    /// <param name="serializer">The serializer to use.</param>
    internal static void Serialize(BincodeSerializer serializer, EguiCallback obj)
    {
        serializer.increase_container_depth();
        serializer.serialize_u64((ulong)obj.func);
        serializer.serialize_u64((ulong)obj.data);
        serializer.decrease_container_depth();
    }

    /// <summary>
    /// Deserializes an instance of this value.
    /// </summary>
    /// <param name="deserializer">The deserializer to use.</param>
    /// <returns>The object that was deserialized.</returns>
    internal static EguiCallback Deserialize(BincodeDeserializer deserializer)
    {
        deserializer.increase_container_depth();
        EguiCallback obj = default;
        obj.func = (delegate* unmanaged[Cdecl]<void*, void*, void>)deserializer.deserialize_u64();
        obj.data = (void*)deserializer.deserialize_u64();
        deserializer.decrease_container_depth();
        return obj;
    }
}