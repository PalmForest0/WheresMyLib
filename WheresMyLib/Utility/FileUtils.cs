using WheresMyLib.Exceptions;

namespace WheresMyLib.Utility;

public static class FileUtils
{
    public static IEnumerable<FileInfo> GetFiles(string directoryPath, Func<FileInfo, bool> filter)
        => new DirectoryInfo(directoryPath).EnumerateFiles().Where(filter);

    public static void ValidateDirectory(string path)
    {
        if (!Directory.Exists(path))
            throw new InvalidGameFilesException(path);
    }

    public static bool MatchPath(string fullPath, string filter)
    {
        string normPath = Path.GetFullPath(fullPath).Replace('/', '\\');
        string normFilter = filter.Replace('/', '\\');
        return normPath.IndexOf(normFilter, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static string SanitiseFileName(string input, string replacement = "_")
    {
        if (string.IsNullOrWhiteSpace(input))
            return "untitled";

        // Remove invalid characters
        char[] invalidChars = Path.GetInvalidFileNameChars();
        string result = string.Join(replacement, input.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

        // Trim spaces and dots from ends
        result = result.Trim().TrimEnd('.');

        // Handle reserved Windows filenames
        string[] reserved =
        [
            "CON", "PRN", "AUX", "NUL",
            "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
            "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
        ];

        if (reserved.Contains(result, StringComparer.OrdinalIgnoreCase))
            result = $"_{result}_";

        // If empty at the end, then we are sad :c
        return string.IsNullOrWhiteSpace(result) ? "untitled" : result;
    }

    /// <summary>
    /// Combines a base absolute path with another path that may start with '/' or '\'.
    /// </summary>
    public static string CombinePaths(string basePath, string pathToAdd)
    {
        if (string.IsNullOrWhiteSpace(basePath))
            throw new ArgumentException("Base path cannot be null or empty.", nameof(basePath));

        if (string.IsNullOrWhiteSpace(pathToAdd))
            return basePath;

        // Make path to add relative
        string relative = pathToAdd.TrimStart('/', '\\');
        string combined = Path.Combine(basePath, relative);

        return Path.GetFullPath(combined);
    }
}