using System.Xml.Serialization;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// A shape of <see cref="Point"/>s used in a <see cref="SpriteReference"/>.
/// </summary>
public class ObjectShape
{
    [XmlElement(ElementName = "Point")]
    public List<Point> Points { get; set; }
}
