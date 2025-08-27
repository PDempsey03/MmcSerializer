namespace MmcSerializer.Attributes
{
    /// <summary>
    /// Indicates a property or field should not be serialized if its value is null
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MmcDontSerializeIfNullAttribute : Attribute
    {

    }
}
