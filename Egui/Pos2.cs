namespace Egui;

public partial struct Pos2
{
    /// <summary>
    /// The zero position, the origin. The top left corner in a GUI. Same as <see cref="Pos2()"/>.
    /// </summary>
    public static readonly Pos2 Zero = new Pos2();

    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator Pos2((float, float) xy) => new Pos2(xy.Item1, xy.Item2);

    /// <summary>
    /// Performs the <c>+</c> operation.
    /// </summary>
    public static Pos2 operator +(Pos2 lhs, Vec2 rhs) => new Pos2(lhs.X + rhs.X, lhs.Y + rhs.Y);

    /// <summary>
    /// Performs the <c>-</c> operation.
    /// </summary>
    public static Pos2 operator -(Pos2 lhs, Vec2 rhs) => new Pos2(lhs.X - rhs.X, lhs.Y - rhs.Y);

    /// <summary>
    /// Performs the <c>*</c> operation.
    /// </summary>
    public static Pos2 operator *(Pos2 lhs, float rhs) => new Pos2(lhs.X * rhs, lhs.Y * rhs);

    /// <summary>
    /// Performs the <c>*</c> operation.
    /// </summary>
    public static Pos2 operator *(float lhs, Pos2 rhs) => new Pos2(lhs * rhs.X, lhs * rhs.Y);

    /// <summary>
    /// Performs the <c>/</c> operation.
    /// </summary>
    public static Pos2 operator /(Pos2 lhs, float rhs) => new Pos2(lhs.X / rhs, lhs.Y / rhs);
}