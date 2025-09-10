using MmcSerializer.Tests.Models.StructBased.Primitive;
using MmcSerializer.Tests.Models.StructBased.Structs;

namespace MmcSerializer.Tests.Models.ClassBased.Structs
{
    public class MultiNestedStruct
    {
        public OnlyFloats _onlyFloats;

        private OnlyFloats _privateOnlyFloats;

        public OnlyFloats OnlyFloats { get; set; }

        private OnlyFloats PrivateOnlyFloats { get; set; }

        public NestedStructOnly _nestedStructOnly;

        private NestedStructOnly _privateNestedStructOnly;

        public NestedStructOnly NestedStructOnly { get; set; }

        private NestedStructOnly PrivateNestedStructOnly { get; set; }

        public DoubleNestedStructOnly _doubleNestedStructOnly;

        private DoubleNestedStructOnly _privateDoubleNestedStructOnly;

        public DoubleNestedStructOnly DoubleNestedStructOnly { get; set; }

        private DoubleNestedStructOnly PrivateDoubleNestedStructOnly { get; set; }

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
