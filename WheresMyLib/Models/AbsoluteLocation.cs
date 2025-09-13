using System.Xml.Serialization;
using WheresMyLib.Models.Levels;

namespace WheresMyLib.Models;

/// <summary>
/// A location property that represents a location of a <see cref="Level"/> or Object.
/// </summary>
[XmlRoot(ElementName = "AbsoluteLocation")]
public class AbsoluteLocation
{
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}