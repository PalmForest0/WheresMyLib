using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WheresMyLib.Exceptions;
using WheresMyLib.Models;
using WheresMyLib.Models.Levels;
using WheresMyLib.Models.Objects;
using WheresMyLib.Models.Sprites;
using WheresMyLib.Models.Textures;
using WheresMyLib.Utility;

namespace WheresMyLib.Core;

public class Game
{
    public string DirectoryPath { get; private set; }
    public DirectoryInfo Assets { get; private set; }

    public List<Level> Levels { get; private set; }
    public List<InteractiveObject> Objects { get; private set; }
    public List<TextureAtlas> Textures { get; private set; }
    public List<Sprite> Sprites { get; private set; }
    
    public Game(string directoryPath)
    {
        string assetsPath = Path.Combine(directoryPath, "assets");
        FileUtils.ValidateDirectory(assetsPath);

        DirectoryPath = directoryPath;
        Assets = new DirectoryInfo(assetsPath);

        Textures = new List<TextureAtlas>();
        Sprites = new List<Sprite>();
        Levels = new List<Level>();
        Objects = new List<InteractiveObject>();

        LoadGameFiles(Assets.FullName);
    }

    private void LoadGameFiles(string assetsPath)
    {
        LoadAllTextures(Path.Combine(assetsPath, "Textures"));
        LoadAllSprites(Path.Combine(assetsPath, "Sprites"));
        LoadAllObjects(Path.Combine(assetsPath, "Objects"));
        LoadAllLevels(Path.Combine(assetsPath, "Levels"));
    }

    private void LoadAllObjects(string objectsPath)
    {
        FileUtils.ValidateDirectory(objectsPath);

        foreach (var file in FileUtils.GetFiles(objectsPath, f => f.Extension == ".hs"))
            Objects.Add(InteractiveObject.Load(file.FullName, this));
    }

    private void LoadAllLevels(string levelsPath)
    {
        FileUtils.ValidateDirectory(levelsPath);

        foreach (var file in FileUtils.GetFiles(levelsPath, f => f.Extension == ".xml"))
            Levels.Add(Level.Load(file.FullName, this));
    }

    private void LoadAllTextures(string texturesPath)
    {
        FileUtils.ValidateDirectory(texturesPath);

        foreach (var file in FileUtils.GetFiles(texturesPath, f => f.Extension == ".imagelist"))
            Textures.Add(TextureAtlas.Load(file.FullName, this));
    }
    
    private void LoadAllSprites(string spritesPath)
    {
        FileUtils.ValidateDirectory(spritesPath);
        
        foreach(var file in FileUtils.GetFiles(spritesPath, f => f.Extension == ".sprite"))
            Sprites.Add(Sprite.Load(file.FullName, this));
    }
    
    private static bool IsMatchingModel(RootModel model, string filter) => model.FileInfo.FullName.ToLower().Contains(filter.ToLower().Replace("/", "\\"));
    
    public InteractiveObject GetObject(string name) => Objects.Find(obj => IsMatchingModel(obj, name));
    public Level GetLevel(string name) => Levels.Find(level => IsMatchingModel(level, name));
    public TextureAtlas GetTexture(string name) => Textures.Find(texture => IsMatchingModel(texture, name));
    public Sprite GetSprite(string name) => Sprites.Find(sprite => IsMatchingModel(sprite, name));
}