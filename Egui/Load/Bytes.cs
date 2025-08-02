using System.Collections.Immutable;

namespace Egui.Load;

public partial struct Bytes
{
    /// <summary>
    /// Creates a new bytes object for the given data.
    /// </summary>
    public Bytes(ImmutableList<byte> value)
    {
        _value = value;
    }
}