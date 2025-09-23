using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Models.Sprites;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Object template found in the game files at <c>assets/Objects/</c>.
/// </summary>
[XmlRoot(ElementName = "InteractiveObject")]
public class InteractiveObject : RootModel
{
    [XmlArray(ElementName = "Shapes")]
    [XmlArrayItem(ElementName = "Shape")]
    public List<ObjectShape> Shapes { get; set; }

    [XmlArray(ElementName = "Sprites")]
    [XmlArrayItem(ElementName = "Sprite")]
    public List<SpriteReference> Sprites { get; set; }

    [XmlArray(ElementName = "DefaultProperties")]
    [XmlArrayItem(ElementName = "Property")]
    public List<Property> DefaultProperties { get; set; }

    public static InteractiveObject Load(string filepath, Game game)
    {
        InteractiveObject obj = SerializerUtils.Deserialize<InteractiveObject>(filepath, game);

        foreach (SpriteReference sprRef in obj.Sprites)
        {
            Sprite sprite = game.GetSprite(sprRef.Filename);
            if (sprite is not null)
                sprRef.Sprite = sprite;
        }

        return obj;
    }
}