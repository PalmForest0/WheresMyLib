using System.Xml.Serialization;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Represents a 2D position used in a <see cref="ObjectShape"/> tag.
/// </summary>
[XmlRoot(ElementName = "Point")]
public class ShapePoint
{
    [XmlAttribute(AttributeName = "pos")]
    public string Position { get; set; }
}