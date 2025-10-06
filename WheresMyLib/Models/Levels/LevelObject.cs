using WheresMyLib.Models.Objects;
using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// An instance of a <see cref="GameObject"/> placed in a <see cref="Level"/>.
/// </summary>
public class LevelObject
{
    public string Name { get; set; }
    public Pos AbsoluteLocation { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}