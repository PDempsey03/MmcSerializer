using System.Reflection;

namespace MmcSerializer
{
    /// <summary>
    /// Class defining all options used in the base serialization process.
    /// Data format specific options may be defined in their own option classes.
    /// </summary>
    public class MmcSerializationOptions
    {
        public static MmcSerializationOptions DefaultMmcSerializationOptions => new MmcSerializationOptions();

        public SerializationMode FieldSerializationMode { get; set; } = SerializationMode.Public;

        public SerializationMode PropertySerializationMode { get; set; } = SerializationMode.Public;

        public Func<MemberInfo, bool>? ShouldSerialize { get; set; }
    }

    public enum SerializationMode
    {
        LabeledOnly,
        Public,
        PublicAndPrivate
    }
}
