using System.Collections.Immutable;

namespace Egui.Epaint;

public partial struct Hsva
{
    public static Hsva FromAdditiveSrgb(ImmutableList<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableList<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_additive_srgb, rgb);
    }

    public static Hsva FromSrgb(ImmutableList<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableList<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_srgb, rgb);
    }

    public static Hsva FromSrgbaUnmultiplied(ImmutableList<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableList<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_srgba_unmultiplied, rgb);
    }

    public static Hsva FromSrgbaPremultiplied(ImmutableList<byte> rgb)
    {
        return EguiMarshal.Call<ImmutableList<byte>, Hsva>(EguiFn.ecolor_hsva_Hsva_from_srgba_premultiplied, rgb);
    }
}