using WheresMyLib.Models.Sprites;
using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Image data XML tag used in an <see cref="GameObject"/>.
/// </summary>
public class SpriteReference
{
    public string Filename { get; set; }
    public int Angle { get; set; }
    public Pos Position { get; set; }
    public Pos GridSize { get; set; }
    public bool IsBackground { get; set; } = false;
    public bool Visible { get; set; } = true;

    public Sprite Sprite { get; set; }
}