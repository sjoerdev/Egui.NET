namespace Egui
{

    public partial struct PointerState
    {
    }
}

namespace Egui.Util
{
    /// <summary>
    /// Holds the history of the pointer.
    /// </summary>
    public struct History
    {
        internal static void Serialize(BincodeSerializer serializer, History value) => value.Serialize(serializer);

        internal void Serialize(BincodeSerializer serializer)
        {
            throw new NotImplementedException();
        }

        internal static History Deserialize(BincodeDeserializer deserializer)
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


namespace Egui.History
{
    /// <summary>
    /// Holds the history of the pointer.
    /// </summary>
    public struct History
    {
        internal static void Serialize(BincodeSerializer serializer, History value) => value.Serialize(serializer);

        internal void Serialize(BincodeSerializer serializer)
        {
            throw new NotImplementedException();
        }

        internal static History Deserialize(BincodeDeserializer deserializer)
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