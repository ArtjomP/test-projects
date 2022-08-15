using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lib
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public sealed record FixtureGroup
    {
        //[Obfuscation(ApplyToMembers = true, Exclude = true)]
        [XmlArrayItem("n", IsNullable = false)]
        public Fader[] Faders
        {
            get;
            init;
        } = Array.Empty<Fader>();
    }
}
