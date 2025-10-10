using WheresMyLib.Data.Types;

namespace WheresMyLib.Data.Objects;

/// <summary>
/// A shape that defines points used in a <see cref="ObjectSprite"/>.
/// </summary>
public class ObjectShape
{
    public List<Pos> Points { get; set; }
}
