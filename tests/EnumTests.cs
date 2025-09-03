using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.ClassBased.Enums;
using System.Text;
using System.Xml;

namespace MmcSerializer.Tests;

[TestClass]
public class EnumTests
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

        XmlReaderSettings = new XmlReaderSettings(); ;
    }

    [TestMethod]
    public void TestXmlEnumsOnly()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var enumsOnly = new EnumsOnly();
        ClassRandomizer.RandomizeClassFieldAndProperties(enumsOnly);

        xmlSerializer.Serialize(enumsOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedStringsOnly = (EnumsOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedStringsOnly, "Deserialize strings only class was null");

        bool deserializedClassMatches = enumsOnly.Equals(deserializedStringsOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlEnumsOnlyWithNulls()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var nullableEnumsOnly = new NullableEnumsOnly();

        xmlSerializer.Serialize(nullableEnumsOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedStringsOnly = (NullableEnumsOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedStringsOnly, "Deserialize strings only class was null");

        bool deserializedClassMatches = nullableEnumsOnly.Equals(deserializedStringsOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }
}
