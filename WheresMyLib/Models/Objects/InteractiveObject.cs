using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Models.Sprites;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Objects;

/// <summary>
/// Object template found in the game files at <c>assets/Objects/</c>.
/// </summary>
[XmlRoot(ElementName = "InteractiveObject")]
public class InteractiveObject : RootModel
{
    [XmlArray(ElementName = "Shapes")]
    [XmlArrayItem(ElementName = "Shape")]
    public List<ObjectShape> Shapes { get; set; }

    [XmlArray(ElementName = "Sprites")]
    [XmlArrayItem(ElementName = "Sprite")]
    public List<SpriteReference> Sprites { get; set; }

    [XmlArray(ElementName = "DefaultProperties")]
    [XmlArrayItem(ElementName = "Property")]
    public List<Property> DefaultProperties { get; set; }

    public static InteractiveObject Load(string filepath, Game game)
    {
        InteractiveObject obj = SerializerUtils.Deserialize<InteractiveObject>(filepath, game);

        foreach (SpriteReference sprRef in obj.Sprites)
        {
            Sprite sprite = game.GetSprite(sprRef.Filename);
            if (sprite is not null)
                sprRef.Sprite = sprite;
        }

        return obj;
    }

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