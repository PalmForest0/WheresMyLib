using SixLabors.ImageSharp;
using System.Xml.Linq;
using WheresMyLib.Core;
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
            Animations = xml.Root.Elements("Animation").Select(anim => Animation.ParseAnimation(anim, game)).ToList()
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