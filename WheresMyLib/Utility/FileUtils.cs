namespace WheresMyLib.Utility;

public static class FileUtils
{
    public static IEnumerable<FileInfo> GetFiles(string directoryPath, Func<FileInfo, bool> filter)
    {
        return new DirectoryInfo(directoryPath).EnumerateFiles().Where(filter);
    }
}