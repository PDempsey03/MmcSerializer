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

        public IntegersOnly(int field, int property, int privateField, int privateProperty)
        {
            _integerField = field;
            IntegerProperty = property;
            _privateIntegerField = privateField;
            PrivateIntegerProperty = privateProperty;
        }

        public IntegersOnly()
        {
            _integerField = Random.Shared.Next(int.MinValue, int.MaxValue);
            IntegerProperty = Random.Shared.Next(int.MinValue, int.MaxValue);
            _privateIntegerField = Random.Shared.Next(int.MinValue, int.MaxValue);
            PrivateIntegerProperty = Random.Shared.Next(int.MinValue, int.MaxValue);
        }

        public override string ToString()
        {
            return $"_integerField = {_integerField}\nIntegerProperty = {IntegerProperty}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not IntegersOnly other) return false;

            return _integerField == other._integerField && IntegerProperty == other.IntegerProperty
                && _privateIntegerField == other._privateIntegerField && PrivateIntegerProperty == other.PrivateIntegerProperty;
        }
    }
}
