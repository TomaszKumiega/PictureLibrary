using System;
using System.Collections.Generic;
using System.IO;

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

        public void AddImage(List<string> albumNames, string path)
        {
            if(path==null) throw new ArgumentNullException();

            var extension = Path.GetExtension(path);
            var newPath = System.IO.Directory.GetParent(FullPath).FullName + '\\' + Guid.NewGuid() + extension;

            System.IO.File.Copy(path, newPath);

            var imageFile = new ImageFile(newPath);

            foreach (var t in albumNames)
            {
                foreach (var album in Albums)
                {
                    if (t == album.Name)
                    {
                        album.Images.Add(imageFile);
                        break;
                    }
                }
            }
        }
    }
}
