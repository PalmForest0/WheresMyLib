using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// An Object instance placed in a <see cref="Level"/>.
/// </summary>
public class LevelObject
{
    public string Name { get; set; }
    public Pos AbsoluteLocation { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}