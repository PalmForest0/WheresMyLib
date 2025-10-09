using SixLabors.ImageSharp;
using System.Xml.Linq;
using WheresMyLib.Core;
using WheresMyLib.Models.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// A level found in the game files at <c>assets/Levels</c>
/// </summary>
public class Level(string filePath, Game game) : GameFile(filePath, game), IGameFileLoader<Level>
{
    public List<LevelObject> Objects { get; set; }
    public Room Room { get; set; }
    public Image Image { get; set; }
    public Dictionary<string, string> Properties { get; set; }

    public static Level Load(string filePath, Game game)
    {
        XDocument xml = XDocument.Load(filePath);

        // Load level objects
        List<LevelObject> objects = xml.Root
            .Elements("Object")
            .Select(obj => new LevelObject()
            {
                Name = obj.Attribute("name").Value,
                AbsoluteLocation = Pos.FromAbsoluteLocation(obj.Element("AbsoluteLocation")),
                Properties = XmlUtils.ParseProperties(obj.Element("Properties"))
            })
            .ToList();

        // Load level room (why do some levels not have a room ._. how is that possible?)
        Room room = xml.Root.Element("Room") is null ? null : new Room()
        {
            AbsoluteLocation = Pos.FromAbsoluteLocation(xml.Root.Element("Room").Element("AbsoluteLocation"))
        };

        // Load level properties (optional)
        Dictionary<string, string> properties = XmlUtils.ParseProperties(xml.Root.Element("Properties"));

        // Attempt to load level PNG with the same filePath
        Image image = null;
        string imagePath = Path.ChangeExtension(filePath, ".png");

        if (File.Exists(imagePath))
        {
            using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            image = Image.Load(stream);
        }

        return new Level(filePath, game)
        {
            Objects = objects,
            Room = room,
            Properties = properties,
            Image = image
        };
    }


    /// <summary>
    /// Saves this <see cref="Level"/> as an <c>XML</c> file and a <c>PNG</c> image with a matching filePath to the loaded game's <c>LevelPath</c>.
    /// </summary>
    public void Save() => Export(this, Game.LevelsPath);

    /// <summary>
    /// Saves this <see cref="Level"/> as an <c>XML</c> file and a <c>PNG</c> image with a matching filePath to a different directoryPath.
    /// </summary>
    /// <param name="directoryPath">Directory path to export the <see cref="Level"/> data and image to.</param>
    public void Save(string directoryPath) => Export(this, directoryPath);

    /// <summary>
    /// Exports this <see cref="Level"/>'s XML data and PNG image to a custom directoryPath.
    /// </summary>
    /// <param name="directoryPath">Custom directoryPath path to export level data and image to.</param>
    public static void Export(Level level, string directoryPath)
    {
        XDocument xml = new XDocument(
            new XElement("Objects",

                // Export level objects
                level.Objects.Select(obj => new XElement("Object",
                    new XAttribute("name", obj.Name),
                    new XElement("AbsoluteLocation", new XAttribute("value", obj.AbsoluteLocation.ToString())),
                    new XElement("Properties", XmlUtils.ExportProperties(obj.Properties))
                )),

                // Export level room (also optional apparently)
                level.Room is not null
                    ? new XElement("Room", new XElement("AbsoluteLocation", new XAttribute("value", level.Room.AbsoluteLocation.ToString())))
                    : null,

                // Export level properties (optional)
                level.Properties.Any() ? new XElement("Properties", XmlUtils.ExportProperties(level.Properties)) : null
            )
        );

        Directory.CreateDirectory(directoryPath);
        string xmlPath = Path.Join(directoryPath, $"{level.FileName}.xml");
        xml.Save(xmlPath);

        // Save the image if it exists (some hidden levels in the files don't)
        if (level.Image is not null)
        {
            string imagePath = Path.ChangeExtension(xmlPath, ".png");
            using FileStream stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write);
            level.Image.SaveAsPng(stream);
        }
    }
}