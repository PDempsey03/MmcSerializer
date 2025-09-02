using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.ClassBased.Strings;
using System.Text;
using System.Xml;

namespace MmcSerializer.Tests;

[TestClass]
public class StringTests
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
    public void TestXmlStringsOnly()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var stringsOnly = new StringsOnly();
        ClassRandomizer.RandomizeClassFieldAndProperties(stringsOnly);

        xmlSerializer.Serialize(stringsOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedStringsOnly = (StringsOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedStringsOnly, "Deserialize strings only class was null");

        bool deserializedClassMatches = stringsOnly.Equals(deserializedStringsOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlStringsOnlyWithNulls()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var stringsOnly = new StringsOnly();
        ClassRandomizer.RandomizeClassFieldAndProperties(stringsOnly);
        stringsOnly.PropertyString = null;
        stringsOnly._stringField = null;

        xmlSerializer.Serialize(stringsOnly);

        string resultText = xmlStringBuilder.ToString();

        TestContext?.WriteLine($"Serialized XML:\n{resultText}");

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedStringsOnly = (StringsOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedStringsOnly, "Deserialize strings only class was null");

        bool deserializedClassMatches = stringsOnly.Equals(deserializedStringsOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }
}
