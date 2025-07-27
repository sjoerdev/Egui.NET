using System.Numerics;

namespace Egui;

/// <summary>
/// A numeric value that you can change by dragging the number. More compact than a <see cref="Slider"/>.
/// </summary>
public ref struct DragValue<T> : IWidget where T : INumber<T>
{
    private ref T _value;
    private double _speed;
    private string _prefix;
    private string _suffix;
    private (double, double) _range;
    private bool _clampExistingToRange;
    private nuint _minDecimals;
    private nuint? _maxDecimals;
    private bool _updateWhileEditing;

    private nuint _minWidth;
    private bool _twosComplement;
    private Parser _parser;

    public DragValue(ref T value)
    {
        _value = ref value;
        _prefix = "";
        _suffix = "";
        _speed = 1.0f;
        _range = (double.NegativeInfinity, double.PositiveInfinity);
        _clampExistingToRange = true;
        _minDecimals = 0;
        _maxDecimals = null;
        _updateWhileEditing = true;

        if (typeof(T) != typeof(float) && typeof(T) != typeof(double))
        {
            _maxDecimals = 0;
            this = Range(T.CreateSaturating(double.MinValue), T.CreateSaturating(double.MaxValue));
        }
    }

    /// <summary>
    /// How much the value changes when dragged one point (logical pixel).<br/>
    ///
    /// Should be finite and greater than zero.
    /// </summary>
    public readonly DragValue<T> Speed(double speed)
    {
        var result = this;
        result._speed = speed;
        return result;
    }

    /// <summary>
    /// Sets valid range for dragging the value.<br/>
    ///
    /// By default all values are clamped to this range, even when not interacted with.
    /// You can change this behavior by passing <c>False</c> to <c>ClampExistingToRange</c>.
    /// </summary>
    public readonly DragValue<T> Range(T start, T end)
    {
        var result = this;
        result._range = (double.CreateSaturating(start), double.CreateSaturating(end));
        return result;
    }

    /// <summary>
    /// If set to <c>True</c>, existing values will be clamped to <c>Range</c>.<br/>
    ///
    /// If <c>False</c>, only values entered by the user (via dragging or text editing)
    /// will be clamped to the range.<br/>
    ///
    /// ### Without calling <c>Range</c>### With <c>.clampExistingToRange(true)</c> (default)### With <c>.clampExistingToRange(false)</c>    /// </summary>
    public readonly DragValue<T> ClampExistingToRange(bool clampExistingToRange)
    {
        var result = this;
        result._clampExistingToRange = clampExistingToRange;
        return result;
    }
    /// <summary>
    /// Show a prefix before the number, e.g. "x: "
    /// </summary>
    public readonly DragValue<T> Prefix(string prefix)
    {
        var result = this;
        result._prefix = prefix;
        return result;
    }

    /// <summary>
    /// How much the value changes when dragged one point (logical pixel).<br/>
    ///
    /// Should be finite and greater than zero.
    /// </summary>
    public readonly DragValue<T> Suffix(string suffix)
    {
        var result = this;
        result._suffix = suffix;
        return result;
    }

    /// <summary>
    /// Set a minimum number of decimals to display.
    /// Normally you don't need to pick a precision, as the slider will intelligently pick a precision for you.
    /// Regardless of precision the slider will use "smart aim" to help the user select nice, round values.
    /// </summary>
    public readonly DragValue<T> MinDecimals(nuint minDecimals)
    {
        var result = this;
        result._minDecimals = minDecimals;
        return result;
    }

    /// <summary>
    /// Set <c>CustomFormatter</c> and <c>CustomParser</c> to display and parse numbers as hexadecimal integers. Floating point
    /// numbers are *not* supported.<br/>
    ///
    /// <c>MinWidth</c> specifies the minimum number of displayed digits; if the number is shorter than this, it will be
    /// prefixed with additional 0s to match <c>MinWidth</c>.<br/>
    ///
    /// If <c>TwosComplement</c> is true, negative values will be displayed as the 2's complement representation. Otherwise
    /// they will be prefixed with a '-' sign.<br/>
    ///
    /// # Panics<br/>
    ///
    /// Panics if <c>MinWidth</c> is 0.
    /// </summary>
    public readonly DragValue<T> MaxDecimals(nuint maxDecimals)
    {
        var result = this;
        result._maxDecimals = maxDecimals;
        return result;
    }

    /// <summary>
    /// Set an exact number of decimals to display.
    /// Values will also be rounded to this number of decimals.
    /// Normally you don't need to pick a precision, as the slider will intelligently pick a precision for you.
    /// Regardless of precision the slider will use "smart aim" to help the user select nice, round values.
    /// </summary>
    public readonly DragValue<T> FixedDecimals(nuint numDecimals)
    {
        var result = this;
        result._minDecimals = numDecimals;
        result._maxDecimals = numDecimals;
        return result;
    }

    /// <summary>
    /// Set <c>CustomFormatter</c> and <c>CustomParser</c> to display and parse numbers as binary integers. Floating point
    /// numbers are *not* supported.<br/>
    ///
    /// <c>MinWidth</c> specifies the minimum number of displayed digits; if the number is shorter than this, it will be
    /// prefixed with additional 0s to match <c>MinWidth</c>.<br/>
    ///
    /// If <c>TwosComplement</c> is true, negative values will be displayed as the 2's complement representation. Otherwise
    /// they will be prefixed with a '-' sign.<br/>
    ///
    /// # Panics<br/>
    ///
    /// Panics if <c>MinWidth</c> is 0.
    /// </summary>
    public readonly DragValue<T> Binary(nuint minWidth, bool twosComplement)
    {
        var result = this;
        result._minWidth = minWidth;
        result._twosComplement = twosComplement;
        result._parser = Parser.Binary;
        return result;
    }

    /// <summary>
    /// Set <c>CustomFormatter</c> and <c>CustomParser</c> to display and parse numbers as octal integers. Floating point
    /// numbers are *not* supported.<br/>
    ///
    /// <c>MinWidth</c> specifies the minimum number of displayed digits; if the number is shorter than this, it will be
    /// prefixed with additional 0s to match <c>MinWidth</c>.<br/>
    ///
    /// If <c>TwosComplement</c> is true, negative values will be displayed as the 2's complement representation. Otherwise
    /// they will be prefixed with a '-' sign.<br/>
    ///
    /// # Panics<br/>
    ///
    /// Panics if <c>MinWidth</c> is 0.
    /// </summary>
    public readonly DragValue<T> Octal(nuint minWidth, bool twosComplement)
    {
        var result = this;
        result._minWidth = minWidth;
        result._twosComplement = twosComplement;
        result._parser = Parser.Octal;
        return result;
    }

    /// <summary>
    /// Set <c>CustomFormatter</c> and <c>CustomParser</c> to display and parse numbers as hexadecimal integers. Floating point
    /// numbers are *not* supported.<br/>
    ///
    /// <c>MinWidth</c> specifies the minimum number of displayed digits; if the number is shorter than this, it will be
    /// prefixed with additional 0s to match <c>MinWidth</c>.<br/>
    ///
    /// If <c>TwosComplement</c> is true, negative values will be displayed as the 2's complement representation. Otherwise
    /// they will be prefixed with a '-' sign.<br/>
    ///
    /// # Panics<br/>
    ///
    /// Panics if <c>MinWidth</c> is 0.
    /// </summary>
    public readonly DragValue<T> Hexadecimal(nuint minWidth, bool twosComplement)
    {
        var result = this;
        result._minWidth = minWidth;
        result._twosComplement = twosComplement;
        result._parser = Parser.Hexadecimal;
        return result;
    }

    /// <summary>
    /// Update the value on each key press when text-editing the value.<br/>
    ///
    /// Default: <c>True</c>.
    /// If <c>False</c>, the value will only be updated when user presses enter or deselects the value.
    /// </summary>
    public readonly DragValue<T> UpdateWhileEditing(bool update)
    {
        var result = this;
        result._updateWhileEditing = update;
        return result;
    }

    /// <inheritdoc/>
    Response IWidget.Ui(Ui ui)
    {
        throw new NotImplementedException();
    }

    private enum Parser
    {
        Default,
        Binary,
        Octal,
        Hexadecimal
    }
}