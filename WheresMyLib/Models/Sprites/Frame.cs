using System.Xml.Serialization;
using WheresMyLib.Models.Textures;

namespace WheresMyLib.Models.Sprites;

[XmlRoot(ElementName = "Frame")]
public class Frame
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    
    [XmlAttribute(AttributeName = "offset")]
    public string Offset { get; set; }

    [XmlAttribute(AttributeName = "repeat")]
    public int Repeat { get; set; } = 0;
    
    [XmlIgnore]
    public ImageData ImageData { get; set; }
}