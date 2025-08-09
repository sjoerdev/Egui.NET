using System.Collections.Immutable;

namespace Egui;

public partial struct AllocatedAtomLayout
{
    public IEnumerable<Image> Images => SizedAtoms.Where(x => x.Kind.Inner is SizedAtomKind.Image)
        .Select(x => ((SizedAtomKind.Image)x.Kind.Inner).Item1);

    public IEnumerable<Galley> Texts => SizedAtoms.Where(x => x.Kind.Inner is SizedAtomKind.Text)
        .Select(x => ((SizedAtomKind.Text)x.Kind.Inner).Value);

    public IEnumerable<SizedAtomKind> Kinds => SizedAtoms.Select(x => x.Kind);

    public void MapKind(Func<SizedAtomKind, SizedAtomKind> f)
    {
        SizedAtoms = SizedAtoms.Select(x =>
        {
            x.Kind = f(x.Kind);
            return x;
        }).ToImmutableArray();
    }

    public void MapImages(Func<Image, Image> f)
    {
        SizedAtoms = SizedAtoms.Select(x =>
        {
            if (x.Kind.Inner is SizedAtomKind.Image image)
            {
                x.Kind = new SizedAtomKind.Image(f(image.Item1), image.Item2);
            }
            return x;
        }).ToImmutableArray();
    }
}