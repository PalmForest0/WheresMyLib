using System.Xml.Linq;
using WheresMyLib.Core;
using WheresMyLib.Data.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Data.Objects;

/// <summary>
/// Object template found in the game files at <c>assets/Objects/</c>.
/// </summary>
public class GameObject(string filePath, Game game) : GameFile(filePath, game), IGameFileLoader<GameObject>
{
    public List<ObjectShape> Shapes { get; set; }
    public List<ObjectSprite> Sprites { get; set; }
    public Dictionary<string, string> DefaultProperties { get; set; }

    public IEnumerable<ObjectSprite> BackgroundSprites => Sprites.Where(s => s.IsBackground);
    public IEnumerable<ObjectSprite> ForegroundSprites => Sprites.Where(s => !s.IsBackground);

    public static GameObject Load(string filePath, Game game)
    {
        XDocument xml = XDocument.Load(filePath);

        // Load Object sprites and seperate them by isBackground attribute
        List<ObjectSprite> sprites = xml.Root.Element("Sprites").Elements("Sprite").Select(e => ParseSprite(e, game)).ToList();

        // Nothing new about loading properties
        Dictionary<string, string> defaultProperties = XmlUtils.ParseProperties(xml.Root.Element("DefaultProperties"));

        // Load Object shapes and the points for each shape
        List<ObjectShape> shapes = xml.Root.Element("Shapes") is null ? [] : xml.Root.Element("Shapes").Elements("Shape").Select(s => new ObjectShape()
        {
            Points = s.Elements("Point").Select(p => Pos.FromString(p.Attribute("pos").Value)).ToList()
        }).ToList();

        return new GameObject(filePath, game)
        {
            Shapes = shapes,
            Sprites = sprites,
            DefaultProperties = defaultProperties
        };
    }

    private static ObjectSprite ParseSprite(XElement spriteElement, Game game)
    {
        var spr = new ObjectSprite()
        {
            FileName = spriteElement.Attribute("filename").Value,
            Angle = int.Parse(spriteElement.Attribute("angle").Value),
            Position = Pos.FromString(spriteElement.Attribute("pos").Value),
            GridSize = Pos.FromString(spriteElement.Attribute("gridSize").Value),
            IsBackground = bool.Parse((string)spriteElement.Attribute("isBackground") ?? "false"),
            Visible = bool.Parse((string)spriteElement.Attribute("visible") ?? "true"),
        };

        spr.Animations = game.GetSprite(spr.FileName)?.Animations;
        return spr;
    }


    /// <summary>
    /// Saves this <see cref="GameObject"/> as an XML file with the <c>.hs</c> extension to the loaded game's <c>ObjectsPath</c>.
    /// </summary>
    public void Save() => Export(this, Game.ObjectsPath);

    /// <summary>
    /// Saves this <see cref="GameObject"/> as an XML file with the <c>.hs</c> extension to a different directoryPath.
    /// </summary>
    /// <param name="directoryPath">Directory path to export the <c>.hs</c> file to.</param>
    public void Save(string directoryPath) => Export(this, directoryPath);

    /// <summary>
    /// Exports the modified GameObject to the specified directoryPath as a <c>.hs</c> file.
    /// </summary>
    /// <param name="directoryPath">Custom directoryPath path to export InteractiveObject data to.</param>
    public static void Export(GameObject obj, string directoryPath)
    {
        XDocument xml = new XDocument(
            new XElement("InteractiveObject",
                // Export InteractiveObject Shapes
                obj.Shapes.Any() ? new XElement("Shapes",
                    obj.Shapes.Select(shape =>
                        new XElement("Shape", shape.Points.Select(p =>
                            new XElement("Point", new XAttribute("pos", p.ToString())))
                        )
                    )
                ) : null,

                // Export InteractiveObject Sprites
                obj.Sprites.Any() ? new XElement("Sprites",
                    obj.Sprites.Select(spr => new XElement("Sprite",
                        new XAttribute("filename", spr.FileName),
                        new XAttribute("pos", spr.Position.ToString()),
                        new XAttribute("angle", spr.Angle),
                        new XAttribute("gridSize", spr.GridSize.ToString()),
                        !spr.Visible ? new XAttribute("visible", spr.Visible) : null,
                        spr.IsBackground ? new XAttribute("isBackground", spr.IsBackground) : null
                    ))
                ) : null,

                // Export InteractiveObject DefaultProperties
                obj.DefaultProperties.Any() ? new XElement("DefaultProperties",
                    obj.DefaultProperties.Select(p => new XElement("Property",
                        new XAttribute("name", p.Key),
                        new XAttribute("value", p.Value)
                    ))
                ) : null
            )
        );

        Directory.CreateDirectory(directoryPath);
        string xmlPath = Path.Join(directoryPath, $"{obj.FileName}.hs");
        XmlUtils.SaveXml(xml, xmlPath);
    }
}