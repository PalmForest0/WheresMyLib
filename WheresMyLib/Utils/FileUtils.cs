namespace WheresMyLib.Utils;

public static class FileUtils
{
    public static string GetShortFileName(string filePath)
    {
        char delimiter = filePath.Contains('\\') ? '\\' : '/';
        return filePath.Split(delimiter).Last().Split('.').First();
    }
}