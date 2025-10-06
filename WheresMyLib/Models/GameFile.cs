using WheresMyLib.Core;

namespace WheresMyLib.Models;

public abstract class GameFile
{
    public Game Game { get; private init; }
    public string FilePath { get; private init; }
    public string FileName { get; private init; }

    public GameFile(string filePath, Game game)
    {
        FilePath = filePath;
        FileName = Path.GetFileNameWithoutExtension(filePath);
        Game = game;
    }
}
