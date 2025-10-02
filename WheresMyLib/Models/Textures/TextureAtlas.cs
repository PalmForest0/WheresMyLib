using SixLabors.ImageSharp;
using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Models.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Textures;

/// <summary>
/// Provides a list of <see cref="ImageRect"/>s from a single texture file. Example:
/// <br/>
/// <code><![CDATA[<ImageList imgSize="128 512" file="/Textures/Balloons.webp" textureBasePath="/Textures/">]]></code>
/// </summary>
[XmlRoot(ElementName = "ImageList")]
public class TextureAtlas : RootModel
{
    [XmlAttribute(AttributeName = "drawScale")]
    public float DrawScale { get; set; }

    [XmlIgnore]
    public Pos ImageSize { get; set; }

    [XmlAttribute(AttributeName = "file")]
    public string ImagePath { get; set; }

    [XmlElement(ElementName = "Image")]
    public List<ImageRect> Rects { get; set; }

    [XmlAttribute(AttributeName = "imgSize")]
    private string ImageSizeString
    {
        get => ImageSize.ToString();
        set => ImageSize = Pos.FromString(value);
    }

    public static TextureAtlas Load(string filepath, Game game)
    {
        TextureAtlas imageList = SerializerUtils.Deserialize<TextureAtlas>(filepath, game);

        // Provide each image with a reference to its parent atlas
        foreach (ImageRect imageRect in imageList.Rects)
            imageRect.ParentAtlas = imageList;

        return imageList;
    }

    public Image GetImageFile()
    {
        // Attempt to load texture file
        string texturePath = Path.Join(Game.AssetsPath, ImagePath.Replace("/", "\\"));
        if (File.Exists(texturePath))
        {
            using FileStream stream = new FileStream(texturePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Image.Load(stream);
        }

        return null;
    }
}
