using System.Xml.Linq;
using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Models.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Object template found in the game files at <c>assets/Objects/</c>.
/// </summary>
[XmlRoot(ElementName = "InteractiveObject")]
public class GameObject(string name, Game game) : GameFile(name, game)
{
    public List<ObjectShape> Shapes { get; set; }
    public List<SpriteReference> Sprites { get; set; }
    public Dictionary<string, string> DefaultProperties { get; set; }

    public IEnumerable<SpriteReference> BackgroundSprites => Sprites.Where(s => s.IsBackground);
    public IEnumerable<SpriteReference> ForegroundSprites => Sprites.Where(s => !s.IsBackground);

    public static GameObject Load(string filepath, Game game)
    {
        XDocument xml = XDocument.Load(filepath);

        // Load Object sprites and seperate them by isBackground attribute
        List<SpriteReference> sprites = xml.Root.Element("Sprites").Elements("Sprite").Select(e =>
        {
            SpriteReference spr = ParseSprite(e);
            spr.Sprite = game.Sprites.Find(s => FileUtils.MatchPath(s.FileInfo.FullName, spr.Filename));
            return spr;
        }).ToList();

        // Nothing new about loading properties
        Dictionary<string, string> defaultProperties = XmlUtils.ParseProperties(xml.Root.Element("DefaultProperties"));

        // Load Object shapes and the points for each shape
        List<ObjectShape> shapes = xml.Root.Element("Shapes") is null ? [] : xml.Root.Element("Shapes").Elements("Shape").Select(s => new ObjectShape()
        {
            Points = s.Elements("Point").Select(p => Pos.FromString(p.Attribute("pos").Value)).ToList()
        }).ToList();

        return new GameObject(Path.GetFileNameWithoutExtension(filepath), game)
        {
            Shapes = shapes,
            Sprites = sprites,
            DefaultProperties = defaultProperties
        };
    }

    private static SpriteReference ParseSprite(XElement spriteElement) => new SpriteReference()
    {
        Filename = spriteElement.Attribute("filename").Value,
        Angle = int.Parse(spriteElement.Attribute("angle").Value),
        Position = Pos.FromString(spriteElement.Attribute("pos").Value),
        GridSize = Pos.FromString(spriteElement.Attribute("gridSize").Value),
        IsBackground = bool.Parse((string)spriteElement.Attribute("isBackground") ?? "false"),
        Visible = bool.Parse((string)spriteElement.Attribute("visible") ?? "true"),
    };

    //public Image GetCombinedTexture()
    //{
    //    List<SpriteReference> sprites = new List<SpriteReference>();

    //    foreach (SpriteReference sprRef in Sprites)
    //    {
    //        if (sprRef is null || sprRef.Sprite is null)
    //            continue;
    //        if (!sprRef.Visible)
    //            continue;

    //        Sprite sprite = sprRef.Sprite;

    //        if (sprite.Animations.IsNullOrEmpty())
    //            continue;
    //        if (sprite.Animations[0].Frames.IsNullOrEmpty())
    //            continue;

    //        Frame frame = sprite.Animations[0].Frames[0];

    //        if (frame is not null && frame.ImageRect is not null)
    //            sprites.Add(sprRef);
    //    }

    //    if (sprites.Count == 0)
    //        return null;

    //    // Create canvas to combine all images
    //    Image<Rgba32> canvas = new Image<Rgba32>(500, 500);

    //    canvas.Mutate(ctx =>
    //    {
    //        foreach (SpriteReference sprite in sprites)
    //        {
    //            Frame frame = sprite.Sprite.Animations[0].Frames[0];
    //            using Image frameImage = frame.ImageRect.GetCroppedImage();

    //            Point size = frame.ImageRect.GetSize();
    //            Vector2 gridSize = sprite.GetGridSize();
    //            //size = new Point((int)(size.X * gridSize.X), (int)(size.Y * gridSize.Y));

    //            Point offset = frame.ImageRect.GetOffset();
    //            offset = new Point(offset.X + (int)sprite.GetPosition().X, offset.Y + (int)sprite.GetPosition().Y);  // funny
    //            offset = new Point((int)(offset.X * gridSize.X), (int)(offset.Y * gridSize.Y));

    //            // Calculate center position
    //            int x = (canvas.Width - size.X) / 2 + offset.X;
    //            int y = (canvas.Height - size.Y) / 2 + offset.Y;

    //            // Apply rotation from 'angle'
    //            if (sprite.Angle != 0)
    //                frameImage.Mutate(frame => frame.Rotate(sprite.Angle));

    //            ctx.DrawImage(frameImage, new Point(x, y), 1.0f);
    //        }
    //    });

    //    return canvas.CropToContent();
    //}
}