using System.Xml.Serialization;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// An Object instance placed in a <see cref="Level"/>.
/// </summary>
[XmlRoot(ElementName = "Object")]
public class LevelObject
{
    [XmlElement(ElementName = "AbsoluteLocation")]
    public AbsoluteLocation AbsoluteLocation { get; set; }

    [XmlArray(ElementName = "Properties")]
    public List<Property> Properties { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}