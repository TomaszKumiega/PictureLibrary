using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PictureLibraryModel.Model
{
    public class ImageFile : IFileSystemEntity
    {
        public FileInfo FileInfo { get; set; }
        public string Name { get; }
        public string FullPath { get; set; }
        public string ImageSource { get; set; }

        public ImageFile()
        {

        }

        public ImageFile(string path)
        {
            if (File.Exists(path) && (IsFileAnImage(path)==true))
            {
                FullPath = path;
                FileInfo = new FileInfo(path);
                Name = FileInfo.Name;
                ImageSource = path;
            }
            else throw new Exception("File not found");
        }

        public static bool IsFileAnImage(string path)
        {
            string[] supportedExtensions = { ".jpg", ".jpeg", ".jpe", ".png", ".jfif", ".bmp", ".tif", ".tiff", ".gif" };
            bool pathEndsWithSupportedExtension = false;
            
            foreach(string t in supportedExtensions)
            {
                if (path.EndsWith(t)) pathEndsWithSupportedExtension = true;
            }

            return pathEndsWithSupportedExtension;
        }

        public Image GetImage()
        {
            return Image.FromFile(FullPath);
        }
    }
}
