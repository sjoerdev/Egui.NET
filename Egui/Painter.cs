namespace Egui;

/// <summary>
/// Helper to paint shapes and text to a specific region on a specific layer.
// All coordinates are screen coordinates in the unit points (one point can consist of many physical pixels).
/// A <see cref="Painter"/>  never outlive a single frame/pass.
/// </summary>
public sealed partial class Painter : EguiObject
{
    /// <summary>
    /// Get a reference to the parent <see cref="Context"/>. 
    /// </summary>
    public readonly Context Ctx;

    /// <summary>
    /// Creates a new painter object from the given handle.
    /// </summary>
    /// <param name="handle">The handle to use.</param>
    internal Painter(Context ctx, EguiHandle handle) : base(handle)
    {
        Ctx = ctx;
    }
}