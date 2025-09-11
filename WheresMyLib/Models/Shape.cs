using System.Xml.Serialization;

namespace WheresMyLib.Models;

/// <summary>
/// A shape of <see cref="Point"/>s used in a <see cref="Sprite"/>.
/// </summary>
[XmlRoot(ElementName = "Shape")]
public class Shape
{
    [XmlElement(ElementName = "Point")]
    public List<Point> Points { get; set; }
}
