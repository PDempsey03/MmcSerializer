using MmcSerializer.Attributes;

namespace MmcSerializer.Tests.Models.ClassBased.Primitive
{
    public class IntegersOnly
    {
        [MmcSerializable]
        public int _integerField;

        [MmcSerializable]
        private int _privateIntegerField;

        [MmcSerializable]
        public int IntegerProperty { get; set; }

        [MmcSerializable]
        private int PrivateIntegerProperty { get; set; }

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
