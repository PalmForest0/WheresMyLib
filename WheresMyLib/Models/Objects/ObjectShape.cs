using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// A shape of <see cref="ShapePoint"/>s used in a <see cref="SpriteReference"/>.
/// </summary>
public class ObjectShape
{
    public List<Pos> Points { get; set; }
}
