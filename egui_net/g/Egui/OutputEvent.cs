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
    /// Things that happened during this frame that the integration may be interested in.
    ///
    /// In particular, these events may be useful for accessibility, i.e. for screen readers.
    /// </summary>
    public partial struct OutputEvent {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static OutputEvent Deserialize(Serde.IDeserializer deserializer) { throw new NotImplementedException(); }}


} // end of namespace Egui
