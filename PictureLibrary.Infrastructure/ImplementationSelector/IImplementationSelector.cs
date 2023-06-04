namespace PictureLibrary.Infrastructure.ImplementationSelector
{
    public interface IImplementationSelector<TKey, TImplementation> where TImplementation : class
    {
        TImplementation Select(TKey key);
    }
}