namespace MmcSerializer
{
    /// <summary>
    /// Tree data structure used to format data needed for the serialization process.
    /// </summary>
    public class SerializationNode
    {
        /// <summary>
        /// Name associated with the node. May be used in the serialization process.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value associated with the node. May be used in the serilization process.
        /// </summary>
        public object? Value { get; set; }

        protected Queue<SerializationNode> ChildNodes { get; init; }

        public SerializationNode(string name, object? value)
        {
            Name = name;
            Value = value;
            ChildNodes = [];
        }

        /// <summary>
        /// Add SerializationNode child to this nodes children.
        /// </summary>
        public void AddChildNode(SerializationNode childNode)
        {
            ChildNodes.Enqueue(childNode);
        }

        /// <summary>
        /// Get the next SerializationNode from child list. Returns null if there are no more elements.
        /// </summary>
        public SerializationNode? GetNextChildNode()
        {
            ChildNodes.TryDequeue(out SerializationNode? result);
            return result;
        }
    }
}
