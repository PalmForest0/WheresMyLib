using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// A shape that defines points used in a <see cref="ObjectSprite"/>.
/// </summary>
public class ObjectShape
{
    public List<Pos> Points { get; set; }
}
