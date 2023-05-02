namespace PictureLibrary.Tools.XamlSerializer
{
    public interface IXmlSerializer
    {
        string SerializeToString<TType>(TType obj) where TType : class, new ();
        TType? DeserializeFromString<TType>(string xml) where TType : class, new();
    }
}
