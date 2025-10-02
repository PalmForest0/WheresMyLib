using SixLabors.ImageSharp;
using System.Xml.Linq;
using WheresMyLib.Core;
using WheresMyLib.Models.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// A level found in the game files at <c>assets/Levels</c>
/// </summary>
public class Level
{
    public Game Game { get; private set; }
    public string Name { get; private set; }

    public List<LevelObject> Objects { get; set; }
    public Room Room { get; set; }
    public Image Image { get; set; }
    public Dictionary<string, string> Properties { get; set; }

    public Level(string name, Game game)
    {
        Game = game;
        Name = FileUtils.SanitiseFileName(name);
    }

    public static Level Load(string filepath, Game game)
    {
        XDocument xml = XDocument.Load(filepath);

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

        // Load level room
        Room room = xml.Root.Element("Room") is null ? null : new Room()
        {
            AbsoluteLocation = Pos.FromAbsoluteLocation(xml.Root.Element("Room").Element("AbsoluteLocation"))
        };

        // Load level properties (optional)
        Dictionary<string, string> properties = XmlUtils.ParseProperties(xml.Root.Element("Properties"));

        // Attempt to load level PNG with the same name
        Image image = null;
        string imagePath = Path.ChangeExtension(filepath, ".png");

        if (File.Exists(imagePath))
        {
            using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            image = Image.Load(stream);
        }

        return new Level(Path.GetFileNameWithoutExtension(filepath), game)
        {
            Objects = objects,
            Room = room,
            Properties = properties,
            Image = image,
        };
    }


    /// <summary>
    /// Exports Level XML and PNG to the original location the level was loaded from. 
    /// </summary>
    public void Save() => Export(this, Game.LevelsPath);

    /// <summary>
    /// Exports Level XML and PNG to a custom directory.
    /// </summary>
    /// <param name="directoryPath">Custom directory path to export level data and image to.</param>
    public static void Export(Level level, string directoryPath)
    {
        XDocument xml = new XDocument(
            new XDeclaration("1.0", null, null),

            // Export level objects
            new XElement("Objects",
                level.Objects.Select(obj =>
                    new XElement("Object",
                        new XAttribute("name", obj.Name),
                        new XElement("AbsoluteLocation", new XAttribute("value", obj.AbsoluteLocation.ToString())),
                        new XElement("Properties", XmlUtils.ExportProperties(obj.Properties))
                    )
                )
            ),

            // Export level room
            new XElement("Room", new XElement("AbsoluteLocation", new XAttribute("value", level.Room.AbsoluteLocation.ToString()))),

            // Export level properties (optional)
            new XElement("Properties", XmlUtils.ExportProperties(level.Properties))
        );

        string xmlPath = Path.Join(directoryPath, Path.ChangeExtension(level.Name, ".xml"));
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