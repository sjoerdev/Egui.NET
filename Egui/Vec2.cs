namespace Egui;

public partial struct Vec2
{
    /// <summary>
    /// +X
    /// </summary>
    public static readonly Vec2 Right = new Vec2(1, 0);

    /// <summary>
    /// -X
    /// </summary>
    public static readonly Vec2 Left = new Vec2(-1, 0);

    /// <summary>
    /// -Y
    /// </summary>
    public static readonly Vec2 Up = new Vec2(0, -1);

    /// <summary>
    /// +Y
    /// </summary>
    public static readonly Vec2 Down = new Vec2(0, 1);

    public static readonly Vec2 Zero = new Vec2(0);

    public static readonly Vec2 One = new Vec2(1);

    public static readonly Vec2 Infinity = new Vec2(float.PositiveInfinity);

    public static readonly Vec2 NaN = new Vec2(float.NaN);

    /// <summary>
    /// Same as <see cref="Splat"/>.
    /// </summary>
    public Vec2(float v) : this(v, v) { }

    /// <summary>
    /// Converts to this type from the input type.
    /// </summary>
    public static implicit operator Vec2((float, float) xy) => new Vec2(xy.Item1, xy.Item2);

    /// <summary>
    /// Performs the <c>+</c> operation.
    /// </summary>
    public static Vec2 operator +(Vec2 lhs, Vec2 rhs) => new Vec2(lhs.X + rhs.X, lhs.Y + rhs.Y);

    /// <summary>
    /// Performs the <c>-</c> operation.
    /// </summary>
    public static Vec2 operator -(Vec2 lhs, Vec2 rhs) => new Vec2(lhs.X - rhs.X, lhs.Y - rhs.Y);

    /// <summary>
    /// Performs the <c>*</c> operation.
    /// </summary>
    public static Vec2 operator *(Vec2 lhs, Vec2 rhs) => new Vec2(lhs.X * rhs.X, lhs.Y * rhs.Y);

    /// <summary>
    /// Performs the <c>*</c> operation.
    /// </summary>
    public static Vec2 operator *(Vec2 lhs, float rhs) => new Vec2(lhs.X * rhs, lhs.Y * rhs);

    /// <summary>
    /// Performs the <c>*</c> operation.
    /// </summary>
    public static Vec2 operator *(float lhs, Vec2 rhs) => new Vec2(lhs * rhs.X, lhs * rhs.Y);

    /// <summary>
    /// Performs the <c>*</c> operation.
    /// </summary>
    public static Vec2 operator /(Vec2 lhs, Vec2 rhs) => new Vec2(lhs.X / rhs.X, lhs.Y / rhs.Y);

    /// <summary>
    /// Performs the <c>/</c> operation.
    /// </summary>
    public static Vec2 operator /(Vec2 lhs, float rhs) => new Vec2(lhs.X / rhs, lhs.Y / rhs);
}