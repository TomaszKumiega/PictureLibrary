using System;
using System.Collections.Generic;
using System.IO;
using PictureLibraryModel.Services;

namespace PictureLibraryModel.Model
{
    public class Library : ILibraryEntity
    {
        private readonly IFileSystemService _fileSystemService;
        public string FullPath { get; }
        public string Name { get; }
        public List<Album> Albums { get; }

        //TODO: Add image source
        public string ImageSource => throw new System.NotImplementedException();

        public Library(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
            Albums = new List<Album>();
        }

        public Library(string fullPath, string name, IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
            FullPath = fullPath;
            Name = name;
            Albums = new List<Album>();
        }

        public Library(string fullPath, string name, List<Album> albums, IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
            FullPath = fullPath;
            Name = name;
            Albums = albums;
        }

        public void AddImage(List<string> albumNames, string path)
        {
            if(path==null) throw new ArgumentNullException();

            var extension = _fileSystemService.GetExtension(path);
            var newPath = _fileSystemService.GetParent(FullPath).FullName + '\\' + Guid.NewGuid() + extension;

            _fileSystemService.CopyFile(path, newPath, false);

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

        public void AddAlbum(string albumName)
        {
            if(albumName==null) throw new ArgumentNullException();

            var album = new Album(albumName);
            Albums.Add(album);
        }
    }
}
