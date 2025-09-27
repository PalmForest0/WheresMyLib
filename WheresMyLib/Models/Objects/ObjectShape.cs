using System.Xml.Serialization;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// A shape of <see cref="ShapePoint"/>s used in a <see cref="SpriteReference"/>.
/// </summary>
public class ObjectShape
{
    [XmlElement(ElementName = "Point")]
    public List<ShapePoint> Points { get; set; }
}
