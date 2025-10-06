namespace WheresMyLib.Utility;

/// <summary>
/// Defines a single column in a table.
/// </summary>
/// <typeparam name="T">The type of object each row in this column contains.</typeparam>
/// <param name="header">The string header of this column.</param>
/// <param name="selector">Selector that shows which value of the given object should be displayed in each row.</param>
/// <param name="color"></param>
public struct Column<T>(string header, Func<T, object> selector, ConsoleColor color = ConsoleColor.Gray)
{
    public string Header { get; set; } = header;
    public Func<T, object> Selector { get; set; } = selector;
    public ConsoleColor Color { get; set; } = color;
}

public static class ConsoleTable
{
    /// <summary>
    /// Prints a list of objects as a formatted table to the console.
    /// </summary>
    /// <typeparam name="T">The object displayed in each row of the table.</typeparam>
    /// <param name="items">A collection of items to display in the table.</param>
    /// <param name="columns">Column definitions for this table.</param>
    public static void PrintTable<T>(IEnumerable<T> items, params Column<T>[] columns)
    {
        var rows = items.ToList();

        // Compute column widths
        var widths = columns.Select(c =>
        {
            int headerWidth = c.Header.Length;
            int maxValueWidth = rows.Count == 0 ? 0 : rows.Max(r => (c.Selector(r)?.ToString() ?? "").Length);
            return Math.Max(headerWidth, maxValueWidth) + 2;
        }).ToList();

        // Print header
        for (int i = 0; i < columns.Length; i++)
        {
            Console.ForegroundColor = columns[i].Color;
            Console.Write(columns[i].Header.PadRight(widths[i]));
            Console.ResetColor();
        }
        Console.WriteLine();

        // Print separator line
        Console.WriteLine(new string('-', widths.Sum()));

        // Print each row
        foreach (var row in rows)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                var col = columns[i];
                string text = col.Selector(row)?.ToString() ?? "";
                Console.ForegroundColor = col.Color;
                Console.Write(text.PadRight(widths[i]));
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}