using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WheresMyLib.Utility;

public static class SerializerUtils
{
    private static Dictionary<Type, XmlSerializer> Serializers = new Dictionary<Type, XmlSerializer>();

    public static T Deserialize<T>(string xml)
    {
        var type = typeof(T);
        if (!Serializers.ContainsKey(type))
            Serializers[type] = new XmlSerializer(type);

        using var stringReader = new StringReader(xml);
        if (Serializers[type].Deserialize(stringReader) is not T obj)
            throw new InvalidOperationException($"Failed to deserialize {type.Name} from XML.");

        return obj;
    }
}
