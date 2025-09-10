using MmcSerializer.Tests.Models.ClassBased.Enums;
using MmcSerializer.Tests.Models.ClassBased.Primitive;
using MmcSerializer.Tests.Models.ClassBased.Strings;
using MmcSerializer.Tests.Models.ClassBased.Structs;

namespace MmcSerializer.Tests.Models.StructBased.Classes
{
    public struct ClassesInStruct
    {
        public MultiNestedStruct _multiNestedStruct;

        private MultiNestedStruct _privateMultiNestedStruct;

        public MultiNestedStruct MultiNestedStruct { get; set; }

        private MultiNestedStruct PrivateMultiNestedStruct { get; set; }

        public EnumsOnly _enumsOnly;

        private EnumsOnly _privateEnumsOnly;

        public EnumsOnly EnumsOnly { get; set; }

        private EnumsOnly PrivateEnumsOnly { get; set; }

        public MultiPrimitive _multiPrimitive;

        private MultiPrimitive _privateMultiPrimitive;

        public MultiPrimitive MultiPrimitive { get; set; }

        private MultiPrimitive PrivateMultiPrimitive { get; set; }

        public StringsOnly _stringsOnly;

        private StringsOnly _privateStringsOnly;

        public StringsOnly StringsOnly { get; set; }

        private StringsOnly PrivateStringsOnly { get; set; }

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
