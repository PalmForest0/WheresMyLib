using System.Xml.Serialization;

namespace WheresMyLib.Models;

[XmlRoot(ElementName = "Objects")]
public class Level
{
    [XmlElement(ElementName = "Object")]
    public List<Object> Objects { get; set; }
    [XmlElement(ElementName = "Room")]
    public Room Room { get; set; }
}

[XmlRoot(ElementName = "AbsoluteLocation")]
public class AbsoluteLocation
{
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "Property")]
public class Property
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; }
}

[XmlRoot(ElementName = "Object")]
public class Object
{
    [XmlElement(ElementName = "AbsoluteLocation")]
    public AbsoluteLocation AbsoluteLocation { get; set; }

    [XmlArray(ElementName = "Properties")]
    public List<Property> Properties { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "Room")]
public class Room
{
    [XmlElement(ElementName = "AbsoluteLocation")]
    public AbsoluteLocation AbsoluteLocation { get; set; }
}