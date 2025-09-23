using System.Xml.Serialization;
using WheresMyLib.Models.Sprites;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Image data XML tag used in an <see cref="InteractiveObject"/>.
/// </summary>
public class SpriteReference
{
    [XmlAttribute(AttributeName = "filename")]
    public string Filename { get; set; }

    [XmlAttribute(AttributeName = "pos")]
    public string Position { get; set; }

    [XmlAttribute(AttributeName = "angle")]
    public string Angle { get; set; }

    [XmlAttribute(AttributeName = "gridSize")]
    public string GridSize { get; set; }

    [XmlAttribute(AttributeName = "isBackground")]
    public bool IsBackground { get; set; } = false;

    [XmlIgnore]
    public Sprite Sprite { get; set; }
}