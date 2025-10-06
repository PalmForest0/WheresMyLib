using WheresMyLib.Models.Sprites;
using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Adds a sprite from the <c>assets/Sprites</c> directory to this <see cref="GameObject"/> with additional data.
/// </summary>
public class ObjectSprite
{
    public string FileName { get; set; }
    public int Angle { get; set; }
    public Pos Position { get; set; }
    public Pos GridSize { get; set; }
    public bool IsBackground { get; set; } = false;
    public bool Visible { get; set; } = true;

    public List<Animation> Animations { get; set; }
}