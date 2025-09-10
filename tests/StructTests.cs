using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.ClassBased.Structs;
using MmcSerializer.Tests.Models.StructBased.Classes;
using MmcSerializer.Tests.Models.StructBased.Structs;
using System.Text;
using System.Xml;

namespace MmcSerializer.Tests;

[TestClass]
public class StructTests
{
    private static MmcSerializationOptions UniversalMmcOptions = null!;
    private static XmlWriterSettings XmlWriterSettings = null!;
    private static XmlReaderSettings XmlReaderSettings = null!;

    public TestContext? TestContext { get; set; }

    [ClassInitialize]
    public static void SetUp(TestContext context)
    {
        UniversalMmcOptions = new MmcSerializationOptions();

        XmlWriterSettings = new XmlWriterSettings()
        {
            Indent = true,
            IndentChars = "\t",
            NewLineOnAttributes = false,
            NewLineChars = Environment.NewLine,
        };

        XmlReaderSettings = new XmlReaderSettings();
    }

    [TestMethod]
    public void TestXmlStructOnlyInClass()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var structOnly = new StructOnly();
        ClassRandomizer.RandomizeClassFieldAndProperties(structOnly);

        xmlSerializer.Serialize(structOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedStructOnly = (StructOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedStructOnly, "Deserialized object was null");

        bool deserializedClassMatches = structOnly.Equals(deserializedStructOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlNullableStructOnlyInClass()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var nullableStructOnly = new NullableStructOnly();

        xmlSerializer.Serialize(nullableStructOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedNullableStructOnly = (NullableStructOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedNullableStructOnly, "Deserialized object was null");

        bool deserializedClassMatches = nullableStructOnly.Equals(deserializedNullableStructOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlStructOnlyInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var nestedStructOnly = new NestedStructOnly();
        ClassRandomizer.RandomizeStructFieldAndProperties(ref nestedStructOnly);

        xmlSerializer.Serialize(nestedStructOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedNestedStructOnly = (NestedStructOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedNestedStructOnly, "Deserialized object was null");

        bool deserializedClassMatches = nestedStructOnly.Equals(deserializedNestedStructOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlNestedStructOnlyInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var doubleNestedStructOnly = new DoubleNestedStructOnly();
        ClassRandomizer.RandomizeStructFieldAndProperties(ref doubleNestedStructOnly);

        xmlSerializer.Serialize(doubleNestedStructOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedDoubleNestedStructOnly = (DoubleNestedStructOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedDoubleNestedStructOnly, "Deserialized object was null");

        bool deserializedClassMatches = doubleNestedStructOnly.Equals(deserializedDoubleNestedStructOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlMultiNestedStructInClass()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var multiNestedStruct = new MultiNestedStruct();
        ClassRandomizer.RandomizeStructFieldAndProperties(ref multiNestedStruct);

        xmlSerializer.Serialize(multiNestedStruct);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedNullableStructOnly = (MultiNestedStruct?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedNullableStructOnly, "Deserialized object was null");

        bool deserializedClassMatches = multiNestedStruct.Equals(deserializedNullableStructOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlClassesInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var classInStruct = new ClassesInStruct();
        ClassRandomizer.RandomizeStructFieldAndProperties(ref classInStruct);

        xmlSerializer.Serialize(classInStruct);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedClassInStruct = (ClassesInStruct?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedClassInStruct, "Deserialized object was null");

        bool deserializedClassMatches = classInStruct.Equals(deserializedClassInStruct);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlNullClassesInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var classInStruct = new ClassesInStruct();

        xmlSerializer.Serialize(classInStruct);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedClassInStruct = (ClassesInStruct?)xmlSerializer.Deserialize();

        TestContext?.WriteLine("Original ToString: " + classInStruct);
        TestContext?.WriteLine("Deserialized ToString: " + deserializedClassInStruct);

        Assert.IsNotNull(deserializedClassInStruct, "Deserialized object was null");

        bool deserializedClassMatches = classInStruct.Equals(deserializedClassInStruct);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }
}
