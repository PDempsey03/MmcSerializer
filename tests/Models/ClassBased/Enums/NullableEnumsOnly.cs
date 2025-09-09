namespace MmcSerializer.Tests.Models.ClassBased.Enums
{
    public class NullableEnumsOnly
    {
        public PublicNullableEnumsOnly? _publicNullableEnumsOnlyField;

        private PublicNullableEnumsOnly? _privatePublicNullableEnumsOnlyField;

        private PrivateNullableEnumsOnly? _privatePrivateNullableEnumsOnlyField;

        public PublicNullableEnumsOnly? PublicNullableEnumsOnlyProperty { get; set; }

        private PublicNullableEnumsOnly? PrivatePublicNullableEnumsOnlyProperty { get; set; }

        private PrivateNullableEnumsOnly? PrivateNullableEnumsOnlyProperty { get; set; }

        public override bool Equals(object? obj)
        {
            return ClassEquals.AreClassesEqualFromFieldsAndProperties(this, obj);
        }

        public override string ToString()
        {
            return ClassToString.GetObjectToStringFromFieldAndProperties(this);
        }

        private enum PrivateNullableEnumsOnly
        {
            PrivateOne,
            PrivateTwo,
            PrivateThree
        }
    }

    public enum PublicNullableEnumsOnly
    {
        One,
        Two,
        Three
    }
}
