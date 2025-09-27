using System.Numerics;
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
    public int Angle { get; set; }

    [XmlAttribute(AttributeName = "gridSize")]
    public string GridSize { get; set; }

    [XmlAttribute(AttributeName = "isBackground")]
    public bool IsBackground { get; set; } = false;

    [XmlAttribute(AttributeName = "visible")]
    public bool Visible { get; set; } = true;

    [XmlIgnore]
    public Sprite Sprite { get; set; }

    public Vector2 GetPosition()
    {
        string[] parts = Position.Split(' ');

        if (parts.Length != 2)
            return Vector2.Zero;

        if (float.TryParse(parts[0], out float x) && float.TryParse(parts[1], out float y))
            return new Vector2(x, y);

        return Vector2.Zero;
    }

    public Vector2 GetGridSize()
    {
        string[] parts = GridSize.Split(' ');

        if (parts.Length != 2)
            return Vector2.Zero;

        if (float.TryParse(parts[0], out float width) && float.TryParse(parts[1], out float height))
            return new Vector2(width, height);

        return Vector2.Zero;
    }
}