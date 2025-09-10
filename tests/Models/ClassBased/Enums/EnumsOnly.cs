namespace MmcSerializer.Tests.Models.ClassBased.Enums
{
    public class EnumsOnly
    {
        public PublicEnumsOnly _publicEnumsOnlyField;

        private PublicEnumsOnly _privatePublicEnumsOnlyField;

        private PrivateEnumsOnly _privatePrivateEnumsOnlyField;

        public PublicEnumsOnly PublicEnumsOnlyProperty { get; set; }

        private PublicEnumsOnly PrivatePublicEnumsOnlyProperty { get; set; }

        private PrivateEnumsOnly PrivateEnumsOnlyProperty { get; set; }

        public override bool Equals(object? obj)
        {
            return ClassEquals.AreClassesEqualFromFieldsAndProperties(this, obj);
        }

        public override string ToString()
        {
            return ClassToString.GetObjectToStringFromFieldAndProperties(this);
        }

        private enum PrivateEnumsOnly
        {
            PrivateOne,
            PrivateTwo,
            PrivateThree
        }
    }

    public enum PublicEnumsOnly
    {
        One,
        Two,
        Three
    }
}
