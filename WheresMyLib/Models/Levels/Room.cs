using System.Xml.Serialization;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// An object that represents the goal room of a <see cref="Level"/>.
/// </summary>
[XmlRoot(ElementName = "Room")]
public class Room
{
    [XmlElement(ElementName = "AbsoluteLocation")]
    public AbsoluteLocation AbsoluteLocation { get; set; }
}