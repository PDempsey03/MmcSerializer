using MmcSerializer.Extensions;
using System.Text;

namespace MmcSerializer
{
    /// <summary>
    /// Tree data structure used to format data needed for the serialization process.
    /// </summary>
    public class SerializationNode
    {
        /// <summary>
        /// Name associated with the node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value associated with the node.
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// Determines whether value just hasn't been instantiated yet or if it is actually null
        /// </summary>
        public bool TrueValueIsNull { get; set; }

        /// <summary>
        /// Type associated with the Value.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Type catgetory associated with the Type's type category.
        /// </summary>
        public TypeCategory TypeCategory { get; set; }

        // TODO: need to include the custom attributes it has incase of custom data specific ones line xmlAttribute

        /// <summary>
        /// String representing the type to its fullest valid qualified name.
        /// </summary>
        public string TypeString => Type.AssemblyQualifiedName ?? Type.FullName ?? Type.Name;

        protected Queue<SerializationNode> ChildNodes { get; init; }

        public SerializationNode(string name, object? value, Type type, TypeCategory typeCategory)
        {
            Name = name;
            Value = value;
            Type = type;
            TypeCategory = typeCategory;
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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("--------------Serialization Node--------------");
            builder.AppendLine($"Name:{Name}");
            builder.AppendLine($"Type: {TypeString}");
            builder.AppendLine($"Object Hash:{Value?.GetHashCode().ToString() ?? "null"}");
            builder.AppendLine($"Value: {Value?.ToString() ?? "null"}");

            foreach (var child in ChildNodes)
            {
                builder.Append(child.ToString());
            }

            return builder.ToString();
        }
    }
}
