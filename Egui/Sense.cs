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
    /// Buttons, sliders, windows, ...
    /// </summary>
    ClickOnly = 1 << 0,

    /// <summary>
    /// Sliders, windows, scroll bars, scroll areas, ...
    /// </summary>
    DragOnly = 1 << 1,

    /// <summary>
    /// This widget wants focus.
    ///
   /// Anything interactive + labels that can be focused
    /// for the benefit of screen readers.
    /// </summary>
    Focusable = 1 << 2,

    /// <summary>
    /// Sense drags and hover, but not clicks.
    /// </summary>
    Click = ClickOnly | Focusable,

    /// <summary>
    /// Sense drags and hover, but not clicks.
    /// </summary>
    Drag = DragOnly | Focusable,

    /// <summary>
    /// Sense both clicks, drags and hover (e.g. a slider or window).
    /// Note that this will introduce a latency when dragging, because when the user starts a press egui can�t know if this is the start of a click or a drag, and it won�t know until the cursor has either moved a certain distance, or the user has released the mouse button.
    /// See <c>IsDecidedlyDragging</c> for details.
    /// </summary>
    ClickAndDrag = ClickOnly | DragOnly | Focusable
}