using System.Xml.Linq;

namespace WheresMyLib.Utility;

public static class XmlUtils
{
    public static Dictionary<string, string> ParseProperties(XElement propertiesElement)
        => propertiesElement is null ? [] : propertiesElement.Elements("Property").ToDictionary(p => p.Attribute("name").Value, p => p.Attribute("value").Value);

    public static IEnumerable<XElement> ExportProperties(Dictionary<string, string> properties)
        => properties.Select(p => new XElement("Property", new XAttribute("name", p.Key), new XAttribute("value", p.Value)));
}
