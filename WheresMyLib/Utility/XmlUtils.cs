using System.Xml;
using System.Xml.Linq;

namespace WheresMyLib.Utility;

public static class XmlUtils
{
    public static Dictionary<string, string> ParseProperties(XElement propertiesElement)
        => propertiesElement is null ? [] : propertiesElement
        .Elements("Property")
        .GroupBy(p => (string)p.Attribute("name")) // Group duplicates
        .ToDictionary(g => g.Key, g => (string)g.First().Attribute("value"));

    public static IEnumerable<XElement> ExportProperties(Dictionary<string, string> properties)
        => properties.Select(p => new XElement("Property", new XAttribute("name", p.Key), new XAttribute("value", p.Value)));

    public static XAttribute OptionalAttribute(string name, object value) => value is null ? null : new XAttribute(name, value);

    public static void SaveXml(XDocument xml, string path)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "    ",
        };

        using var writer = XmlWriter.Create(path, settings);
        xml.Save(writer);
    }
}
