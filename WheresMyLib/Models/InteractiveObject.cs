using System.Xml.Serialization;

namespace WheresMyLib.Models;

/// <summary>
/// Represents an Object template found in the game files at <c>assets/Objects/</c>
/// </summary>
public class InteractiveObject
{
    [XmlIgnore]
    public string Name { get; set; }
    [XmlIgnore]
    public string Path { get; set; }

    [XmlElement("DefaultProperties")]
    public Property[] DefaultProperties { get; set; }
}
