using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using WheresMyLib.Data.Types;

namespace WheresMyLib.Data.Textures;

/// <summary>
/// Specifies the rectangle of a texture that corresponds to an image. Example:
/// <br/>
/// <code><![CDATA[<Image name="Balloon_Texture.png" offset="0 0" size="64 64" rect="1 196 64 64"/>]]></code>
/// </summary>
public class ImageRect
{
    public string Name { get; set; }
    public Pos Offset { get; set; }
    public Pos Size { get; set; }
    public Rect Rect { get; set; }

    public ImageAtlas ParentAtlas { get; set; }

    public Image GetImage()
    {
        if (ParentAtlas is null)
            return new Image<Rgba32>(1, 1);

        Image croppedImage = ParentAtlas.Image.Clone(ctx => ctx.Crop((Rectangle)Rect));
        return croppedImage;
    }
}