namespace Egui.Text;

/// <summary>
/// The collection of fonts used by epaint.
/// Required in order to paint text. Create one and reuse. Cheap to clone.
/// Each Fonts comes with a font atlas textures that needs to be used when painting.
/// If you are using egui, use <see cref="Context.SetFonts"/> and <see cref="Context.Fonts"/> .
/// You need to call <see cref="BeginPass"/> and <see cref="FontImageDelta"/> once every frame.
/// </summary>
public ref partial struct Fonts
{
    /// <summary>
    /// A pointer to the underlying fonts object.
    /// </summary>
    internal readonly nuint Ptr;

    /// <summary>
    /// Initializes this object.
    /// </summary>
    /// <param name="ptr">The pointer representing the object.</param>
    internal Fonts(nuint ptr)
    {
        Ptr = ptr;
    }
}