using WheresMyLib.Core;

namespace WheresMyLib.Models;

public interface IGameFileLoader<T> where T : GameFile
{
    abstract void Save();
    abstract void Save(string directoryPath);

    static abstract T Load(string filePath, Game game);
    static abstract void Export(T gameFile, string directoryPath);
}