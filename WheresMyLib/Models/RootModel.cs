using System.Xml.Serialization;
using WheresMyLib.Core;

namespace WheresMyLib.Models;

public abstract class RootModel
{
    [XmlIgnore]
    public FileInfo FileInfo { get; set; }

    [XmlIgnore]
    public Game Game { get; set; }
}