using WheresMyLib.Core;

namespace WheresMyLib.Models;

public abstract class GameFile
{
    public Game Game { get; private init; }
    public string Name { get; private init; }

    public GameFile(string name, Game game)
    {
        Name = name;
        Game = game;
    }
}
