using System.Xml.Serialization;
using WheresMyLib.Models.Objects;

namespace WheresMyLib.Models;

/// <summary>
/// Represents a 2D position used in a <see cref="ObjectShape"/> tag.
/// </summary>
[XmlRoot(ElementName = "Point")]
public class ShapePoint
{
    [XmlAttribute(AttributeName = "pos")]
    public string Position { get; set; }
}