using MmcSerializer.Tests.Models.StructBased.Primitive;

namespace MmcSerializer.Tests.Models.ClassBased.Structs
{
    public class NullableStructOnly
    {
        public OnlyFloats? _onlyFloats = null;

        private OnlyFloats? _privateOnlyFloats = null;

        public OnlyFloats? OnlyFloats { get; set; } = null;

        private OnlyFloats? PrivateOnlyFloats { get; set; } = null;

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
