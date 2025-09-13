using System.Xml.Serialization;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Image data XML tag used in an <see cref="InteractiveObject"/>.
/// </summary>
[XmlRoot(ElementName = "Sprite")]
public class ObjectSprite
{
    [XmlAttribute(AttributeName = "filename")]
    public string Filename { get; set; }

    [XmlAttribute(AttributeName = "pos")]
    public string Pos { get; set; }

    [XmlAttribute(AttributeName = "angle")]
    public string Angle { get; set; }

    [XmlAttribute(AttributeName = "gridSize")]
    public string GridSize { get; set; }

    [XmlAttribute(AttributeName = "isBackground")]
    public bool IsBackground { get; set; }
}