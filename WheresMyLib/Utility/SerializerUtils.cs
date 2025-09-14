using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WheresMyLib.Core;
using WheresMyLib.Exceptions;
using WheresMyLib.Models;

namespace WheresMyLib.Utility;

public static class SerializerUtils
{
    private static Dictionary<Type, XmlSerializer> Serializers = new Dictionary<Type, XmlSerializer>();

    public static T Deserialize<T>(string filepath, Game game) where T : RootModel
    {
        if (!File.Exists(filepath))
            throw new InvalidGameFilesException(filepath);
        
        var type = typeof(T);
        if (!Serializers.ContainsKey(type))
            Serializers[type] = new XmlSerializer(type);

        using var stringReader = new StringReader(File.ReadAllText(filepath));
        if (Serializers[type].Deserialize(stringReader) is not T obj)
            throw new InvalidOperationException($"Failed to deserialize {type.Name} from XML.");

        obj.FileInfo = new FileInfo(filepath);
        return obj;
    }
}
