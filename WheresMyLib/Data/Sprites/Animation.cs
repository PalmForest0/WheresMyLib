using System.Xml.Linq;
using WheresMyLib.Core;
using WheresMyLib.Data.Textures;
using WheresMyLib.Data.Types;

namespace WheresMyLib.Data.Sprites;

public class Animation
{
    public string Name { get; set; }
    public float Fps { get; set; }
    public string PlaybackMode { get; set; }
    public int LoopCount { get; set; }

    public string AtlasPath { get; set; }
    public ImageAtlas Atlas { get; set; }

    /// <summary>
    /// Contains the root path of all texture images. Always equal to <c>"/Textures/"</c>.
    /// </summary>
    public string TextureBasePath { get; } = "/Textures/";

    public List<Frame> Frames { get; set; }

    internal static Animation ParseAnimation(XElement elem, Game game)
    {
        var anim = new Animation()
        {
            Name = (string)elem.Attribute("name"),
            AtlasPath = (string)elem.Attribute("atlas"),
            Fps = float.Parse((string)elem.Attribute("fps")),
            LoopCount = int.Parse((string)elem.Attribute("loopCount")),
            PlaybackMode = (string)elem.Attribute("playbackMode")
        };

        string[] pathParts = anim.AtlasPath.Split('.');
        // Try loading TabHD texture if UseHDTextures is enabled
        if (game.Options.HasFlag(GameOptions.UseHDTextures))
            anim.Atlas = game.GetTexture($"{pathParts[0]}-TabHD.{pathParts[^1]}");
        // If it's not found, try loading HD texture
        if (anim.Atlas is null && game.Options.HasFlag(GameOptions.UseHDTextures))
            anim.Atlas = game.GetTexture($"{pathParts[0]}-HD.{pathParts[^1]}");
        // Fallback to normal texture if that fails
        if (anim.Atlas is null) anim.Atlas = game.GetTexture(anim.AtlasPath);

        // Create lookup dictionary of atlas rects for fast searching in LoadFrame() calls
        Dictionary<string, ImageRect> rectLookup = anim.Atlas is null ? null : anim.Atlas.Rects.ToDictionary(r => r.Name, r => r);
        anim.Frames = elem.Elements("Frame").Select(frame => ParseFrame(frame, rectLookup, game)).ToList();

        return anim;
    }

    private static Frame ParseFrame(XElement elem, Dictionary<string, ImageRect> rectLookup, Game game)
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
}