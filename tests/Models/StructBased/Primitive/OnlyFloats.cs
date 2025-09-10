using MmcSerializer.Attributes;

namespace MmcSerializer.Tests.Models.StructBased.Primitive
{
    public struct OnlyFloats
    {
        [MmcSerializable]
        public float _floatField;

        [MmcSerializable]
        private float _privateFloatField;

        [MmcSerializable]
        public float FloatProperty { get; set; }

        [MmcSerializable]
        private float PrivateFloatProperty { get; set; }

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
