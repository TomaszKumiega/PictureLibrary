namespace PictureLibraryModel.DI_Configuration
{
    public interface IImplementationSelector<in TKey, out TType>
    {
        TType Select(TKey key);
    }
}
