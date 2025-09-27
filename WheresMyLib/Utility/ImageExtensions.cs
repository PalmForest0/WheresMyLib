using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace WheresMyLib.Utility;

public static class ImageExtensions
{
    public static Image<Rgba32> CropToContent(this Image<Rgba32> image)
    {
        int minX = image.Width, minY = image.Height;
        int maxX = 0, maxY = 0;
        bool foundPixel = false;

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < image.Height; y++)
            {
                var row = accessor.GetRowSpan(y);
                for (int x = 0; x < image.Width; x++)
                {
                    if (row[x].A > 0) // Non-transparent
                    {
                        foundPixel = true;
                        if (x < minX) minX = x;
                        if (y < minY) minY = y;
                        if (x > maxX) maxX = x;
                        if (y > maxY) maxY = y;
                    }
                }
            }
        });

        // Do not crop if it's a fully transparent image
        if (!foundPixel)
            return image.Clone();

        var rect = new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
        return image.Clone((IImageProcessingContext x) => x.Crop(rect));
    }
}