namespace MmcSerializer.Tests.Models.TypeCategory
{
    public class TypeCategoryTestClass
    {
        public bool boolTest = true;

        public byte byteTest = 25;

        public sbyte sbyteTest = 25;

        public char charTest = 'a';

        public double doubleTest = 25.01;

        public float floatTest = 25.01f;

        public int intTest = 25;

        public uint uintTest = 25;

        public nint nintTest = 25;

        public nuint nuintTest = 25;

        public long longTest = 25;

        public ulong ulongTest = 25;

        public short shortTest = 25;

        public ushort ushortTest = 25;

        public bool? nullableBoolTest = true;

        public byte? nullableByteTest = 25;

        public sbyte? nullableSbyteTest = 25;

        public char? nullableCharTest = 'a';

        public double? nullableDoubleTest = 25.01;

        public float? nullableFloatTest = 25.01f;

        public int? nullableIntTest = 25;

        public uint? nullableUintTest = 25;

        public nint? nullableNintTest = 25;

        public nuint? nullableNuintTest = 25;

        public long? nullableLongTest = 25;

        public ulong? nullableUlongTest = 25;

        public short? nullableShortTest = 25;

        public ushort? nullableUshortTest = 25;

        public TypeCategoryTestEnum enumTest = TypeCategoryTestEnum.One;

        public TypeCategoryTestEnum? nullableEnumTest = TypeCategoryTestEnum.Two;

        public TypeCategoryTestStruct structTest = new TypeCategoryTestStruct();

        public TypeCategoryTestStruct? nullableStructTest = new TypeCategoryTestStruct();

        public TypeCategoryInternalTestClass classTest = new TypeCategoryInternalTestClass();

        public TypeCategoryInternalTestClass? nullableClassTest = new TypeCategoryInternalTestClass();

        public string stringTest = "test";

        public string? nullableStringTest = "test2";

        public int[] arrayTest = [1, 2, 3];

        public int[]? nullableArrayTest = [4, 5, 6];

        public Dictionary<int, string> dictionaryTest = new() { { 5, "hi" }, { 0, "no" } };

        public Dictionary<int, string>? nullableDictionaryTest = new() { { 20, "bye" }, { -5, "yes" } };

        public List<string> listTest = ["one", "two"];

        public List<string>? nullableListTest = ["three", "four"];
    }

    public enum TypeCategoryTestEnum
    {
        One,
        Two,
    }

    public struct TypeCategoryTestStruct
    {
        public int a;

        public string b;
    }

    public class TypeCategoryInternalTestClass
    {
        public int a;

        public float b;
    }
}
