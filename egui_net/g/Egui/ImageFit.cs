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
    /// This type determines how the image should try to fit within the UI.
    ///
    /// The final fit will be clamped to <c>MaxSize</c>.
    /// </summary>
    public partial struct ImageFit {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static ImageFit Deserialize(Serde.IDeserializer deserializer) { throw new NotImplementedException(); }}


} // end of namespace Egui
