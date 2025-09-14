using SixLabors.ImageSharp;
using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Textures;

/// <summary>
/// Provides a list of <see cref="ImageData"/>s from a single texture file. Example:
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
    public string TextureBasePath { get; set; }

    [XmlElement(ElementName = "Image")]
    public List<ImageData> Images { get; set; }

    [XmlIgnore]
    public Image Texture { get; set; }

    public static TextureAtlas Load(string filepath, Game game)
    {
        TextureAtlas imageList = SerializerUtils.Deserialize<TextureAtlas>(filepath, game);

        // Attempt to load texture file
        if (File.Exists(imageList.TexturePath))
        {
            using FileStream stream = new FileStream(imageList.TexturePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using Image image = Image.Load(stream);
            imageList.Texture = image;
        }

        return imageList;
    }
}
