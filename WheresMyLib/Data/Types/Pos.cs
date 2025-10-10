using System.Globalization;
using System.Xml.Linq;

namespace WheresMyLib.Data.Types;

public class Pos
{
    public float X { get; set; }
    public float Y { get; set; }

    public Pos(float x, float y) => (X, Y) = (x, y);

    /// <summary>
    /// Parses a point from a string (eg. "5.2 10")
    /// </summary>
    public static Pos FromString(string str)
    {
        if (str is null)
            return null;

        string[] parts = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            throw new InvalidOperationException($"Invalid point string: \"{str}\".");

        if (float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) &&
            float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y))
        {
            return new Pos(x, y);
        }

        throw new InvalidOperationException($"Unable to parse string \"{str}\" to a point.");
    }

    public static Pos FromAbsoluteLocation(XElement xmlElement)
    {
        if (xmlElement is null)
            return null;

        if (xmlElement.Name != "AbsoluteLocation" || !xmlElement.HasAttributes)
            throw new InvalidOperationException($"Invalid AbsolutePosition XML element: \"{xmlElement}\".");

        return FromString(xmlElement.Attribute("value").Value);
    }

    public override string ToString() => $"{X} {Y}";

    public static Pos operator +(Pos a, Pos b) => new(a.X + b.X, a.Y + b.Y);
    public static Pos operator -(Pos a, Pos b) => new(a.X - b.X, a.Y - b.Y);
    public static Pos operator *(Pos a, Pos b) => new(a.X * b.X, a.Y * b.Y);
    public static Pos operator /(Pos a, Pos b)
    {
        if (b.X == 0 || b.Y == 0)
            throw new DivideByZeroException($"Cannot divide by a Point with X or Y equal to 0.");
        return new(a.X / b.X, a.Y / b.Y);
    }

    public static bool operator ==(Pos a, Pos b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Pos a, Pos b) => !(a == b);

    public override bool Equals(object obj) => obj is Pos p && p == this;
    public override int GetHashCode() => HashCode.Combine(X, Y);
}
