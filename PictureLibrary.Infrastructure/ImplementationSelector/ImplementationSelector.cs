using Autofac.Features.Indexed;

namespace PictureLibrary.Infrastructure.ImplementationSelector
{
    public class ImplementationSelector<TKey, TImplementation> : IImplementationSelector<TKey, TImplementation> 
        where TImplementation : class
    {
        private readonly IIndex<TKey, TImplementation> _index;

        public ImplementationSelector(IIndex<TKey, TImplementation> index)
        {
            _index = index;
        }

        public TImplementation Select(TKey key)
        {
            return _index.TryGetValue(key, out var implementation)
                ? implementation
                : throw new ArgumentException($"Key {key} is not registered and implementation could not be found.");
        }
    }
}
