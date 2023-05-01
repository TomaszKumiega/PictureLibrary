﻿using PictureLibraryModel.Model;

namespace PictureLibrary.Tools.XamlEditor
{
    public interface ILibraryXmlService
    {
        string UpdateLibraryNode<TLibrary>(string xml, TLibrary library) 
            where TLibrary : Library;
        string UpdateImageFileNode<TImageFile>(string xml, TImageFile imageFile)
            where TImageFile : ImageFile;
        string AddImageFileNode<TImageFile>(string xml, TImageFile imageFile)
            where TImageFile : ImageFile;
        string RemoveImageFileNode<TImageFile>(string xml, TImageFile imageFile)
            where TImageFile : ImageFile;
        string AddTagNode(string xml, Tag tag);
        string UpdateTagNode(string xml, Tag tag);
        string RemoveTagNode(string xml, Tag tag);

        IEnumerable<TImageFile> GetImageFiles<TImageFile>(string xml);
        IEnumerable<Tag> GetTags(string xml);
    }
}
