namespace MmcSerializer.Attributes
{
    /// <summary>
    /// Attribute used to override the name used in the serialization
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MmcSerializeAsAttribute : MmcAttribute
    {
        public string Name { get; set; }

        public MmcSerializeAsAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException();
        }
    }
}
