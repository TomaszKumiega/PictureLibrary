namespace PictureLibraryModel.Model.Builders
{
    public interface IGenericLibraryBuilder<out TBuilder> : ILibraryBuilder
    {
        TBuilder CreateLibrary();
        TBuilder WithName(string name);
        TBuilder WithDescription(string description);
    }
}
