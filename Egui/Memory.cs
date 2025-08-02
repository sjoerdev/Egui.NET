using System.Collections.Immutable;

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
    /// Global egui options.
    /// </summary>
    public Options Options
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<nuint, Options>(EguiFn.egui_memory_Memory_options, Ptr);
        }
        set
        {
            AssertInitialized();
            EguiMarshal.Call(EguiFn.egui_memory_Memory_set_options, Ptr, value);
        }
    }

    /// <summary>
    /// An iterator over all layers. Back-to-front, top is last.
    /// </summary>
    public IEnumerable<LayerId> LayerIds
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<nuint, ImmutableList<LayerId>>(EguiFn.egui_memory_Memory_layer_ids, Ptr);
        }
    }

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

    /// <summary>
    /// Throws an exception if this is a null object.
    /// </summary>
    internal readonly void AssertInitialized()
    {
        if (Ptr == 0) { throw new NullReferenceException("Memory instance was uninitialized"); }
    }
}