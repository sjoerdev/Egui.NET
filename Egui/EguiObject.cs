using System.ComponentModel;

namespace Egui;

/// <exclude/>
[EditorBrowsable(EditorBrowsableState.Never)]
public class EguiObject
{
    /// <summary>
    /// A pointer to the underlying object pointer.
    /// </summary>
    internal nuint Ptr => _handle.ptr;

    /// <summary>
    /// The underlying handle that represents the heap-allocated Rust object.
    /// </summary>
    private readonly EguiHandle _handle;

    /// <summary>
    /// Creates a new <c>egui</c> object.
    /// </summary>
    /// <param name="handle">The handle representing the object.</param>
    internal EguiObject(EguiHandle handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Drops the handle.
    /// </summary>
    ~EguiObject()
    {
        EguiBindings.egui_drop(_handle);
    }
}