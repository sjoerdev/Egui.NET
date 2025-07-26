using System.Collections.Immutable;

namespace Egui;

public partial struct AllocatedAtomLayout
{
    public IEnumerable<SizedAtomKind> Kinds => SizedAtoms.Select(x => x.Kind);

    public void MapKind(Func<SizedAtomKind, SizedAtomKind> f)
    {
        SizedAtoms = SizedAtoms.Select(x =>
        {
            x.Kind = f(x.Kind);
            return x;
        }).ToImmutableList();
    }
}