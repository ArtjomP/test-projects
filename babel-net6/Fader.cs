using System.ComponentModel;
using System.Xml.Serialization;

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class Fader
{
    [XmlAttribute]
    public String MappingName { get; init; } = String.Empty;
}