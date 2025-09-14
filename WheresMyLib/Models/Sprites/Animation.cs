using System.Xml.Serialization;
using WheresMyLib.Models.Textures;

namespace WheresMyLib.Models.Sprites;

[XmlRoot(ElementName = "Animation")]
public class Animation
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    
    [XmlAttribute(AttributeName = "textureBasePath")]
    public string TextureBasePath { get; set; }
    
    [XmlAttribute(AttributeName = "atlas")]
    public string AtlasPath { get; set; }
    
    [XmlAttribute(AttributeName = "fps")]
    public float FrameRate { get; set; }
    
    [XmlAttribute(AttributeName = "playbackMode")]
    public string PlaybackMode { get; set; }
    
    [XmlAttribute(AttributeName = "loopCount")]
    public int LoopCount { get; set; }
    
    [XmlElement(ElementName = "Frame")]
    public List<Frame> Frames { get; set; }
    
    [XmlIgnore]
    public TextureAtlas Atlas { get; set; }
}