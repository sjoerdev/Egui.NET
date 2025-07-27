namespace Egui.Viewport;

public partial struct ViewportId
{
    /// <summary>
    /// The <see cref="ViewportId"/> of the root viewport.
    /// </summary>
    public static readonly ViewportId Root = new ViewportId { _value = Id.Null };
}