namespace Egui.Widgets;

public partial struct Image
{
    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator Image(ImageSource source) => new Image(source);
}