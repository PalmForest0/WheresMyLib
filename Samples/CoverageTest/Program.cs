using System.Diagnostics;
using WheresMyLib.Core;
using WheresMyLib.Models.Levels;
using WheresMyLib.Models.Objects;
using WheresMyLib.Models.Sprites;
using WheresMyLib.Models.Textures;

Stopwatch timer = Stopwatch.StartNew();
Game game = new Game(@"C:\Water_ 1.18.9");

int totalTests = 0;
int passedTests = 0;

if(DoListCheck(game.TextureAtlases, $"{nameof(ImageAtlas)} loading"))
{
    DoStringCheck(game.TextureAtlases[0].ImagePath, $"{nameof(ImageAtlas.ImagePath)}", 1);
    DoStringCheck(game.TextureAtlases[0].ImageSize, $"{nameof(ImageAtlas.ImageSize)}", 1);
    DoListCheck(game.TextureAtlases[0].Rects, $"{nameof(ImageAtlas.Rects)}", 1);
}


DoListCheck(game.Levels, $"{nameof(Level)} loading");
DoListCheck(game.Sprites, $"{nameof(Sprite)} loading");
DoListCheck(game.Objects, $"{nameof(GameObject)} loading");

EndTests(passedTests, totalTests, timer);

bool DoStringCheck(string value, string message, int level = 0)
{
    if (string.IsNullOrEmpty(value))
    {
        PrintFailure(message, level == 0 ? "> " : "  - ");
        return false;
    }
        
    PrintSuccess(message, level == 0 ? "> " : "  - ");
    return true;
}

bool DoListCheck(IEnumerable<object> list, string message, int level = 0)
{
    if (list is null || !list.Any())
    {
        PrintFailure(message, level == 0 ? "> " : "  - ");
        return false;
    }
        
    PrintSuccess(message, level == 0 ? "> " : "  - ");
    return true;
}

bool DoNullCheck(object value, string message, int level = 0)
{
    if(value is null)
    {
        PrintFailure(message, level == 0 ? "> " : "  - ");
        return false;
    }
        
    PrintSuccess(message, level == 0 ? "> " : "  - ");
    return true;
}

void PrintSuccess(string message, string prefix = "> ")
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"{prefix} {message} passed.");
    Console.ForegroundColor = ConsoleColor.White;

    totalTests++;
    passedTests++;
}

void PrintFailure(string message, string prefix = "> ")
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"{prefix} {message} failed.");
    Console.ForegroundColor = ConsoleColor.White;

    totalTests++;
}

void EndTests(int passed, int total, Stopwatch timer)
{
    if (passed <= 0)
        Console.ForegroundColor = ConsoleColor.Red;
    else if (passed < total)
        Console.ForegroundColor = ConsoleColor.DarkYellow;
    else Console.ForegroundColor = ConsoleColor.DarkGreen;

    timer.Stop();

    Console.WriteLine();
    Console.WriteLine($"Passed {passed} / {total} total tests in {timer.Elapsed.TotalSeconds:0.00} seconds.");
    Console.ForegroundColor = ConsoleColor.White;
}