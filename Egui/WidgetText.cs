namespace Egui;

public partial struct WidgetText
{
    public string RawText => EguiMarshal.Call<WidgetText, string>(EguiFn.egui_widget_text_WidgetText_text, 0, this);

    /// <summary>
    /// Creates a widget text object for the provided rich text.
    /// </summary>
    /// <param name="value">The rich text to convert.</param>
    public static implicit operator WidgetText(Egui.RichText value) => new WidgetText.RichText { Value = value };

    /// <summary>
    /// Creates a widget text object for the provided string.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    public static implicit operator WidgetText(string value) => new WidgetText.RichText { Value = value };
}