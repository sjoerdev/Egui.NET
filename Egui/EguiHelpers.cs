namespace Egui;

public static partial class EguiHelpers
{
    /// <inheritdoc cref="ResetButtonWith{T}"/>
    public static void ResetButton<T>(Ui ui, ref T value, string text) where T : new()
    {
        ResetButtonWith(ui, ref value, text, new());
    }

    /// <summary>
    /// Show a button to reset a value to its default.
    /// The button is only enabled if the value does not already have its original value.
    ///
    /// The <paramref name="text"/> could be something like "Reset foo".
    /// </summary>
    public static void ResetButtonWith<T>(Ui ui, ref T value, string text, T resetValue)
    {
        if (ui.AddEnabled(EqualityComparer<T>.Default.Equals(value, resetValue), new Button(text)).Clicked)
        {
            value = resetValue;
        }
    }
}