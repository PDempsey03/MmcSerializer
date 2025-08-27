namespace MmcSerializer.Adapters
{
    /// <summary>
    /// Defines methods needed for MmcSerializer to serialize a given data format.
    /// Implementations will handle the reading and writing of data to and from the file.
    /// </summary>
    public interface ISerializerAdapter
    {
        void Serialize(SerializationNode rootNode);

        SerializationNode? Deserialize();
    }
}
