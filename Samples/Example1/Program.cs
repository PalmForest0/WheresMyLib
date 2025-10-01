using System.Diagnostics;
using WheresMyLib.Core;

Stopwatch timer = Stopwatch.StartNew();
Game game = new Game(@"C:\Water_ 1.18.9");

timer.Stop();
Print($"Successfully loaded game files in {timer.Elapsed.TotalSeconds:0.00} seconds0.\n\n", ConsoleColor.DarkGreen);

// Export Object textures
//foreach (var obj in game.Objects)
//{
//    if (!Directory.Exists("Objects"))
//        Directory.CreateDirectory("Objects");

//    Image texture = obj.GetCombinedTexture();
//    if (texture is not null)
//    {
//        texture.Save(Path.Combine("Objects", Path.ChangeExtension($"{obj.FileInfo.Name}", ".png")));
//        texture.Dispose();

//        Print($"Successfully saved object texture '{Path.ChangeExtension($"{obj.FileInfo.Name}", ".png")}'.", ConsoleColor.DarkGreen);
//    }
//    else
//    {
//        Print($"Failed to save object texture '{Path.ChangeExtension($"{obj.FileInfo.Name}", ".png")}'.", ConsoleColor.Red);
//    }
//}

void PrintTextures(Game game)
{
    // Print all textures
    foreach (var texture in game.TextureAtlases)
    {
        Print($"Texture: {texture.ImagePath} ({texture.Rects.Count} images)", ConsoleColor.Cyan);
        foreach (var image in texture.Rects)
            Print($" - Image: {image.Name} Rect: {image.Rect}", ConsoleColor.DarkCyan);
    }
}

void Print(object content, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
{
    Console.ForegroundColor = foregroundColor;
    Console.BackgroundColor = backgroundColor;

    Console.WriteLine(content);

    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Black;
}