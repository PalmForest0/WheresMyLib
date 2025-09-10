using System.Xml.Serialization;
using WheresMyLib.Models;
using WheresMyLib.Utils;

namespace WheresMyLib.Serialization;

public static class ObjectSerializer
{
    public static InteractiveObject Deserialize(string objectFilePath)
    {
        string xml = File.ReadAllText(objectFilePath);
        var serializer = new XmlSerializer(typeof(InteractiveObject));
        using var stringReader = new StringReader(xml);

        if (serializer.Deserialize(stringReader) is InteractiveObject obj)
        {
            obj.Name = FileUtils.GetShortFileName(objectFilePath);
            obj.Path = objectFilePath;

            return obj;
        }

        throw new InvalidOperationException($"Failed to deserialize {nameof(InteractiveObject)} from file: {objectFilePath}");
    }
}