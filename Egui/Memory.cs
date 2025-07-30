namespace Egui;

/// <summary>
/// The data that egui persists between frames.<br/>
/// This includes window positions and sizes, how far the user has scrolled in a <see cref="ScrollArea"/> etc.<br/>
/// If you want this to persist when closing your app, you should serialize <see cref="Memory"/> and store it. For this you need to enable the persistence.<br/>
/// If you want to store data for your widgets, you should look at <see cref="Data"/> 
/// </summary>
public ref partial struct Memory
{
    /// <summary>
    /// A pointer to the underlying UI object.
    /// </summary>
    internal readonly nuint Ptr;

    /// <summary>
    /// Creates a new memory object that references the given pointer.
    /// </summary>
    /// <param name="ptr">The native object pointer.</param>
    internal Memory(nuint ptr)
    {
        Ptr = ptr;
    }
}