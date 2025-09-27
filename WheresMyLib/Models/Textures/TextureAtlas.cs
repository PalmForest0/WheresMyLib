using SixLabors.ImageSharp;
using System.Xml.Serialization;
using WheresMyLib.Core;
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
    [XmlAttribute(AttributeName = "imgSize")]
    public string ImageSize { get; set; }

    [XmlAttribute(AttributeName = "file")]
    public string TexturePath { get; set; }

    [XmlAttribute(AttributeName = "textureBasePath")]
    private string TextureBasePath { get; set; }

    [XmlElement(ElementName = "Image")]
    public List<ImageRect> ImageRects { get; set; }

    public static TextureAtlas Load(string filepath, Game game)
    {
        TextureAtlas imageList = SerializerUtils.Deserialize<TextureAtlas>(filepath, game);

        // Provide each image with a reference to its parent atlas
        foreach (ImageRect imageRect in imageList.ImageRects)
            imageRect.ParentAtlas = imageList;

        return imageList;
    }

    public Image GetTexture()
    {
        // Attempt to load texture file
        string texturePath = Path.Join(Game.Assets.FullName, TexturePath.Replace("/", "\\"));
        if (File.Exists(texturePath))
        {
            using FileStream stream = new FileStream(texturePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Image.Load(stream);
        }

        return null;
    }
}
