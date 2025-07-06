#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    /// <summary>
    /// Alias for a <c>FontId</c> (font of a certain size).
    ///
    /// The font is found via look-up in <c>TextStyles</c>.
    /// You can use <c>Resolve</c> to do this lookup.
    /// </summary>
    public partial struct TextStyle {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static TextStyle Deserialize(Serde.IDeserializer deserializer) { throw new NotImplementedException(); }}


} // end of namespace Egui
