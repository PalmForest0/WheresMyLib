using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Models.Textures;
using WheresMyLib.Utility;

namespace WheresMyLib.Models.Sprites;

[XmlRoot(ElementName = "Sprite")]
public class Sprite : RootModel
{
    [XmlElement(ElementName = "Animation")]
    public List<Animation> Animations { get; set; }

    public static Sprite Load(string filepath, Game game)
    {
        Sprite sprite = SerializerUtils.Deserialize<Sprite>(filepath, game);
        
        foreach (Animation animation in sprite.Animations)
        {
            FileInfo atlasFile = new FileInfo(Path.Join(game.Assets.FullName, animation.AtlasPath));
            TextureAtlas atlas = game.Textures.Find(a => a.FileInfo.FullName == atlasFile.FullName);

            // Load the texture atlas of each animation and
            // link the frames to the atlas images
            if (atlas is not null)
            {
                animation.Atlas = atlas;
                
                foreach (Frame frame in animation.Frames)
                    frame.ImageData = atlas.Images.Find(i => i.Name == frame.Name);
            }
        }
        
        return sprite;
    }
}