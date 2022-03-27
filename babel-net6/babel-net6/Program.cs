// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Xml;
using System.Xml.Serialization;

var faders = new Fader[]
{
    new Fader { MappingName = "Hello"},
    new Fader { MappingName = "World"},
};

var fixtures = new FixtureGroup()
{
    Faders = faders
};

Serialize(fixtures, "fixtures.xml");

[global::System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute("Calls System.Xml.Serialization.XmlSerializer.Serialize(System.Xml.XmlWriter, object?)")]
void Serialize<T>(T obj, String path)
{
    var serializer = new XmlSerializer(typeof(T));

    using var sww = new Utf8StringWriter();
    var xmlSettings = new XmlWriterSettings
    {
        Indent = true,
        // NewLineOnAttributes = true,
        Encoding = Encoding.UTF8
    };
    using var writer = XmlWriter.Create(sww, xmlSettings);

    serializer.Serialize(writer, obj);
    var xml = sww.ToString();
    File.WriteAllText(path, xml);
}