using MmcSerializer.Adapters;
using MmcSerializer.Attributes;
using MmcSerializer.Exceptions;
using MmcSerializer.Extensions;
using System.Reflection;
using System.Runtime.CompilerServices;

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

            var typeCategory = type.GetTypeCategory();

            if (typeCategory != TypeCategory.Struct && typeCategory != TypeCategory.Class) throw new InvalidObjectSerializationException(typeCategory);

            var nodeName = type.Name;

            SerializationNode rootNode = new SerializationNode(nodeName, o, type, typeCategory);

            var propInfos = GetValidPropertiesForObjectSerialization(o);
            var fieldInfos = GetValidFieldsForObjectSerialization(o);

            foreach (var fieldInfo in fieldInfos)
            {
                HandleFieldSerialization(fieldInfo, rootNode);
            }

            foreach (var propInfo in propInfos)
            {
                HandlePropertySerialization(propInfo, rootNode);
            }

            Adapter.Serialize(rootNode);
        }

        protected virtual void HandlePropertySerialization(PropertyInfo propertyInfo, SerializationNode parentNode)
        {
            Type propType = propertyInfo.PropertyType;

            string name = propertyInfo.GetCustomAttribute<MmcSerializeAsAttribute>()?.Name ?? propertyInfo.Name;

            object? value = propertyInfo.GetValue(parentNode.Value);

            HandleSerialization(name, value, propType, parentNode);
        }

        protected virtual void HandleFieldSerialization(FieldInfo fieldInfo, SerializationNode parentNode)
        {
            Type fieldType = fieldInfo.FieldType;

            string name = fieldInfo.GetCustomAttribute<MmcSerializeAsAttribute>()?.Name ?? fieldInfo.Name;

            object? value = fieldInfo.GetValue(parentNode.Value);

            HandleSerialization(name, value, fieldType, parentNode);
        }

        protected virtual void HandleSerialization(string name, object? value, Type type, SerializationNode parentNode)
        {
            TypeCategory typeCategory = type.GetTypeCategory();

            switch (typeCategory)
            {
                case TypeCategory.Class:
                    SerializeClassType(name, value, type, parentNode);
                    break;

                case TypeCategory.String:
                    SerializeStringType(name, value, type, parentNode);
                    break;

                case TypeCategory.Primitive:
                    SerializePrimitiveType(name, value, type, typeCategory, parentNode);
                    break;

                case TypeCategory.NullablePrimitive:
                    SerializePrimitiveType(name, value, Nullable.GetUnderlyingType(type) ?? type, typeCategory, parentNode);
                    break;
            }
        }

        protected virtual void SerializeClassType(string name, object? value, Type type, SerializationNode parentNode)
        {
            SerializationNode childNode = new SerializationNode(name, value, type, TypeCategory.Class);

            parentNode.AddChildNode(childNode);

            var propInfos = GetValidPropertiesForObjectSerialization(value);
            var fieldInfos = GetValidFieldsForObjectSerialization(value);

            foreach (var fieldInfo in fieldInfos)
            {
                HandleFieldSerialization(fieldInfo, childNode);
            }

            foreach (var propInfo in propInfos)
            {
                HandlePropertySerialization(propInfo, childNode);
            }
        }

        protected virtual void SerializeStringType(string name, object? value, Type type, SerializationNode parentNode)
        {
            SerializationNode childNode = new SerializationNode(name, value, type, TypeCategory.String);

            parentNode.AddChildNode(childNode);
        }

        protected virtual void SerializePrimitiveType(string name, object? value, Type type, TypeCategory typeCategory, SerializationNode parentNode)
        {
            SerializationNode childNode = new SerializationNode(name, value, type, typeCategory);

            parentNode.AddChildNode(childNode);
        }

        protected virtual PropertyInfo[] GetValidPropertiesForObjectSerialization(object? o)
        {
            if (o == null) return [];

            var type = o.GetType();

            var propInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return propInfo;
        }

        protected virtual FieldInfo[] GetValidFieldsForObjectSerialization(object? o)
        {
            if (o == null) return [];

            var type = o.GetType();

            var allFields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // finally remove any compiler generated fields like backing fields for auto properties
            var validFields = allFields.Where(f => !f.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false)).ToArray();

            return validFields;
        }

        /// <summary>
        /// Deserializes an object based on the adapter.
        /// </summary>
        /// <returns>Nullable deserialized object</returns>
        public virtual object? Deserialize()
        {
            SerializationNode? rootNode = Adapter.Deserialize();

            if (rootNode == null) return null;

            TypeCategory typeCategory = rootNode.TypeCategory;

            if (typeCategory == TypeCategory.Class)
            {
                DeserializeClassType(rootNode, null, (parameters) =>
                {
                    if (parameters != null && parameters.Length == 1)
                    {
                        rootNode.Value = parameters[0];
                    }
                });
            }
            else if (typeCategory == TypeCategory.Struct)
            {

            }

            return rootNode.Value;
        }

        protected virtual void DeserializeClassType(SerializationNode currentNode, SerializationNode? parentNode, Action<object?[]?> setter)
        {
            Type classType = currentNode.Type;

            object? instance = Activator.CreateInstance(classType);

            setter.Invoke([instance]);

            Dictionary<string, PropertyInfo> propInfos = [];
            Dictionary<string, FieldInfo> fieldInfos = [];

            foreach (var propInfo in GetValidPropertiesForObjectDeserialization(instance))
            {
                var hasCustomName = propInfo.IsDefined(typeof(MmcSerializeAsAttribute), inherit: false);

                var propertyName = propInfo.Name;

                var name = hasCustomName ? propInfo.GetCustomAttribute<MmcSerializeAsAttribute>()?.Name ?? propertyName : propertyName;

                propInfos.Add(name, propInfo);
            }

            foreach (var fieldInfo in GetValidFieldsForObjectDeserialization(instance))
            {
                var hasCustomName = fieldInfo.IsDefined(typeof(MmcSerializeAsAttribute), inherit: false);

                var fieldName = fieldInfo.Name;

                var name = hasCustomName ? fieldInfo.GetCustomAttribute<MmcSerializeAsAttribute>()?.Name ?? fieldName : fieldName;

                fieldInfos.Add(name, fieldInfo);
            }

            var nextChildNode = currentNode.GetNextChildNode();
            while (nextChildNode != null)
            {
                Action<object?[]?>? newSetter = null;

                if (propInfos.TryGetValue(nextChildNode.Name, out PropertyInfo? propertyInfo))
                {
                    newSetter = (parameters) => propertyInfo.SetMethod?.Invoke(instance, parameters);
                }
                else if (fieldInfos.TryGetValue(nextChildNode.Name, out FieldInfo? fieldInfo))
                {
                    newSetter = (parameters) => fieldInfo.SetValue(instance, parameters?[0] ?? null);
                }

                if (newSetter != null)
                {
                    switch (nextChildNode.TypeCategory)
                    {
                        case TypeCategory.Class:
                            DeserializeClassType(nextChildNode, currentNode, newSetter);
                            break;

                        case TypeCategory.String:
                            DeserializeStringType(nextChildNode, currentNode, newSetter);
                            break;

                        case TypeCategory.Primitive:
                        case TypeCategory.NullablePrimitive:
                            DeserializePrimitiveType(nextChildNode, currentNode, newSetter);
                            break;
                    }
                }

                nextChildNode = currentNode.GetNextChildNode();
            }
        }

        protected virtual void DeserializeStringType(SerializationNode currentNode, SerializationNode? parentNode, Action<object?[]?> setter)
        {
            if (currentNode.Value is string stringValue)
            {
                setter.Invoke([stringValue]);
            }
            else if (currentNode.Value is null)
            {
                setter.Invoke([null]);
            }
        }

        protected virtual void DeserializePrimitiveType(SerializationNode currentNode, SerializationNode? parentNode, Action<object?[]?> setter)
        {
            if (currentNode.Value is string stringValue)
            {
                object? convertedType;

                if (currentNode.Type == typeof(nint))
                {
                    if (long.TryParse(stringValue, out long parsedLong))
                        convertedType = new IntPtr(parsedLong);
                    else
                        convertedType = default(nint);
                }
                else if (currentNode.Type == typeof(nuint))
                {
                    if (ulong.TryParse(stringValue, out ulong parsedUlong))
                        convertedType = new UIntPtr(parsedUlong);
                    else
                        convertedType = default(nuint);
                }
                else
                {
                    convertedType = Convert.ChangeType(stringValue, currentNode.Type);
                }

                setter.Invoke([convertedType]);
            }
            else if (currentNode.Value is null)
            {
                setter.Invoke([null]);
            }
        }

        protected virtual PropertyInfo[] GetValidPropertiesForObjectDeserialization(object? o)
        {
            if (o == null) return [];

            var type = o.GetType();

            var propInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return propInfo;
        }

        protected virtual FieldInfo[] GetValidFieldsForObjectDeserialization(object? o)
        {
            if (o == null) return [];

            var type = o.GetType();

            var allFields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // finally remove any compiler generated fields like backing fields for auto properties
            var validFields = allFields.Where(f => !f.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false)).ToArray();

            return validFields;
        }
    }
}
