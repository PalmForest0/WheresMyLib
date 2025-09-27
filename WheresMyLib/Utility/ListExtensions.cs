namespace WheresMyLib.Utility;

public static class ListExtensions
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> list)
    {
        return list == null || list.Count == 0;
    }
}