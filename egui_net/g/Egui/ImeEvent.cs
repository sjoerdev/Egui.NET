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
    /// IME event.
    ///
    /// See <https://docs.rs/winit/latest/winit/event/enum.Ime.html>
    /// </summary>
    public partial struct ImeEvent {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static ImeEvent Deserialize(Serde.IDeserializer deserializer) { throw new NotImplementedException(); }}


} // end of namespace Egui
