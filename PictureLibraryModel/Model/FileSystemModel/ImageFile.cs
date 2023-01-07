using PictureLibraryModel.Model.FileSystemModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PictureLibraryModel.Model
{
    public enum ImageExtension : int
    {
        JPG,
        BMP,
        GIF,
        PNG,
        TIFF,
        NONE
    }

    public static class ImageExtensionHelper
    {
        public static ImageExtension GetExtension(string extension)
        {
            switch(extension)
            {
                case ".jpg": return ImageExtension.JPG;
                case ".jpeg": return ImageExtension.JPG;
                case ".jfif": return ImageExtension.JPG;
                case ".pjpeg": return ImageExtension.JPG;
                case ".pjp": return ImageExtension.JPG;
                case ".png": return ImageExtension.PNG;
                case ".bmp": return ImageExtension.BMP;
                case ".gif": return ImageExtension.GIF;
                case ".tiff": return ImageExtension.TIFF;
                case ".tif": return ImageExtension.TIFF;
            }
            return ImageExtension.NONE;
        }
    }

    [XmlInclude(typeof(LocalImageFile))]
    [XmlInclude(typeof(GoogleDriveImageFile))]
    public abstract class ImageFile : File, IEntity
    {
        public Guid Id { get; set; }
        public string LibraryFullName { get; set; }
        public List<Tag> Tags { get; set; }

        public ImageFile() : base()
        {
            Id = Guid.NewGuid();
            Tags = new List<Tag>();
        }

        public static bool IsFileAnImage(FileInfo fileInfo)
        {
            var extension = ImageExtensionHelper.GetExtension(fileInfo.Extension);

            if (extension == ImageExtension.NONE) return false;
            else return true;
        }
    }
}
