namespace PictureLibraryModel.DI_Configuration
{
    public interface IImplementationSelector<TKey, TType>
    {
        TType Select(TKey key);
    }
}
