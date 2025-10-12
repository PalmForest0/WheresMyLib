using WheresMyLib.Data;
using WheresMyLib.Data.Levels;
using WheresMyLib.Data.Objects;
using WheresMyLib.Data.Sprites;
using WheresMyLib.Data.Textures;
using WheresMyLib.Utility;

namespace WheresMyLib.Core;

public class Game
{
    public GameType Type { get; private set; }
    public GameOptions Options { get; private set; }

    public string GamePath { get; private set; }
    public string AssetsPath { get; private set; }
    public string LevelsPath { get; private set; }
    public string TexturesPath { get; private set; }
    public string SpritesPath { get; private set; }
    public string ObjectsPath { get; private set; }

    public List<Level> Levels { get; private set; }
    public List<GameObject> Objects { get; private set; }
    public List<ImageAtlas> Textures { get; private set; }
    public List<Sprite> Sprites { get; private set; }

    private Game() { }

    public static Game Load(string rootPath, GameType type = GameType.WheresMyWater, GameOptions options = GameOptions.None)
    {
        FileUtils.ValidateGameFiles(rootPath);

        // TODO: add options for spinoff games
        string assetsPath = Path.Combine(rootPath, "assets");

        var game = new Game()
        {
            GamePath = rootPath,
            AssetsPath = assetsPath,

            TexturesPath = Path.Combine(assetsPath, "Textures"),
            SpritesPath = Path.Combine(assetsPath, "Sprites"),
            ObjectsPath = Path.Combine(assetsPath, "Objects"),
            LevelsPath = Path.Combine(assetsPath, "Levels")
        };

        game.Type = type;
        game.Options = options;

        game.Textures = LoadGameFiles<ImageAtlas>(game.TexturesPath, game, file => file.Extension == ".imagelist");
        game.Sprites = LoadGameFiles<Sprite>(game.SpritesPath, game, file => file.Extension == ".sprite");
        game.Objects = LoadGameFiles<GameObject>(game.ObjectsPath, game, file => file.Extension == ".hs");
        game.Levels = LoadGameFiles<Level>(game.LevelsPath, game, file => file.Extension == ".xml");

        return game;
    }

    private T GetGameFileByName<T>(IEnumerable<T> gameFiles, string name) where T : GameFile
        => gameFiles.FirstOrDefault(f => FileUtils.MatchPath(f.FilePath, name));

    private static List<T> LoadGameFiles<T>(string directory, Game game, Func<FileInfo, bool> filter) where T : GameFile, IGameFileLoader<T>
        => FileUtils.GetFiles(directory, filter).Select(file => T.Load(file.FullName, game)).ToList();


    public Level GetLevel(string name) => GetGameFileByName(Levels, name);
    public Level GetLevel(Func<Level, bool> filter) => Levels.First(filter);

    public GameObject GetObject(string name) => GetGameFileByName(Objects, name);
    public GameObject GetObject(Func<GameObject, bool> filter) => Objects.First(filter);

    public ImageAtlas GetTexture(string name) => GetGameFileByName(Textures, name);
    public ImageAtlas GetTexture(Func<ImageAtlas, bool> filter) => Textures.First(filter);

    public Sprite GetSprite(string name) => GetGameFileByName(Sprites, name);
    public Sprite GetSprite(Func<Sprite, bool> filter) => Sprites.First(filter);
}