using System.Xml.Serialization;

namespace WheresMyLib.Models;

/// <summary>
/// Represents an Object placed in a <see cref="Level"/>.
/// </summary>
public class LevelObject
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("AbsoluteLocation")]
    public string Location { get; set; }

    [XmlElement("Properties")]
    public Property[] Properties { get; set; }
}
