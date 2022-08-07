namespace PictureLibraryModel.Model.Builders
{
    public interface ILocalLibraryBuilder : IGenericLibraryBuilder<ILocalLibraryBuilder>
    {
        ILocalLibraryBuilder WithPath(string path);
    }
}
