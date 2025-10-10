using System.Globalization;

namespace WheresMyLib.Data.Types;

public class Rect
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public Rect(float x, float y, float width, float height)
        => (X, Y, Width, Height) = (x, y, width, height);

    /// <summary>
    /// Parses a Rect from a string (eg. "10 20 30 40")
    /// </summary>
    public static Rect FromString(string str)
    {
        string[] parts = str.Split(' ');
        if (parts.Length != 4)
            throw new InvalidOperationException($"Invalid Rect string: \"{str}\".");

        if (float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) &&
            float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) &&
            float.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float w) &&
            float.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float h))
        {
            return new Rect(x, y, w, h);
        }

        throw new InvalidOperationException($"Unable to parse string \"{str}\" to a Rect.");
    }

    public override string ToString() => $"{X} {Y} {Width} {Height}";

    public static Rect operator +(Rect a, Rect b)
        => new(a.X + b.X, a.Y + b.Y, a.Width + b.Width, a.Height + b.Height);

    public static Rect operator -(Rect a, Rect b)
        => new(a.X - b.X, a.Y - b.Y, a.Width - b.Width, a.Height - b.Height);

    public static Rect operator *(Rect a, Rect b)
        => new(a.X * b.X, a.Y * b.Y, a.Width * b.Width, a.Height * b.Height);

    public static Rect operator /(Rect a, Rect b)
    {
        if (b.X == 0 || b.Y == 0 || b.Width == 0 || b.Height == 0)
            throw new DivideByZeroException($"Cannot divide by a Rect with X, Y, Width, or Height equal to 0.");
        return new(a.X / b.X, a.Y / b.Y, a.Width / b.Width, a.Height / b.Height);
    }

    public static bool operator ==(Rect a, Rect b)
        => a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;

    public static bool operator !=(Rect a, Rect b) => !(a == b);

    public override bool Equals(object obj) => obj is Rect r && r == this;
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    public static explicit operator System.Drawing.Rectangle(Rect rect)
        => new System.Drawing.Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

    public static explicit operator SixLabors.ImageSharp.Rectangle(Rect rect)
        => new SixLabors.ImageSharp.Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
}
