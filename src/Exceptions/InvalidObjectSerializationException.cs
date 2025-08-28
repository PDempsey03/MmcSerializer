using MmcSerializer.Extensions;

namespace MmcSerializer.Exceptions
{
    public class InvalidObjectSerializationException : Exception
    {
        public InvalidObjectSerializationException(TypeCategory typeCategory) : base("Cannot serialize a object of type category " + typeCategory) { }

        public InvalidObjectSerializationException(string message) : base(message) { }
    }
}
