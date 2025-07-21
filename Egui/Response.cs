namespace Egui;

public partial struct Response
{
    /// <summary>
    /// Converts a response to its associated popup anchor.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    public static implicit operator PopupAnchor(Response response)
    {
        var widgetRect = response.InteractRect;

        /* TODO:
        var global = response.Ctx2.LayerTransformToGlobal(response.LayerId);
        if (global.HasValue)
        {
            //todo: widgetRect = global * widgetRect;
        }*/
        
        return new PopupAnchor.ParentRect()
        {
            Value = widgetRect
        };
    }
}