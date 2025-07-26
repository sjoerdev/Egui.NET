namespace Egui;

public partial struct AtomKind
{
    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(string value) => new AtomKind.Text { Value = value };

    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(RichText value) => new AtomKind.Text { Value = value };

    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(WidgetText value) => new AtomKind.Text { Value = value };
    
    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(Egui.Text.LayoutJob value) => new AtomKind.Text { Value = value };
    
    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(Egui.Galley value) => new AtomKind.Text { Value = value };

    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(ImageSource value) => new AtomKind.Image { Value = value };

    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator AtomKind(Egui.Image value) => new AtomKind.Image { Value = value };
}