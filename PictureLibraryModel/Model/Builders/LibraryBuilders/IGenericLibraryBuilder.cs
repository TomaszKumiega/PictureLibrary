namespace PictureLibraryModel.Model.Builders
{
    public interface IGenericLibraryBuilder<TBuilder> : ILibraryBuilder
    {
        TBuilder CreateLibrary();
        TBuilder WithName(string name);
        TBuilder WithDescription(string description);
    }
}
