using System.Xml.Serialization;

namespace PictureLibrary.Tools.XamlSerializer
{
    public class XmlSerializerWrapped<TType> : IXmlSerializer<TType>
        where TType : class, new()
    {
        public TType? DeserializeFromString(string xml)
        {
            using StringReader sr = new(xml);
            XmlSerializer xmlSerializer = new(typeof(TType));

            return xmlSerializer.Deserialize(sr) as TType;
        }

        public string SerializeToString(TType obj)
        {
            using StringWriter sw = new();
            XmlSerializer xmlSerializer = new(typeof(TType));
            
            xmlSerializer.Serialize(sw, obj);

            return sw.ToString();
        }
    }
}
