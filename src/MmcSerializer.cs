using MmcSerializer.Adapters;
using MmcSerializer.Exceptions;
using System.Reflection;

namespace MmcSerializer
{
    /// <summary>
    /// Primary class used for the serilization and deserialization of objects.
    /// Requires a data format specific adapter and options to run.
    /// </summary>
    public class MmcSerializer
    {
        public ISerializerAdapter Adapter { get; set; }

        public MmcSerializationOptions SerializationOptions { get; set; }

        public MmcSerializer(ISerializerAdapter adapter, MmcSerializationOptions serializerOptions)
        {
            Adapter = adapter;
            SerializationOptions = serializerOptions;
        }

        public MmcSerializer(ISerializerAdapter adapter) : this(adapter, MmcSerializationOptions.DefaultMmcSerializationOptions)
        {

        }

        /// <summary>
        /// Serializes an object in the format specified by the adapter.
        /// </summary>
        /// <param name="o">Object to serialize.</param>
        /// <exception cref="NullObjectSerializationException">Thrown if <paramref name="o"/> is null.</exception>
        public virtual void Serialize(object o)
        {
            if (o == null) throw new NullObjectSerializationException();

            var type = o.GetType();

            var nodeName = type.AssemblyQualifiedName ?? type.FullName ?? type.Name;

            SerializationNode rootNode = new SerializationNode(nodeName, o);

            var propInfos = GetValidPropertiesForObject(o);
            var fieldInfos = GetValidFieldsForObject(o);

            foreach (var fieldInfo in fieldInfos)
            {
                InternalSerialize(fieldInfo, rootNode);
            }

            foreach (var propInfo in propInfos)
            {
                InternalSerialize(propInfo, rootNode);
            }
        }

        protected virtual void InternalSerialize(PropertyInfo propInfo, SerializationNode parentNode)
        {
            // TODO: use recursion to build tree
        }

        protected virtual void InternalSerialize(FieldInfo fieldInfo, SerializationNode parentNode)
        {
            // TODO: use recursion to build tree
        }

        protected virtual PropertyInfo[] GetValidPropertiesForObject(object? o)
        {
            if (o == null) return [];

            var type = o.GetType();

            var propInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return propInfo;
        }

        protected virtual FieldInfo[] GetValidFieldsForObject(object? o)
        {
            if (o == null) return [];

            var type = o.GetType();

            var fieldInfo = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return fieldInfo;
        }

        /// <summary>
        /// Deserializes an object based on the adapter.
        /// </summary>
        /// <returns>Nullable deserialized object</returns>
        public virtual object? Deserialize()
        {
            throw new NotImplementedException(); // TODO: use adapter to deserialize into a SerializationNode tree and then handle reflection here
        }
    }
}
