using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.ClassBased.Structs;
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
}
