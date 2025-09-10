using MmcSerializer.Tests.Models.StructBased.Primitive;

namespace MmcSerializer.Tests.Models.StructBased.Structs
{
    public struct NestedStructOnly
    {
        public OnlyFloats _onlyFloats;

        private OnlyFloats _privateOnlyFloats;

        public OnlyFloats OnlyFloats { get; set; }

        private OnlyFloats PrivateOnlyFloats { get; set; }

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
