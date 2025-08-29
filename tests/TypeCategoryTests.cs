using MmcSerializer.Extensions;
using MmcSerializer.Tests.Models.TypeCategory;

namespace MmcSerializer.Tests;

[TestClass]
public class TypeCategoryTests
{
    private static TypeCategoryTestClass Test { get; set; } = null!;

    [ClassInitialize]
    public static void Setup(TestContext testContext)
    {
        Test = new();
    }

    [TestMethod]
    public void TestPrimitives()
    {
        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.boolTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.byteTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.sbyteTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.charTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        // TODO: move to struct since decimal is technically a struct not a primitive
        /*decimal decimalTest = 25.01M;
        Assert.AreEqual(TypeCategory.Primitive, decimalTest.GetType().GetTypeCategory());*/

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.doubleTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.floatTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.intTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.uintTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nintTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nuintTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.longTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.ulongTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.shortTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Primitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.ushortTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        // move to nullables

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableBoolTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableByteTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableSbyteTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableCharTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableDoubleTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableFloatTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableIntTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableUintTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableNintTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableNuintTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableLongTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableUlongTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableShortTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullablePrimitive,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableUshortTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }

    [TestMethod]
    public void TestEnums()
    {
        Assert.AreEqual(TypeCategory.Enum,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.enumTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullableEnum,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableEnumTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }

    [TestMethod]
    public void TestStructs()
    {
        Assert.AreEqual(TypeCategory.Struct,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.structTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.NullableStruct,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableStructTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }

    [TestMethod]
    public void TestClasses()
    {
        Assert.AreEqual(TypeCategory.Class,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.classTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Class,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableClassTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }

    [TestMethod]
    public void TestStrings()
    {
        Assert.AreEqual(TypeCategory.String,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.stringTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.String,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableStringTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }

    [TestMethod]
    public void TestArrays()
    {
        Assert.AreEqual(TypeCategory.Array,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.arrayTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Array,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableArrayTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }

    [TestMethod]
    public void TestEnumerables()
    {
        Assert.AreEqual(TypeCategory.Enumerable,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.dictionaryTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Enumerable,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.listTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Enumerable,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableDictionaryTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);

        Assert.AreEqual(TypeCategory.Enumerable,
            typeof(TypeCategoryTestClass).GetField(nameof(Test.nullableListTest))?.FieldType.GetTypeCategory() ?? TypeCategory.Unknown);
    }
}
