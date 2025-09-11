using System.Xml.Serialization;

namespace WheresMyLib.Models;

/// <summary>
/// Object template found in the game files at <c>assets/Objects/</c>.
/// </summary>
[XmlRoot(ElementName = "InteractiveObject")]
public class InteractiveObject
{
    [XmlArray(ElementName = "Shapes")]
    public List<Shape> Shapes { get; set; }

    [XmlArray(ElementName = "Sprites")]
    public List<Sprite> Sprites { get; set; }

    [XmlArray(ElementName = "DefaultProperties")]
    public List<Property> DefaultProperties { get; set; }
}