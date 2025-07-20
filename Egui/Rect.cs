namespace Egui;

public partial struct Rect
{
    /// <summary>
    /// Infinite rectangle that contains every point.
    /// </summary>
    public static readonly Rect Everything = Rect.FromMinMax(new Pos2(float.NegativeInfinity, float.NegativeInfinity), new Pos2(float.PositiveInfinity, float.PositiveInfinity));

    /// <summary>
    /// The inverse of <see cref="Everything"/> : stretches from positive infinity to negative infinity.
    /// Contains no points.><br/>
    ///
    /// This is useful as the seed for bounding boxes.
    /// </summary>
    public static readonly Rect Nothing = Rect.FromMinMax(new Pos2(float.PositiveInfinity, float.PositiveInfinity), new Pos2(float.NegativeInfinity, float.NegativeInfinity));

    /// <summary>
    /// An invalid <see cref="Rect"/> filled with <see cref="float.NaN"/>. 
    /// </summary>
    public static readonly Rect NaN = Rect.FromMinMax(new Pos2(float.NaN, float.NaN), new Pos2(float.NaN, float.NaN));

    /// <summary>
    /// A <see cref="Rect"/> filled with zeroes. 
    /// </summary>
    public static readonly Rect Zero = Rect.FromMinMax(new Pos2(0, 0), new Pos2(0, 0));

    /// <summary>
    /// <c>Max.y</c>
    /// </summary>
    public float Bottom
    {
        readonly get => Max.Y;
        set => Max.Y = value;
    }

    public Pos2 Center
    {
        readonly get => EguiMarshal.Call<Rect, Pos2>(EguiFn.emath_rect_Rect_center, 0, this);
        set => this = EguiMarshal.Call<Rect, Pos2, Rect>(EguiFn.emath_rect_Rect_center, 0, this, value);
    }

    /// <summary>
    /// Note: this can be negative.
    /// </summary>
    public float Height
    {
        readonly get => Max.Y - Min.Y;
        set => Max.Y = Min.Y + value;
    }

    /// <summary>
    /// Note: this can be negative.
    /// </summary>
    public float Width
    {
        readonly get => Max.X - Min.X;
        set => Max.Y = Min.X + value;
    }

    /// <summary>
    /// <c>Min.x</c>
    /// </summary>
    public float Left
    {
        readonly get => Min.X;
        set => Min.X = value;
    }

    /// <summary>
    /// <c>Max.x</c>
    /// </summary>
    public float Right
    {
        readonly get => Max.X;
        set => Max.X = value;
    }

    /// <summary>
    /// <c>Min.y</c>
    /// </summary>
    public float Top
    {
        readonly get => Min.Y;
        set => Min.Y = value;
    }

    public void ExtendWith(Pos2 p)
    {
        this = EguiMarshal.Call<Rect, Pos2, Rect>(EguiFn.emath_rect_Rect_extend_with, 0, this, p);
    }

    /// <summary>
    /// Expand to include the given x coordinate
    /// </summary>
    public void ExtendWithX(float x)
    {
        this = EguiMarshal.Call<Rect, float, Rect>(EguiFn.emath_rect_Rect_extend_with_x, 0, this, x);
    }

    /// <summary>
    /// Expand to include the given y coordinate
    /// </summary>    
    public void ExtendWithY(float y)
    {
        this = EguiMarshal.Call<Rect, float, Rect>(EguiFn.emath_rect_Rect_extend_with_y, 0, this, y);
    }
}