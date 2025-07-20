namespace Egui;

/// <summary>
/// Anything implementing Widget can be added to a <see cref="Ui"/> with <see cref="Ui.Add"/>.<br/>
///
/// <see cref="Button"/>, <see cref="Label"/>, <see cref="Slider"/>, etc all implement the <see cref="Widget"/> trait.<br/>
///
/// You only need to implement <see cref="Widget"/> if you care about being able to do <c>ui.Add(yourWidget);</c>.<br/>
///
/// Note that the widgets (<see cref="Button"/> , <see cref="TextEdit"/>  etc) are
/// <c>builders</c> and not objects that hold state.
/// </summary>
public interface Widget
{
    /// <summary>
    /// Allocate space, interact, paint, and return a <see cref="Response"/> .
    /// </summary>
    public Response Ui(Ui ui);
}