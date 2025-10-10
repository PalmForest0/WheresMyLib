using SixLabors.ImageSharp;
using System.Xml.Linq;
using WheresMyLib.Core;
using WheresMyLib.Data.Textures;
using WheresMyLib.Data.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Data.Sprites;

public class Sprite(string filePath, Game game) : GameFile(filePath, game), IGameFileLoader<Sprite>
{
    public List<Animation> Animations { get; set; }

    public static Sprite Load(string filePath, Game game)
    {
        XDocument xml = XDocument.Load(filePath);

        return new Sprite(filePath, game)
        {
            Animations = xml.Root.Elements("Animation").Select(anim => LoadAnimation(anim, game)).ToList()
        };
    }

    private static Animation LoadAnimation(XElement elem, Game game)
    {
        var anim = new Animation()
        {
            Name = (string)elem.Attribute("name"),
            AtlasPath = (string)elem.Attribute("atlas"),
            Fps = float.Parse((string)elem.Attribute("fps")),
            LoopCount = int.Parse((string)elem.Attribute("loopCount")),
            PlaybackMode = (string)elem.Attribute("playbackMode")
        };

        anim.Atlas = game.GetTexture(anim.AtlasPath);

        // Create lookup dictionary of atlas rects for fast searching in LoadFrame() calls
        Dictionary<string, ImageRect> rectLookup = anim.Atlas is null ? null : anim.Atlas.Rects.ToDictionary(r => r.Name, r => r);
        anim.Frames = elem.Elements("Frame").Select(frame => LoadFrame(frame, rectLookup, game)).ToList();

        return anim;
    }

    private static Frame LoadFrame(XElement elem, Dictionary<string, ImageRect> rectLookup, Game game)
    {
        string imageName = elem.Attribute("name").Value;

        return new Frame()
        {
            ImageName = imageName,
            Offset = Pos.FromString((string)elem.Attribute("offset")),
            Scale = Pos.FromString((string)elem.Attribute("scale")),
            Angle = float.TryParse((string)elem.Attribute("angleDeg"), out float angle) ? angle : 0,
            Repeat = int.TryParse((string)elem.Attribute("repeat"), out int repeat) ? repeat : 0,
            AtlasRect = rectLookup is null ? null : rectLookup.TryGetValue(imageName, out ImageRect rect) ? rect : null
        };
    }

    /// <summary>
    /// Saves this <see cref="Sprite"/> as an XML file with the <c>.sprite</c> extension to the loaded game's <c>SpritesPath</c>.
    /// </summary>
    public void Save() => Export(this, Game.SpritesPath);

    /// <summary>
    /// Saves this <see cref="Sprite"/> as an XML file with the <c>.sprite</c> extension to a different directoryPath.
    /// </summary>
    /// <param name="directoryPath">Directory path to export the <c>.sprite</c> file to.</param>
    public void Save(string directoryPath) => Export(this, directoryPath);

    /// <summary>
    /// Exports a <see cref="Sprite"/> as an XML file with the <c>.sprite</c> extension to a specified directory.
    /// </summary>
    /// <param name="directoryPath">Directory path to export the <c>.sprite</c> file to.</param>
    public static void Export(Sprite sprite, string directoryPath)
    {
        XDocument xml = new XDocument(new XElement("Sprite",
            sprite.Animations.Select(anim => new XElement("Animation",

                // Export all Animation attributes
                XmlUtils.OptionalAttribute("name", anim.Name),
                new XAttribute("textureBasePath", anim.TextureBasePath),
                XmlUtils.OptionalAttribute("atlas", anim.AtlasPath),
                new XAttribute("fps", anim.Fps == 1 ? "1.0" : anim.Fps), // Gotta add that .0 after the 1 lol
                XmlUtils.OptionalAttribute("playbackMode", anim.PlaybackMode),
                anim.LoopCount <= 0 ? null : new XAttribute("loopCount", anim.LoopCount),

                // Export all Frames
                anim.Frames.Select(frame => new XElement("Frame",
                    new XAttribute("name", frame.ImageName),
                    XmlUtils.OptionalAttribute("offset", frame.Offset),
                    XmlUtils.OptionalAttribute("scale", frame.Scale),
                    frame.Angle == 0 ? null : new XAttribute("angleDeg", frame.Angle),
                    frame.Repeat <= 0 ? null : new XAttribute("repeat", frame.Repeat)
                ))
            ))
        ));

        Directory.CreateDirectory(directoryPath);
        string xmlPath = Path.Join(directoryPath, $"{sprite.FileName}.sprite");
        XmlUtils.SaveXml(xml, xmlPath);
    }
}