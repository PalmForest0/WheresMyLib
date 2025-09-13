using System.Xml.Serialization;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Object template found in the game files at <c>assets/Objects/</c>.
/// </summary>
[XmlRoot(ElementName = "InteractiveObject")]
public class InteractiveObject
{
    [XmlArray(ElementName = "Shapes")]
    public List<ObjectShape> Shapes { get; set; }

    [XmlArray(ElementName = "Sprites")]
    public List<ObjectSprite> Sprites { get; set; }

    [XmlArray(ElementName = "DefaultProperties")]
    public List<Property> DefaultProperties { get; set; }

    public static InteractiveObject Load(string filepath)
    {
        InteractiveObject obj = SerializerUtils.Deserialize<InteractiveObject>(File.ReadAllText(filepath));
        return obj;
    }
}