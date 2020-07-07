using System.Collections.Generic;

namespace PictureLibraryModel.Model
{
    public class Library : ILibraryEntity
    {
        public string FullPath { get; }
        public string Name { get; }
        public List<Album> Albums { get; }

        //TODO: Add image source
        public string ImageSource => throw new System.NotImplementedException();

        public Library(string fullPath, string name)
        {
            FullPath = fullPath;
            Name = name;
            Albums = new List<Album>();
        }

        public Library(string fullPath, string name, List<Album> albums)
        {
            FullPath = fullPath;
            Name = name;
            Albums = albums;
        }

    }
}
