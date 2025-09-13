using System.Xml.Serialization;
using WheresMyLib.Models.Levels;

namespace WheresMyLib.Models;

/// <summary>
/// Custom <see cref="Level"/> Object property with a name and a value.
/// </summary>
[XmlRoot(ElementName = "Property")]
public class Property
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}