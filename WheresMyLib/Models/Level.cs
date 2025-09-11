using SixLabors.ImageSharp;
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
    [XmlIgnore]
    public Image Image { get; set; }


    private static XmlSerializer LevelSerializer = new XmlSerializer(typeof(Level));

    public static Level Load(string filepath)
    {
        using var stringReader = new StringReader(File.ReadAllText(filepath));
        if (LevelSerializer.Deserialize(stringReader) is not Level level)
            throw new InvalidOperationException($"Failed to deserialize {nameof(Level)} from file: {filepath}");

        string imagePath = Path.ChangeExtension(filepath, ".png");
        if (File.Exists(imagePath))
        {
            using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using Image image = Image.Load(stream);

            level.Image = image;
        }
        
        return level;
    }
}