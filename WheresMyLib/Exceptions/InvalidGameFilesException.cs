namespace WheresMyLib.Exceptions;

public class InvalidGameFilesException : Exception
{
    public InvalidGameFilesException() { }
    public InvalidGameFilesException(string missingPath) : base(GetMessage(missingPath)) { }
    public InvalidGameFilesException(string missingPath, Exception inner) : base(GetMessage(missingPath), inner) { }

    private static string GetMessage(string missingPath)
        => $"Could not locate '{missingPath}' within the specified game directory. Check that your game files are intact and in the right location.";
}