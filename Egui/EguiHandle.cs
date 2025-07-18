namespace Egui;

internal class EguiHandle
{
    /// <summary>
    /// The underlying handle that represents the heap-allocated Rust object.
    /// </summary>
    protected readonly nuint _handle;

    /// <summary>
    /// Drops the handle.
    /// </summary>
    ~EguiHandle()
    {

    }
}