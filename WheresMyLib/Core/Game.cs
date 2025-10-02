using WheresMyLib.Models.Levels;
using WheresMyLib.Utility;

namespace WheresMyLib.Core;

public class Game
{
    public string GamePath { get; private set; }
    public string AssetsPath { get; private set; }
    public string LevelsPath { get; private set; }

    public List<Level> Levels { get; private set; }
    //public List<InteractiveObject> Objects { get; private set; }
    //public List<TextureAtlas> TextureAtlases { get; private set; }
    //public List<Sprite> Sprites { get; private set; }

    public Game(string gamePath)
    {
        GamePath = gamePath;
        AssetsPath = Path.Combine(gamePath, "assets");
        LevelsPath = Path.Combine(AssetsPath, "Levels");

        FileUtils.ValidateDirectory(AssetsPath);

        //TextureAtlases = new List<TextureAtlas>();
        //Sprites = new List<Sprite>();
        //Levels = new List<Level>();
        //Objects = new List<InteractiveObject>();

        LoadGameFiles();
    }

    public Level GetLevel(string name) => Levels.Find(l => l.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    public Level GetLevel(Func<Level, bool> filter) => Levels.First(filter);
    public IEnumerable<Level> GetLevels(Func<Level, bool> filter) => Levels.Where(filter);

    private void LoadGameFiles()
    {
        //LoadAllTextures(Path.Combine(AssetsPath, "Textures"));
        //LoadAllSprites(Path.Combine(AssetsPath, "Sprites"));
        //LoadAllObjects(Path.Combine(AssetsPath, "Objects"));
        Levels = LoadAllLevels(LevelsPath, this);
    }

    //private void LoadAllObjects(string objectsPath)
    //{
    //    FileUtils.ValidateDirectory(objectsPath);

    //    foreach (var file in FileUtils.GetFiles(objectsPath, f => f.Extension == ".hs"))
    //        Objects.Add(InteractiveObject.Load(file.FullName, this));
    //}

    public static List<Level> LoadAllLevels(string levelsPath, Game game)
    {
        FileUtils.ValidateDirectory(levelsPath);
        List<Level> levels = new List<Level>();

        foreach (var file in FileUtils.GetFiles(levelsPath, f => f.Extension == ".xml"))
            levels.Add(Level.Load(file.FullName, game));

        return levels;
    }

    //private void LoadAllTextures(string texturesPath)
    //{
    //    FileUtils.ValidateDirectory(texturesPath);

    //    foreach (var file in FileUtils.GetFiles(texturesPath, f => f.Extension == ".imagelist"))
    //        TextureAtlases.Add(TextureAtlas.Load(file.FullName, this));
    //}

    //private void LoadAllSprites(string spritesPath)
    //{
    //    FileUtils.ValidateDirectory(spritesPath);

    //    foreach (var file in FileUtils.GetFiles(spritesPath, f => f.Extension == ".sprite"))
    //        Sprites.Add(Sprite.Load(file.FullName, this));
    //}

    //private static bool IsMatchingModel(RootModel model, string filter) => model.FileInfo.FullName.ToLower().Contains(filter.ToLower().Replace("/", "\\"));

    //public InteractiveObject GetObject(string name) => Objects.Find(obj => IsMatchingModel(obj, name));
    //public Level GetLevel(string name) => Levels.Find(level => FileUtils.MatchPath(level.Name, name));
    //public TextureAtlas GetTexture(string name) => TextureAtlases.Find(texture => IsMatchingModel(texture, name));
    //public Sprite GetSprite(string name) => Sprites.Find(sprite => IsMatchingModel(sprite, name));
}