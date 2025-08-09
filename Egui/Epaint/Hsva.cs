using System.Collections.Immutable;

namespace Egui.Epaint;

public partial struct Hsva
{
    public static Hsva FromAdditiveSrgb(ImmutableArray<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableArray<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_additive_srgb, rgb);
    }

    public static Hsva FromSrgb(ImmutableArray<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableArray<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_srgb, rgb);
    }

    public static Hsva FromSrgbaUnmultiplied(ImmutableArray<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableArray<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_srgba_unmultiplied, rgb);
    }

    public static Hsva FromSrgbaPremultiplied(ImmutableArray<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableArray<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_srgba_premultiplied, rgb);
    }
}