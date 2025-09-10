using System.Xml.Serialization;

namespace WheresMyLib.Models;

/// <summary>
/// A level found in the game files at <c>assets/Levels</c>
/// </summary>
[XmlRoot(ElementName = "Objects")]
public class Level
{
    [XmlElement(ElementName = "Object")]
    public List<LevelObject> Objects { get; set; }
    [XmlElement(ElementName = "Room")]
    public Room Room { get; set; }
}