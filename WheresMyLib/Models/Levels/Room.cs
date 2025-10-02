using WheresMyLib.Models.Types;

namespace WheresMyLib.Models.Levels;

/// <summary>
/// An object that represents the goal room of a <see cref="Level"/>.
/// </summary>
public class Room
{
    public Pos AbsoluteLocation { get; set; }
}