/// <summary>
/// Holds a <c>struct</c> on the heap.
/// </summary>
/// <typeparam name="T">The type of item contained in the box.</typeparam>
public class ReadOnlyBox<T> where T : struct
{
    /// <summary>
    /// The underlying value held in this box.
    /// </summary>
    public readonly T Value;

    /// <summary>
    /// Allocates a struct on the heap.
    /// </summary>
    /// <param name="value">The value to allocate.</param>
    public ReadOnlyBox(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Unwraps a box to get its value.
    /// </summary>
    /// <param name="x">The box to unwrap.</param>
    public static implicit operator T(ReadOnlyBox<T> x) => x.Value;

    /// <inheritdoc cref="ReadOnlyBox{T}.ReadOnlyBox(T)"/>
    public static implicit operator ReadOnlyBox<T>(T x) => new ReadOnlyBox<T>(x);
}