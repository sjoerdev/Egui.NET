using System.ComponentModel;

namespace Egui;

/// <exclude/>
[EditorBrowsable(EditorBrowsableState.Never)]
public class EguiObject
{
    /// <summary>
    /// The underlying handle that represents the heap-allocated Rust object.
    /// </summary>
    internal readonly EguiHandle Handle;

    /// <summary>
    /// Creates a new <c>egui</c> object.
    /// </summary>
    /// <param name="handle">The handle representing the object.</param>
    internal EguiObject(EguiHandle handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Drops the handle.
    /// </summary>
    ~EguiObject()
    {
        EguiBindings.egui_drop(Handle);
    }
}