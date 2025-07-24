namespace Epaint;

/// <summary>
/// Low-level manager for allocating textures.<br/>
/// Communicates with the painting subsystem using <see cref="TakeDelta"/> 
/// </summary>
public sealed partial class TextureManager : EguiObject
{
    /// <summary>
    /// Creates a texture manager for the given handle.
    /// </summary>
    internal TextureManager(EguiHandle handle) : base(handle) { }
}