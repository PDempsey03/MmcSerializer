using MmcSerializer.Extensions;
using System.Xml;

namespace MmcSerializer.Adapters
{
    /// <summary>
    /// Adapter that allows for serilization to and from an xml file.
    /// </summary>
    public class XmlSerializerAdapter : ISerializerAdapter
    {
        private const string TypeAttribute = "type";
        private const string IsNullAttribute = "isnull";

        public XmlWriter? XmlWriter { get; set; }

        public XmlReader? XmlReader { get; set; }

        public XmlSerializerAdapter()
        {

        }

        public void Serialize(SerializationNode rootNode)
        {
            ArgumentNullException.ThrowIfNull(rootNode, nameof(rootNode));
            ArgumentNullException.ThrowIfNull(XmlWriter, nameof(XmlWriter));

            XmlWriter.WriteStartElement(rootNode.Name);
            XmlWriter.WriteAttributeString(TypeAttribute, rootNode.TypeString);

            SerializationNode? nextNode = rootNode.GetNextChildNode();

            if (nextNode == null && (IsTypeCatgoryValidForValue(rootNode.TypeCategory) || rootNode.Value == null))
            {
                object? value = rootNode.Value;

                if (value == null)
                    XmlWriter.WriteAttributeString(IsNullAttribute, true.ToString().ToLower());
                else
                    XmlWriter.WriteString(value.ToString());
            }
            else
            {
                while (nextNode != null)
                {
                    Serialize(nextNode);
                    nextNode = rootNode.GetNextChildNode();
                }
            }

            XmlWriter.WriteEndElement();

            XmlWriter.Flush();
        }

        public SerializationNode? Deserialize()
        {
            ArgumentNullException.ThrowIfNull(XmlReader, nameof(XmlReader));

            // skip to the first element to serialize
            while (XmlReader.NodeType != XmlNodeType.Element)
                XmlReader.Read();

            string name = XmlReader.Name;
            string typeString = XmlReader.GetAttribute(TypeAttribute) ?? throw new XmlException($"Xml element missing attribute {TypeAttribute}");
            string? isNullString = XmlReader.GetAttribute(IsNullAttribute);
            if (!bool.TryParse(isNullString, out bool isNull)) isNull = false;

            Type type = Type.GetType(typeString) ?? throw new InvalidOperationException($"Cannot resolve type {typeString}");

            TypeCategory typeCategory = type.GetTypeCategory();

            if (isNull) typeCategory = typeCategory.ToNullable();

            var currentNode = new SerializationNode(name, null, type, typeCategory)
            {
                TrueValueIsNull = isNull,
            };

            bool isEmpty = XmlReader.IsEmptyElement;
            XmlReader.Read(); // move inside

            if (!isEmpty)
            {
                if (IsTypeCatgoryValidForValue(currentNode.TypeCategory))
                {
                    if (isNull)
                        currentNode.Value = null;
                    else
                        currentNode.Value = XmlReader.ReadContentAsString();
                }
                else
                {
                    // Recursively read children
                    while (XmlReader.NodeType != XmlNodeType.EndElement)
                    {
                        if (XmlReader.NodeType != XmlNodeType.Element)
                        {
                            XmlReader.Read();
                            continue;
                        }

                        var child = Deserialize();
                        if (child != null) currentNode.AddChildNode(child);
                    }
                    XmlReader.ReadEndElement();
                }

                if (XmlReader.NodeType == XmlNodeType.EndElement)
                    XmlReader.ReadEndElement();
            }
            return currentNode;
        }

        private static bool IsTypeCatgoryValidForValue(TypeCategory typeCategory)
        {
            return typeCategory switch
            {
                TypeCategory.Primitive or TypeCategory.NullablePrimitive or
                TypeCategory.Enum or TypeCategory.NullableEnum or TypeCategory.String => true,
                _ => false
            };
        }
    }
}
