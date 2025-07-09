using Bincode;

namespace Egui;

internal static class EguiMarshal
{
    /// <summary>
    /// The serializer to use for temporary operations.
    /// </summary>
    [ThreadStatic]
    private static BincodeSerializer? _serializer;

    public static R Call<R>(EguiFn func)
    {
        Egui.Egui.invoke()
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
}