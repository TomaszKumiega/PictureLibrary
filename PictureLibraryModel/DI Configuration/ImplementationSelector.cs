using Autofac.Features.Indexed;

namespace PictureLibraryModel.DI_Configuration
{
    public class ImplementationSelector<TKey, TType> : IImplementationSelector<TKey, TType>
    {
        private IIndex<TKey, TType> Implementations { get; }

        public ImplementationSelector(IIndex<TKey, TType> implementations)
        {
            Implementations = implementations;
        }

        public TType Select(TKey key)
        {
            return Implementations.TryGetValue(key, out TType value)
                ? value
                : default;
        }
    }
}
