namespace Egui;

public partial struct PointerState
{
    /// <summary>
    /// Holds the history of the pointer.
    /// </summary>
    public struct History
    {
        internal static void Serialize(Serde.ISerializer serializer, History value) => value.Serialize(serializer);

        internal void Serialize(Serde.ISerializer serializer)
        {
            throw new NotImplementedException();
        }

        internal static History Deserialize(Serde.IDeserializer deserializer)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj) => obj is History other && Equals(other);

        public static bool operator ==(History left, History right) => Equals(left, right);

        public static bool operator !=(History left, History right) => !Equals(left, right);

        public bool Equals(History other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}