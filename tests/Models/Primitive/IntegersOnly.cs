using MmcSerializer.Attributes;

namespace MmcSerializer.Tests.Models.Primitive
{
    public class IntegersOnly
    {
        [MmcSerializable]
        public int _integerField;

        [MmcSerializable]
        public int IntegerProperty { get; set; }

        public IntegersOnly(int field, int property)
        {
            _integerField = field;
            IntegerProperty = property;
        }

        public IntegersOnly()
        {
            _integerField = Random.Shared.Next(int.MinValue, int.MaxValue);
            IntegerProperty = Random.Shared.Next(int.MinValue, int.MaxValue);
        }

        public override string ToString()
        {
            return $"_integerField = {_integerField}\nIntegerProperty = {IntegerProperty}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not IntegersOnly other) return false;

            return _integerField == other._integerField && IntegerProperty == other.IntegerProperty;
        }
    }
}
