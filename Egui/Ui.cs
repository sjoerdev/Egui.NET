namespace Egui;

/// <summary>
/// This is what you use to place widgets.
/// Represents a region of the screen with a type of layout (horizontal or vertical).
/// </summary>
public readonly ref partial struct Ui
{
    /// <summary>
    /// Get a reference to the parent <see cref="Context"/>. 
    /// </summary>
    public readonly Context Ctx;

    /// <summary>
    /// A pointer to the underlying UI object.
    /// </summary>
    private readonly nuint _ptr;

    /// <summary>
    /// Creates a UI reference.
    /// </summary>
    /// <param name="ctx">The context that owns the UI.</param>
    /// <param name="ptr">A pointer to the UI.</param>
    internal Ui(Context ctx, nuint ptr)
    {
        Ctx = ctx;
        _ptr = ptr;
    }
}