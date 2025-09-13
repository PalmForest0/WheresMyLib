using SixLabors.ImageSharp;
using System.Xml.Serialization;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Levels;

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

    [XmlIgnore]
    public Image Texture { get; set; }

    public static Level Load(string filepath)
    {
        Level level = SerializerUtils.Deserialize<Level>(File.ReadAllText(filepath));

        // Attempt to load level PNG with the same name
        string imagePath = Path.ChangeExtension(filepath, ".png");
        if (File.Exists(imagePath))
        {
            using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using Image image = Image.Load(stream);

            level.Texture = image;
        }
        
        return level;
    }
}