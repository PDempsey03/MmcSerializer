namespace MmcSerializer.Attributes.CustomAttributes.XmlCustomAttributes
{
    /// <summary>
    /// Attribute used to define a property or field as an attribute for an xml tag
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MmcXmlAttribute : MmcCustomSerializationAttribute
    {

    }
}
