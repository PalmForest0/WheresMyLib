using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WheresMyLib.Models;

namespace WheresMyLib.Core;

public class Game
{
    public string DirectoryPath { get; private set; }

    public DirectoryInfo Assets { get; private set; }


    private XmlSerializer levelSerializer = new XmlSerializer(typeof(Level));

    public Game(string directoryPath)
    {
        LoadGameFiles(directoryPath);
    }

    private void LoadGameFiles(string directoryPath)
    {
        DirectoryPath = directoryPath;
        Assets = new DirectoryInfo(directoryPath);

        LoadObjects(Path.Combine(Assets.FullName, "Objects"));
        LoadLevels(Path.Combine(Assets.FullName, "Levels"));
    }

    public void LoadObjects(string objectsPath)
    {

    }

    public void LoadLevels(string levelsPath)
    {

    }

    public Level LoadLevel(string levelPath)
    {
        using var stringReader = new StringReader(File.ReadAllText(levelPath));
        if(levelSerializer.Deserialize(stringReader) is Level level)
            return level;

        throw new InvalidOperationException($"Failed to deserialize {nameof(Level)} from file: {levelPath}");
    }
}