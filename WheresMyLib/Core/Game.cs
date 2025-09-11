using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WheresMyLib.Models;

namespace WheresMyLib.Core;

public class Game
{
    public string DirectoryPath { get; private set; }
    public DirectoryInfo Assets { get; private set; }

    public List<Level> Levels { get; private set; }

    public Game(string directoryPath)
    {
        DirectoryPath = directoryPath;
        Assets = new DirectoryInfo(Path.Combine(directoryPath, "assets"));

        Levels = new List<Level>();

        LoadGameFiles(directoryPath);
    }

    private void LoadGameFiles(string directoryPath)
    {
        LoadAllObjects(Path.Combine(Assets.FullName, "Objects"));
        LoadAllLevels(Path.Combine(Assets.FullName, "Levels"));
    }

    public void LoadAllObjects(string objectsPath)
    {

    }

    public void LoadAllLevels(string levelsPath)
    {
        foreach (var file in new DirectoryInfo(levelsPath).EnumerateFiles().Where(f => f.Extension == ".xml"))
            Levels.Add(Level.Load(file.FullName));
    }
}