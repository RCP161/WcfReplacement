using Catel.Runtime.Serialization.Xml;
using Prototype.Testing.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Prototype.Testing.BL
{
    internal class TestObjSerialiser
    {
        internal string SerializeToXml(SerialisationTestObj serialisationTestObj)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SerialisationTestObj));

            using(StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, serialisationTestObj);
                return writer.ToString();
            }
        }

        internal SerialisationTestObj DeserializeToXml(string serialisedTestObj)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(SerialisationTestObj));

            using(StringReader reader = new StringReader(serialisedTestObj))
            {
                return (SerialisationTestObj)serializer.Deserialize(reader);
            }
        }
    }
}
