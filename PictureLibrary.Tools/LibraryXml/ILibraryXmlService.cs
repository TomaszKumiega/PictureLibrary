using PictureLibraryModel.Model;

namespace PictureLibrary.Tools.LibraryXml
{
    public interface ILibraryXmlService
    {
        string UpdateLibraryNode<TLibrary>(string xml, TLibrary library) 
            where TLibrary : Library, new();
        string UpdateImageFileNode<TImageFile>(string xml, TImageFile imageFile)
            where TImageFile : ImageFile, new();
        string AddImageFileNode<TImageFile>(string xml, TImageFile imageFile)
            where TImageFile : ImageFile, new();
        string RemoveImageFileNode<TImageFile>(string xml, TImageFile imageFile)
            where TImageFile : ImageFile, new();
        string AddTagNode(string xml, Tag tag);
        string UpdateTagNode(string xml, Tag tag);
        string RemoveTagNode(string xml, Tag tag);

        IEnumerable<TImageFile> GetImageFiles<TImageFile>(string xml)
            where TImageFile : ImageFile, new();
        IEnumerable<Tag> GetTags(string xml);
    }
}
