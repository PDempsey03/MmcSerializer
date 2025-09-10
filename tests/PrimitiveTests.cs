using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.ClassBased.Primitive;
using MmcSerializer.Tests.Models.StructBased.Primitive;
using System.Text;
using System.Xml;

namespace MmcSerializer.Tests;

[TestClass]
public class PrimitiveTests
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
    public void TestXmlIntegersOnlyInClass()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var intOnly = new IntegersOnly();
        ClassRandomizer.RandomizeClassFieldAndProperties(intOnly);

        xmlSerializer.Serialize(intOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedIntOnly = (IntegersOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedIntOnly, "Deserialized object was null");

        bool deserializedClassMatches = intOnly.Equals(deserializedIntOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlAllPrimitivesInClass()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var multiPrimitive = new MultiPrimitive();
        ClassRandomizer.RandomizeClassFieldAndProperties(multiPrimitive);

        xmlSerializer.Serialize(multiPrimitive);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedAllPrimitives = (MultiPrimitive?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedAllPrimitives, "Deserialized object was null");

        bool deserializedClassMatches = multiPrimitive.Equals(deserializedAllPrimitives);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlAllNullablePrimitivesInClass()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var nullableMultiPrimitive = new NullableMultiPrimitive();

        xmlSerializer.Serialize(nullableMultiPrimitive);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedAllNullablePrimitives = (NullableMultiPrimitive?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedAllNullablePrimitives, "Deserialized object was null");

        bool deserializedClassMatches = nullableMultiPrimitive.Equals(deserializedAllNullablePrimitives);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlFloatsOnlyInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var onlyFloats = new OnlyFloats();
        ClassRandomizer.RandomizeStructFieldAndProperties(ref onlyFloats);

        xmlSerializer.Serialize(onlyFloats);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedFloatOnly = (OnlyFloats?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedFloatOnly, "Deserialized object was null");

        bool deserializedClassMatches = onlyFloats.Equals(deserializedFloatOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlAllPrimitivesInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var multiPrimitiveStruct = new MultiPrimitiveStruct();
        ClassRandomizer.RandomizeStructFieldAndProperties(ref multiPrimitiveStruct);

        xmlSerializer.Serialize(multiPrimitiveStruct);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedIntOnly = (MultiPrimitiveStruct?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedIntOnly, "Deserialized object was null");

        bool deserializedClassMatches = multiPrimitiveStruct.Equals(deserializedIntOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlAllNullablePrimitivesInStruct()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var nullableMultiPrimitiveStruct = new NullableMultiPrimitiveStruct();

        xmlSerializer.Serialize(nullableMultiPrimitiveStruct);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedNullableMultiPrimitiveStruct = (NullableMultiPrimitiveStruct?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedNullableMultiPrimitiveStruct, "Deserialized object was null");

        bool deserializedClassMatches = nullableMultiPrimitiveStruct.Equals(deserializedNullableMultiPrimitiveStruct);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }
}
