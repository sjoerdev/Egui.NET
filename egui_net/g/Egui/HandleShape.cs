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
    /// Shape of the handle for sliders and similar widgets.
    /// </summary>
    public partial struct HandleShape {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static HandleShape Deserialize(Serde.IDeserializer deserializer) { throw new NotImplementedException(); }}


} // end of namespace Egui
