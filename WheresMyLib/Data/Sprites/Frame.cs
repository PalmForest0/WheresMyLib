using WheresMyLib.Data.Textures;
using WheresMyLib.Data.Types;

namespace WheresMyLib.Data.Sprites;

/// <summary>
/// A frame that is part of an <see cref="Animation"/>, specifying an image found in an <see cref="ImageAtlas"/>
/// </summary>
public class Frame
{
    public string ImageName { get; set; }
    public ImageRect AtlasRect { get; set; }

    public Pos Offset { get; set; }
    public Pos Scale { get; set; }
    public float Angle { get; set; }
    public int Repeat { get; set; }
}