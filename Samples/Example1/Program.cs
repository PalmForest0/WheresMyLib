using SixLabors.ImageSharp;
using System.Diagnostics;
using WheresMyLib.Core;

Stopwatch timer = Stopwatch.StartNew();
Game game = new Game(@"C:\Water_ 1.18.9");

timer.Stop();
Print($"Successfully loaded game files in {timer.Elapsed.TotalSeconds:0.00} seconds.", ConsoleColor.DarkGreen);

var bomb = game.GetObject("bomb");
Image img = bomb.Sprites[0].Sprite.Animations[0].Frames[0].ImageRect.GetCroppedImage(game);
img.Save("bomb.png");

//if(bomb.Sprites[0].SpriteData is not null)
//    Print($"Bomb sprite has {bomb.Sprites[0].SpriteData.Animations.Count} animations.", ConsoleColor.Green);
//else
//    Print("Bomb sprite not found!", ConsoleColor.Red);

void PrintTextures(Game game)
{
    // Print all textures
    foreach (var texture in game.Textures)
    {
        Print($"Texture: {texture.TexturePath} ({texture.ImageRects.Count} images)", ConsoleColor.Cyan);
        foreach (var image in texture.ImageRects)
            Print($" - Image: {image.Name} Rect: {image.Rect}", ConsoleColor.DarkCyan);
    }
}

void Print(object content, ConsoleColor foregroundColor = ConsoleColor.White,  ConsoleColor backgroundColor = ConsoleColor.Black)
{
    Console.ForegroundColor = foregroundColor;
    Console.BackgroundColor = backgroundColor;

    Console.WriteLine(content);

    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Black;
}