namespace Egui;

/// <summary>
/// The response from showing a <see cref="CollapsingHeader"/> .
/// </summary>
public struct CollapsingResponse
{
    /// <summary>
    /// Response of the actual clickable header.
    /// </summary>
    public Response HeaderResponse;

    /// <summary>
    /// None iff collapsed.
    /// </summary>
    public Response? BodyResponse;

    /// <summary>
    /// 0.0 if fully closed, 1.0 if fully open, and something in-between while animating.
    /// </summary>
    public float Openness;

    /// <summary>
    /// Was the <see cref="CollapsingHeader"/> fully closed (and not being animated)?
    /// </summary>
    public bool FullyClosed => Openness <= 0.0f;

    /// <summary>
    /// Was the <see cref="CollapsingHeader"/> fully open (and not being animated)?
    /// </summary>
    public bool FullyOpen => Openness >= 1.0f;
}

/// <inheritdoc cref="CollapsingResponse"/>
public struct CollapsingResponse<R>
{
    /// <inheritdoc cref="CollapsingResponse.HeaderResponse"/>
    public Response HeaderResponse;

    /// <inheritdoc cref="CollapsingResponse.BodyResponse"/>
    public Response? BodyResponse;

    /// <summary>
    /// None iff collapsed.
    /// </summary>
    public R? BodyReturned;

    /// <inheritdoc cref="CollapsingResponse.Openness"/>
    public float Openness;

    /// <inheritdoc cref="CollapsingResponse.FullyClosed"/>
    public bool FullyClosed => Openness <= 0.0f;

    /// <inheritdoc cref="CollapsingResponse.FullyOpen"/>
    public bool FullyOpen => Openness >= 1.0f;
}