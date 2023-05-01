namespace PictureLibrary.Tools.XamlSerializer
{
    public interface IXmlSerializer<TType>
        where TType : class, new()
    {
        string SerializeToString(TType obj);
        TType? DeserializeFromString(string xml);
    }
}
