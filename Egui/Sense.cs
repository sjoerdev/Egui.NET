namespace Egui;

/// <summary>
/// What sort of interaction is a widget sensitive to?
/// </summary>
public enum Sense
{
    /// <summary>
    /// Only hovers detected.
    /// </summary>
    Hover = 0,

    /// <summary>
    /// Clicks detected.
    /// </summary>
    Click = 1 << 0,

    /// <summary>
    /// Drags detected.
    /// </summary>
    Drag = 1 << 1,

    /// <summary>
    /// The element should be focusable with tab.
    /// </summary>
    Focusable = 1 << 2
}