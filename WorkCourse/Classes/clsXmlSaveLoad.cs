using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WorkCourse
{
    class clsXmlSaveLoad
    {
        private clsXmlSaveLoad() { }

        public static string SerializeXmlToString<T>(T value)
        {
            XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlSerializer serializer = new XmlSerializer(value.GetType());
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter stream = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, value, emptyNamespaces);
                return stream.ToString();
            }
        }

        public static T DeserializeXmlFromString<T>(string XmlString) where T : new()
        {
            object value = new T();
            XmlSerializer deserializer = new XmlSerializer(value.GetType());
            using (StringReader stream = new StringReader(XmlString))
            {
                value = (T)deserializer.Deserialize(stream);
                return (T)value;
            }
                
        }

        public static T DeserializeXmlFromFile<T>(string fileName) where T : new()
        {
            object value = new T();
            XmlSerializer deserializer = new XmlSerializer(value.GetType());
            FileStream fs = new FileStream(fileName, FileMode.Open);
            value = (T)deserializer.Deserialize(fs);
            fs.Close();
            return (T)value;
        }
    }
}
