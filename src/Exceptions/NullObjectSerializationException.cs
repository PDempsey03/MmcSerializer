namespace MmcSerializer.Exceptions
{
    /// <summary>
    /// Thrown when a null object is passed into the serializer
    /// </summary>
    public class NullObjectSerializationException : Exception
    {
        public NullObjectSerializationException() : base("Cannot serialize a null object") { }

        public NullObjectSerializationException(string message) : base(message) { }
    }
}
