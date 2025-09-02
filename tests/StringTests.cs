using MmcSerializer.Adapters;
using MmcSerializer.Tests.Models.Strings;
using System.Text;
using System.Xml;

namespace MmcSerializer.Tests;

[TestClass]
public class StringTests
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

        Assert.IsTrue(resultText.Length > 0);

        xmlAdapter.XmlReader = XmlReader.Create(new StringReader(resultText), XmlReaderSettings);

        var deserializedStringsOnly = (StringsOnly?)xmlSerializer.Deserialize();

        Assert.IsNotNull(deserializedStringsOnly, "Deserialize strings only class was null");

        bool deserializedClassMatches = stringsOnly.Equals(deserializedStringsOnly);

        Assert.IsTrue(deserializedClassMatches, "Xml serializer failed");
    }
}
