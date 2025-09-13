using System.Xml.Serialization;

namespace WheresMyLib.Models.Textures;

/// <summary>
/// Specifies the rectangle of a texture that corresponds to an image. Example:
/// <br/>
/// <code><![CDATA[<Image name="Balloon_Texture.png" offset="0 0" size="64 64" rect="1 196 64 64"/>]]></code>
/// </summary>
[XmlRoot(ElementName = "Image")]
public class ImageData
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlAttribute(AttributeName = "offset")]
    public string Offset { get; set; }

    [XmlAttribute(AttributeName = "size")]
    public string Size { get; set; }

    [XmlAttribute(AttributeName = "rect")]
    public string Rect { get; set; }
}
