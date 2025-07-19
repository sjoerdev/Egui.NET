namespace Egui;

/// <summary>
/// Returned when we wrap some ui-code and want to return both the results of the inner function and the ui as a whole.
/// </summary>
/// <typeparam name="R">The type of the closure.</typeparam>
public struct InnerResponse
{
    /// <summary>
    /// The response of the area.
    /// </summary>
    public required Response Response;
}

/// <summary>
/// Returned when we wrap some ui-code and want to return both the results of the inner function and the ui as a whole.
/// </summary>
/// <typeparam name="R">The type of the closure.</typeparam>
public struct InnerResponse<R>
{
    /// <summary>
    /// What the user closure returned.
    /// </summary>
    public required R Inner;

    /// <summary>
    /// The response of the area.
    /// </summary>
    public required Response Response;
}