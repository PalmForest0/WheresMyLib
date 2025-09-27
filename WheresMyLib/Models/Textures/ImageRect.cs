using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Xml.Serialization;

namespace WheresMyLib.Models.Textures;

/// <summary>
/// Specifies the rectangle of a texture that corresponds to an image. Example:
/// <br/>
/// <code><![CDATA[<Image name="Balloon_Texture.png" offset="0 0" size="64 64" rect="1 196 64 64"/>]]></code>
/// </summary>
[XmlRoot(ElementName = "Image")]
public class ImageRect
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "offset")]
    public string Offset { get; set; }

    [XmlAttribute(AttributeName = "size")]
    public string Size { get; set; }

    [XmlAttribute(AttributeName = "rect")]
    public string Rect { get; set; }

    [XmlIgnore]
    public TextureAtlas ParentAtlas { get; set; }

    public Image GetCroppedImage()
    {
        Image texture = ParentAtlas.GetTexture();

        if (texture is null)
            return null;

        // Get cropped image using the paresed rect
        Rectangle cropRect = GetRectangle();
        Image croppedImage = texture.Clone(ctx => ctx.Crop(cropRect));
        return croppedImage;
    }

    public Rectangle GetRectangle()
    {
        string[] parts = Rect.Split(' ');

        if (parts.Length != 4)
            return Rectangle.Empty;

        if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y) && int.TryParse(parts[2], out int width) && int.TryParse(parts[3], out int height))
            return new Rectangle(x, y, width, height);

        return Rectangle.Empty;
    }

    public Point GetOffset()
    {
        string[] parts = Offset.Split(' ');

        if (parts.Length != 2)
            return Point.Empty;

        if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
            return new Point(x, y);

        return Point.Empty;
    }

    public Point GetSize()
    {
        string[] parts = Size.Split(' ');

        if (parts.Length != 2)
            return Point.Empty;

        if (int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
            return new Point(x, y);

        return Point.Empty;
    }
}