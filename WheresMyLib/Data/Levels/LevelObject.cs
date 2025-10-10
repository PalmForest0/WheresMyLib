using WheresMyLib.Data.Objects;
using WheresMyLib.Data.Types;

namespace WheresMyLib.Data.Levels;

/// <summary>
/// An instance of a <see cref="GameObject"/> placed in a <see cref="Level"/>.
/// </summary>
public class LevelObject
{
    public string Name { get; set; }
    public Pos AbsoluteLocation { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}