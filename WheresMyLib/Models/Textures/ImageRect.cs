using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Xml.Serialization;
using WheresMyLib.Models.Types;

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
    [XmlIgnore] public TextureAtlas ParentAtlas { get; set; }
    [XmlIgnore] public Pos Offset { get; set; }
    [XmlIgnore] public Pos Size { get; set; }
    [XmlIgnore] public Rect Rect { get; set; }


    [XmlAttribute(AttributeName = "offset")]
    private string OffsetString
    {
        get => Offset.ToString();
        set => Offset = Pos.FromString(value);
    }

    [XmlAttribute(AttributeName = "size")]
    private string SizeString
    {
        get => Size.ToString();
        set => Size = Pos.FromString(value);
    }

    [XmlAttribute(AttributeName = "rect")]
    private string RectString
    {
        get => Rect.ToString();
        set => Rect = Rect.FromString(value);
    }

    /// <summary>
    /// Exports the image 
    /// </summary>
    /// <returns></returns>
    public Image ExportImage()
    {
        Image texture = ParentAtlas.GetImageFile();

        if (texture is null)
            return null;

        // Get cropped image using the paresed rect
        Image croppedImage = texture.Clone(ctx => ctx.Crop((int)Rect.Width, (int)Rect.Height));
        return croppedImage;
    }
}