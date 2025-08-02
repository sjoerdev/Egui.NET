using System.Collections.Immutable;

namespace Egui.Layers;

public partial struct GraphicLayers
{
    /// <summary>
    /// Get the <see cref="PaintList"/> for the given <see cref="LayerId"/>.
    /// </summary>
    public readonly PaintList? Get(LayerId layerId)
    {
        return EguiMarshal.Call<GraphicLayers, LayerId, PaintList?>(EguiFn.egui_layers_GraphicLayers_get, this, layerId);
    }

    public ImmutableList<ClippedShape> Drain(ImmutableList<LayerId> areaOrder, ImmutableDictionary<LayerId, TSTransform> toGlobal)
    {
        var (result, newThis) = EguiMarshal.Call<GraphicLayers, ImmutableList<LayerId>, ImmutableDictionary<LayerId, TSTransform>, (ImmutableList<ClippedShape>, GraphicLayers)>(EguiFn.egui_layers_GraphicLayers_drain, this, areaOrder, toGlobal);
        this = newThis;
        return result;
    }
}