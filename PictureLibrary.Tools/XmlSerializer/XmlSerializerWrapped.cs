using System.Xml.Serialization;

namespace PictureLibrary.Tools.XamlSerializer
{
    public class XmlSerializerWrapped : IXmlSerializer
    {
        #region Public methods
        public TType? DeserializeFromString<TType>(string xml)
            where TType : class, new()
        {
            using StringReader sr = new(xml);
            XmlSerializer xmlSerializer = new(typeof(TType));

            return xmlSerializer.Deserialize(sr) as TType;
        }

        public string SerializeToString<TType>(TType obj)
            where TType : class, new()
        {
            using StringWriter sw = new();
            XmlSerializer xmlSerializer = new(typeof(TType));
            
            xmlSerializer.Serialize(sw, obj);

            return sw.ToString();
        }
        #endregion
    }
}
