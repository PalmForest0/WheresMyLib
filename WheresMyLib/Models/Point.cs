using System.Xml.Serialization;

namespace WheresMyLib.Models;

/// <summary>
/// Represents a 2D position used in a <see cref="Shape"/> tag.
/// </summary>
[XmlRoot(ElementName = "Point")]
public class Point
{
    [XmlAttribute(AttributeName = "pos")]
    public string Position { get; set; }
}