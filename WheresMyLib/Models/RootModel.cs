using System.Xml.Serialization;

namespace WheresMyLib.Models;

public abstract class RootModel
{
    [XmlIgnore]
    public FileInfo FileInfo { get; set; }
}