using WheresMyLib.Core;
using WheresMyLib.Models;

Game game = new Game(@"C:\Water_ 1.18.9");

Level level = game.LoadLevel(@"C:\Water_ 1.18.9\assets\Levels\06_DUAL_TOOLS.xml");
Console.WriteLine(level.Objects[0].Name);