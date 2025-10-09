using WheresMyLib.Models.Textures;

namespace WheresMyLib.Models.Sprites;

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
}