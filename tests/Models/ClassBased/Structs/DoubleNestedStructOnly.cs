namespace MmcSerializer.Tests.Models.ClassBased.Structs
{
    public struct DoubleNestedStructOnly
    {
        public NestedStructOnly _nestedStructOnly;

        private NestedStructOnly _privateNestedStructOnly;

        public NestedStructOnly NestedStructOnly { get; set; }

        private NestedStructOnly PrivateNestedStructOnly { get; set; }

        public override string ToString()
        {
            return ClassToString.GetObjectToStringFromFieldAndProperties(this);
        }

        public override bool Equals(object? obj)
        {
            return ClassEquals.AreClassesEqualFromFieldsAndProperties(this, obj);
        }
    }
}
