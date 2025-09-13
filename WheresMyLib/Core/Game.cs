using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WheresMyLib.Exceptions;
using WheresMyLib.Models.Levels;
using WheresMyLib.Models.Objects;
using WheresMyLib.Models.Textures;
using WheresMyLib.Utility;

namespace WheresMyLib.Core;

public class Game
{
    public string DirectoryPath { get; private set; }
    public DirectoryInfo Assets { get; private set; }

    public List<Level> Levels { get; private set; }
    public List<InteractiveObject> Objects { get; private set; }
    public List<TextureData> Textures { get; private set; }

    public Game(string directoryPath)
    {
        string assetsPath = Path.Combine(directoryPath, "assets");
        FileUtils.ValidateDirectory(assetsPath);

        DirectoryPath = directoryPath;
        Assets = new DirectoryInfo(assetsPath);

        Levels = new List<Level>();
        Objects = new List<InteractiveObject>();
        Textures = new List<TextureData>();

        LoadGameFiles(directoryPath);
    }

    private void LoadGameFiles(string directoryPath)
    {
        LoadAllObjects(Path.Combine(Assets.FullName, "Objects"));
        LoadAllLevels(Path.Combine(Assets.FullName, "Levels"));
        LoadAllTextures(Path.Combine(Assets.FullName, "Textures"));
    }

    public void LoadAllObjects(string objectsPath)
    {
        FileUtils.ValidateDirectory(objectsPath);

        foreach (var file in FileUtils.GetFiles(objectsPath, f => f.Extension == ".hs"))
            Objects.Add(InteractiveObject.Load(file.FullName));
    }

    public void LoadAllLevels(string levelsPath)
    {
        FileUtils.ValidateDirectory(levelsPath);

        foreach (var file in FileUtils.GetFiles(levelsPath, f => f.Extension == ".xml"))
            Levels.Add(Level.Load(file.FullName));
    }

    public void LoadAllTextures(string texturesPath)
    {
        FileUtils.ValidateDirectory(texturesPath);

        foreach (var file in FileUtils.GetFiles(texturesPath, f => f.Extension == ".imagelist"))
            Textures.Add(TextureData.Load(file.FullName));
    }
}