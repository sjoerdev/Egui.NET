#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct PlatformOutput : IEquatable<PlatformOutput> {
        public ImmutableList<OutputCommand> Commands;
        public CursorIcon CursorIcon;
        public OpenUrl? OpenUrl;
        public string CopiedText;
        public ImmutableList<OutputEvent> Events;
        public bool MutableTextUnderCursor;
        public IMEOutput? Ime;
        public ulong NumCompletedPasses;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_vector_OutputCommand(Commands, serializer);
            CursorIcon.Serialize(serializer);
            TraitHelpers.serialize_option_OpenUrl(OpenUrl, serializer);
            serializer.serialize_str(CopiedText);
            TraitHelpers.serialize_vector_OutputEvent(Events, serializer);
            serializer.serialize_bool(MutableTextUnderCursor);
            TraitHelpers.serialize_option_IMEOutput(Ime, serializer);
            serializer.serialize_u64(NumCompletedPasses);
            serializer.decrease_container_depth();
        }

        internal static PlatformOutput Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            PlatformOutput obj = new PlatformOutput {
            	Commands = TraitHelpers.deserialize_vector_OutputCommand(deserializer),
            	CursorIcon = CursorIconExtensions.Deserialize(deserializer),
            	OpenUrl = TraitHelpers.deserialize_option_OpenUrl(deserializer),
            	CopiedText = deserializer.deserialize_str(),
            	Events = TraitHelpers.deserialize_vector_OutputEvent(deserializer),
            	MutableTextUnderCursor = deserializer.deserialize_bool(),
            	Ime = TraitHelpers.deserialize_option_IMEOutput(deserializer),
            	NumCompletedPasses = deserializer.deserialize_u64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is PlatformOutput other && Equals(other);

        public static bool operator ==(PlatformOutput left, PlatformOutput right) => Equals(left, right);

        public static bool operator !=(PlatformOutput left, PlatformOutput right) => !Equals(left, right);

        public bool Equals(PlatformOutput other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Commands.Equals(other.Commands)) return false;
            if (!CursorIcon.Equals(other.CursorIcon)) return false;
            if (!OpenUrl.Equals(other.OpenUrl)) return false;
            if (!CopiedText.Equals(other.CopiedText)) return false;
            if (!Events.Equals(other.Events)) return false;
            if (!MutableTextUnderCursor.Equals(other.MutableTextUnderCursor)) return false;
            if (!Ime.Equals(other.Ime)) return false;
            if (!NumCompletedPasses.Equals(other.NumCompletedPasses)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Commands.GetHashCode();
                value = 31 * value + CursorIcon.GetHashCode();
                value = 31 * value + OpenUrl.GetHashCode();
                value = 31 * value + CopiedText.GetHashCode();
                value = 31 * value + Events.GetHashCode();
                value = 31 * value + MutableTextUnderCursor.GetHashCode();
                value = 31 * value + Ime.GetHashCode();
                value = 31 * value + NumCompletedPasses.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
