using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

[Serializable]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public sealed record FixtureGroup
{

    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    [XmlArrayItem("n", IsNullable = false)]
    public Fader[] Faders
    {
        get;
        init;
    } = Array.Empty<Fader>();
}