using System;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using lib;
using System.IO;

namespace babel_net5
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
        }

        private static void Serialize<T>(T obj, String path)
        {
            var serializer = new XmlSerializer(typeof(T));

            using var sww = new Utf8StringWriter();
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };
            using var writer = XmlWriter.Create(sww, xmlSettings);

            serializer.Serialize(writer, obj);
            var xml = sww.ToString();
            File.WriteAllText(path, xml);
        }
    }
}