using SixLabors.ImageSharp;
using System.Diagnostics;
using WheresMyLib.Core;

Stopwatch timer = Stopwatch.StartNew();
Game game = Game.Load(@"C:\Water_ 1.18.9");

timer.Stop();
Print($"Successfully loaded game files in {timer.Elapsed.TotalSeconds:0.00} seconds.\n\n", ConsoleColor.DarkGreen);

//game.GetObject("star.hs").GetImage().Save("Test/coo.png");

//foreach (var atlas in game.Textures)
//    Print(atlas.Quality + "\t| " + atlas.FilePath, ConsoleColor.DarkGreen);

//Sprite spr = game.GetSprite("/spout.sprite");
//spr.Save("Test");

//int pingpong = 0;
//int loop = 0;

//foreach (Sprite sprite in game.Sprites)
//{
//    foreach (var anim in sprite.Animations)
//    {
//        Print(anim.Fps);
//        //Print($"{anim.PlaybackMode}:\t\t{sprite.FileName} - {anim.Name}", anim.PlaybackMode != "ONCE" ? ConsoleColor.DarkYellow : ConsoleColor.Gray);

//        if (anim.PlaybackMode == "LOOP")
//            loop++;
//        else if (anim.PlaybackMode == "PINGPONG")
//            pingpong++;
//    }
//}

//Print("\nTotal LOOP count: " + loop, ConsoleColor.DarkGreen);
//Print("\nTotal PINGPONG count: " + pingpong, ConsoleColor.DarkGreen);


//ConsoleTable.PrintTable(
//    game.Textures,
//    new Column<ImageAtlas>("File Name", a => a.FileName, ConsoleColor.Cyan),
//    new Column<ImageAtlas>("Image Path", a => a.ImagePath, ConsoleColor.DarkGray),
//    new Column<ImageAtlas>("Loaded", a => a.Image is not null, ConsoleColor.Green)
//);



//GameObject obj = game.GetObject("cup_5blocks");
//obj.Save("Test");

//Level level = game.GetLevel("bhvr_swirlie_bomb");
//level.Save(@"Test");

// Export Object textures
foreach (var obj in game.Objects)
{
    if (!Directory.Exists("Objects"))
        Directory.CreateDirectory("Objects");

    Image texture = obj.GetImage();
    string savePath = $"Objects/{obj.FileName}.png";

    if (texture is not null)
    {
        texture.Save(savePath);
        texture.Dispose();

        Print($"Successfully saved object texture of '{obj.FileName}'.", ConsoleColor.DarkGreen);
    }
    else
    {
        Print($"Failed to save object texture of '{obj.FileName}'.", ConsoleColor.Red);
    }
}

//void PrintTextures(Game game)
//{
//    // Print all textures
//    foreach (var texture in game.TextureAtlases)
//    {
//        Print($"Texture: {texture.ImagePath} ({texture.Rects.Count} images)", ConsoleColor.Cyan);
//        foreach (var image in texture.Rects)
//            Print($" - Image: {image.FilePath} Rect: {image.Rect}", ConsoleColor.DarkCyan);
//    }
//}

void Print(object content, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
{
    Console.ForegroundColor = foregroundColor;
    Console.BackgroundColor = backgroundColor;

    Console.WriteLine(content);
    Console.ResetColor();
}