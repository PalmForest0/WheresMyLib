using WheresMyLib.Models.Levels;
using WheresMyLib.Models.Objects;
using WheresMyLib.Models.Sprites;
using WheresMyLib.Models.Textures;
using WheresMyLib.Utility;

namespace WheresMyLib.Core;

public class Game
{
    public string GamePath { get; private set; }
    public string AssetsPath { get; private set; }
    public string LevelsPath { get; private set; }
    public string TexturesPath { get; private set; }
    public string SpritesPath { get; private set; }
    public string ObjectsPath { get; private set; }

    public List<Level> Levels { get; private set; }
    public List<GameObject> Objects { get; private set; }
    public List<ImageAtlas> Atlases { get; private set; }
    public List<Sprite> Sprites { get; private set; }

    public Game(string gamePath)
    {
        GamePath = gamePath;
        AssetsPath = Path.Combine(gamePath, "assets");
        FileUtils.ValidateDirectory(AssetsPath);

        TexturesPath = Path.Combine(AssetsPath, "Textures");
        Atlases = LoadAllAtlases(TexturesPath, this);

        SpritesPath = Path.Combine(AssetsPath, "Sprites");
        Sprites = LoadAllSprites(SpritesPath, this);

        ObjectsPath = Path.Combine(AssetsPath, "Objects");
        Objects = LoadAllObjects(ObjectsPath, this);

        LevelsPath = Path.Combine(AssetsPath, "Levels");
        Levels = LoadAllLevels(LevelsPath, this);
    }

    public Level GetLevel(string name) => Levels.Find(l => l.FilePath.Contains(name, StringComparison.OrdinalIgnoreCase));
    public Level GetLevel(Func<Level, bool> filter) => Levels.First(filter);
    public IEnumerable<Level> GetLevels(Func<Level, bool> filter) => Levels.Where(filter);

    public GameObject GetObject(string name) => Objects.Find(o => o.FilePath.Contains(name, StringComparison.OrdinalIgnoreCase));
    public GameObject GetLevel(Func<GameObject, bool> filter) => Objects.First(filter);
    public IEnumerable<GameObject> GetLevels(Func<GameObject, bool> filter) => Objects.Where(filter);

    private static List<GameObject> LoadAllObjects(string objectsPath, Game game)
    {
        FileUtils.ValidateDirectory(objectsPath);
        List<GameObject> objects = new List<GameObject>();

        foreach (var file in FileUtils.GetFiles(objectsPath, f => f.Extension == ".hs"))
            objects.Add(GameObject.Load(file.FullName, game));

        return objects;
    }

    public static List<Level> LoadAllLevels(string levelsPath, Game game)
    {
        FileUtils.ValidateDirectory(levelsPath);
        List<Level> levels = new List<Level>();

        foreach (var file in FileUtils.GetFiles(levelsPath, f => f.Extension == ".xml"))
            levels.Add(Level.Load(file.FullName, game));

        return levels;
    }

    public static List<ImageAtlas> LoadAllAtlases(string atlasesPath, Game game)
    {
        FileUtils.ValidateDirectory(atlasesPath);
        List<ImageAtlas> atlases = new List<ImageAtlas>();

        foreach (var file in FileUtils.GetFiles(atlasesPath, f => f.Extension == ".imagelist"))
            atlases.Add(ImageAtlas.Load(file.FullName, game));

        return atlases;
    }

    public static List<Sprite> LoadAllSprites(string spritesPath, Game game)
    {
        FileUtils.ValidateDirectory(spritesPath);
        List<Sprite> sprites = new List<Sprite>();

        foreach (var file in FileUtils.GetFiles(spritesPath, f => f.Extension == ".sprite"))
            sprites.Add(Sprite.Load(file.FullName, game));

        return sprites;
    }

    //private static bool IsMatchingModel(RootModel model, string filter) => model.FileInfo.FullName.ToLower().Contains(filter.ToLower().Replace("/", "\\"));

    //public InteractiveObject GetObject(string name) => Objects.Find(obj => IsMatchingModel(obj, name));
    //public Level GetLevel(string name) => Levels.Find(level => FileUtils.MatchPath(level.FilePath, name));
    //public ImageAtlas GetTexture(string name) => TextureAtlases.Find(texture => IsMatchingModel(texture, name));
    //public Sprite GetSprite(string name) => Sprites.Find(sprite => IsMatchingModel(sprite, name));
}