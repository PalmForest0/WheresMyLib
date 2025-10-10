using SixLabors.ImageSharp;
using System.Xml.Linq;
using WheresMyLib.Core;
using WheresMyLib.Data.Types;
using WheresMyLib.Utility;

namespace WheresMyLib.Data.Textures;

/// <summary>
/// Provides a list of <see cref="ImageRect"/>s from a single <c>.imagelist</c> file. Example:
/// <br/>
/// <code><![CDATA[<ImageList imgSize="128 512" file="/Textures/Balloons.webp" textureBasePath="/Textures/">]]></code>
/// </summary>
public class ImageAtlas(string filePath, Game game) : GameFile(filePath, game), IGameFileLoader<ImageAtlas>
{
    public Pos ImageSize { get; set; }
    public float DrawScale { get; set; }
    public string ImagePath { get; set; }
    public Image Image { get; set; }

    /// <summary>
    /// Contains all the <c><![CDATA[<Image>]]></c> tags within this <c><![CDATA[<ImageList>]]></c> that define individual images. 
    /// </summary>
    public List<ImageRect> Rects { get; set; }

    /// <summary>
    /// Contains the root path of all texture images. Always equal to <c>"/Textures"</c>.
    /// </summary>
    public string TextureBasePath { get; } = "/Textures";

    public TextureQuality Quality { get; set; }

    public static ImageAtlas Load(string filePath, Game game)
    {
        XDocument xml = XDocument.Load(filePath);

        string imgPath = (string)xml.Root.Attribute("file");
        Pos imgSize = Pos.FromString((string)xml.Root.Attribute("imgSize"));
        float drawScale = float.Parse((string)xml.Root.Attribute("drawScale") ?? "1");

        // Attempt to load atlas image
        Image image = null;
        string imagePath = FileUtils.CombinePaths(game.AssetsPath, imgPath);

        if (File.Exists(imagePath))
        {
            using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            image = Image.Load(stream);
        }

        ImageAtlas atlas = new ImageAtlas(filePath, game)
        {
            ImagePath = imgPath,
            ImageSize = imgSize,
            DrawScale = drawScale,
            Image = image
        };

        // Determine texture quality based of filename
        if (atlas.FileName.EndsWith("TabHD"))
            atlas.Quality = TextureQuality.TabHD;
        else if (atlas.FileName.EndsWith("HD"))
            atlas.Quality = TextureQuality.HD;
        else atlas.Quality = TextureQuality.Normal;

        // Load each <Image> tag from ImageList
        List<ImageRect> rects = xml.Root.Elements("Image").Select(img => ParseRect(img, atlas)).ToList();

        atlas.Rects = rects;
        return atlas;
    }

    private static ImageRect ParseRect(XElement element, ImageAtlas atlas) => new ImageRect()
    {
        Name = (string)element.Attribute("name"),
        Offset = Pos.FromString((string)element.Attribute("offset")),
        Size = Pos.FromString((string)element.Attribute("size")),
        Rect = Rect.FromString((string)element.Attribute("rect")),
        ParentAtlas = atlas,
    };

    /// <summary>
    /// Saves this <see cref="ImageAtlas"/> as an <c>.imagelist</c> file and an image at <see cref="ImagePath"/> to the loaded game's <c>TexturesPath</c>.
    /// </summary>
    public void Save() => Export(this, Game.TexturesPath);

    /// <summary>
    /// Saves this <see cref="ImageAtlas"/> as an <c>.imagelist</c> file and an image at <see cref="ImagePath"/> to a different directoryPath.
    /// </summary>
    /// <param name="directoryPath">Custom directoryPath path to export level data and image to.</param>
    public void Save(string directoryPath) => Export(this, directoryPath);

    public static void Export(ImageAtlas atlas, string directoryPath)
    {
        XDocument xml = new XDocument(
            new XElement("ImageList",

                // Export all attributes, assuming only drawScale is optional
                new XAttribute("file", atlas.ImagePath),
                new XAttribute("textureBasePath", atlas.TextureBasePath),
                new XAttribute("imgSize", atlas.ImageSize.ToString()),
                atlas.DrawScale == 1f ? null : new XAttribute("drawScale", atlas.DrawScale.ToString()),

                // Export all <Image> tags with each attribute being optional
                atlas.Rects.Select(rect => new XElement("Image",
                    rect.Name is null ? null : new XAttribute("name", rect.Name),
                    rect.Offset is null ? null : new XAttribute("offset", rect.Offset.ToString()),
                    rect.Size is null ? null : new XAttribute("size", rect.Size.ToString()),
                    rect.Rect is null ? null : new XAttribute("rect", rect.Rect.ToString())
                ))
            )
        );

        Directory.CreateDirectory(directoryPath);
        string xmlPath = Path.Join(directoryPath, $"{atlas.FileName}.imagelist");
        XmlUtils.SaveXml(xml, xmlPath);

        // Save the image if it exists (should never be null)
        if (atlas.Image is not null)
        {
            string imagePath = FileUtils.CombinePaths(directoryPath, atlas.ImagePath.Split('/').Last());
            using FileStream stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write);
            atlas.Image.SaveAsPng(stream);
        }
    }
}
