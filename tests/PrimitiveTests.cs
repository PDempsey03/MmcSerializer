using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.ClassBased.Primitive;
using System.Text;
using System.Xml;

namespace MmcSerializer.Tests;

[TestClass]
public class PrimitiveTests
{
    private static MmcSerializationOptions UniversalMmcOptions = null!;
    private static XmlWriterSettings XmlWriterSettings = null!;
    private static XmlReaderSettings XmlReaderSettings = null!;

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
    public void TestXmlIntegersOnly()
    {
        var xmlStringBuilder = new StringBuilder();
        var xmlAdapter = new XmlSerializerAdapter()
        {
            XmlWriter = XmlWriter.Create(xmlStringBuilder, XmlWriterSettings),
        };
        var xmlSerializer = new MmcSerializer(xmlAdapter, UniversalMmcOptions);

        var intOnly = new IntegersOnly(12, 20, 300, 5432678);

        xmlSerializer.Serialize(intOnly);

        string resultText = xmlStringBuilder.ToString();

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedIntOnly = (IntegersOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedIntOnly, "Deserialize int only class was null");

        bool deserializedClassMatches = intOnly.Equals(deserializedIntOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }

    [TestMethod]
    public void TestXmlAllPrimitives()
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

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedIntOnly = (MultiPrimitive?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedIntOnly, "Deserialize int only class was null");

        bool deserializedClassMatches = multiPrimitive.Equals(deserializedIntOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }
}
