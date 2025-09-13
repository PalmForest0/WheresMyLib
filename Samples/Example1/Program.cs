using System.Diagnostics;
using WheresMyLib.Core;
using WheresMyLib.Models;

Stopwatch timer = Stopwatch.StartNew();
Game game = new Game(@"C:\Water_ 1.18.9");

timer.Stop();
Print($"Successfully loaded game files in {timer.Elapsed.TotalSeconds:0.00} seconds.", ConsoleColor.DarkGreen);

// Print all textures
foreach(var texture in game.Textures)
{
    Print($"Texture: {texture.TexturePath} ({texture.Images.Count} images)", ConsoleColor.Cyan);
    foreach(var image in texture.Images)
        Print($" - Image: {image.Name} Rect: {image.Rect}", ConsoleColor.DarkCyan);
}

void Print(object content, ConsoleColor foregroundColor = ConsoleColor.White,  ConsoleColor backgroundColor = ConsoleColor.Black)
{
    Console.ForegroundColor = foregroundColor;
    Console.BackgroundColor = backgroundColor;

    Console.WriteLine(content);

    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Black;
}