using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WheresMyLib.Models;
using WheresMyLib.Utility;

namespace WheresMyLib.Core;

public class Game
{
    public string DirectoryPath { get; private set; }
    public DirectoryInfo Assets { get; private set; }

    public List<Level> Levels { get; private set; }
    public List<InteractiveObject> Objects { get; private set; }

    public Game(string directoryPath)
    {
        DirectoryPath = directoryPath;
        Assets = new DirectoryInfo(Path.Combine(directoryPath, "assets"));

        Levels = new List<Level>();
        Objects = new List<InteractiveObject>();

        LoadGameFiles(directoryPath);
    }

    private void LoadGameFiles(string directoryPath)
    {
        LoadAllObjects(Path.Combine(Assets.FullName, "Objects"));
        LoadAllLevels(Path.Combine(Assets.FullName, "Levels"));
    }

    public void LoadAllObjects(string objectsPath)
    {
        foreach (var file in FileUtils.GetFiles(objectsPath, f => f.Extension == ".hs"))
            Objects.Add(InteractiveObject.Load(file.FullName));
    }

    public void LoadAllLevels(string levelsPath)
    {
        foreach (var file in FileUtils.GetFiles(levelsPath, f => f.Extension == ".xml"))
            Levels.Add(Level.Load(file.FullName));
    }
}