namespace Egui;

/// <summary>
/// Used to paint images.
/// An image is pixels stored in RAM, and represented using <see cref="ImageData"/>. Before you can paint it however, you need to convert it to a texture.
/// If you are using egui, use <see cref="Context.LoadTexture"/> .
/// The <see cref="TextureHandle"/> can be cloned cheaply. When the last <see cref="TextureHandle"/> for specific texture is dropped, the texture is freed.
/// See also <see cref="TextureManager"/>.
/// </summary>
public sealed partial class TextureHandle : EguiObject
{
    /// <summary>
    /// Creates a texture handle for the given underlying object.
    /// </summary>
    internal TextureHandle(EguiHandle handle) : base(handle) { }
}