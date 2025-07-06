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
    /// Commands that the egui integration should execute at the end of a frame.
    ///
    /// Commands that are specific to a viewport should be put in <c>ViewportCommand</c> instead.
    /// </summary>
    public partial struct OutputCommand {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static OutputCommand Deserialize(Serde.IDeserializer deserializer) { throw new NotImplementedException(); }}


} // end of namespace Egui
