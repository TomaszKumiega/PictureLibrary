using Autofac.Features.Indexed;

namespace PictureLibraryModel.DI_Configuration
{
    public class ImplementationSelector<TKey, TType> : IImplementationSelector<TKey, TType>
    {
        private readonly IIndex<TKey, TType> _implementations;

        public ImplementationSelector(IIndex<TKey, TType> implementations)
        {
            _implementations = implementations;
        }

        public TType Select(TKey key)
        {
            return _implementations.TryGetValue(key, out TType value)
                ? value
                : default;
        }
    }
}
