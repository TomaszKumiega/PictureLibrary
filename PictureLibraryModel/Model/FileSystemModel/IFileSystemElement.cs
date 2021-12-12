namespace PictureLibraryModel.Model
{
    public interface IFileSystemElement : IExplorableElement
    {
        string Extension { get; set; }
    }
}
