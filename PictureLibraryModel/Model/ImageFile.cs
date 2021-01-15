using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureLibraryModel.Model
{
    public enum ImageExtension
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
        public static ImageExtension GetExtension(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg2

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageExtension.BMP;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageExtension.GIF;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageExtension.PNG;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageExtension.TIFF;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageExtension.TIFF;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageExtension.JPG;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageExtension.JPG;

            return ImageExtension.NONE;
        }

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

    public class ImageFile : IExplorableElement
    {
        public string Name { get; set; }
        public ImageExtension Extension { get; set; }
        public string FullPath { get; set; }
        public string LibraryFullPath { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }
        public List<Tag> Tags { get; set; }
        public string IconSource { get; set; }
        public Origin Origin { get; set; }

        public ImageFile()
        {
            IconSource = FullPath;
        }

        public static bool IsFileAnImage(string path)
        {
            var fileBytes = File.ReadAllBytes(path);
            return ImageExtensionHelper.GetExtension(fileBytes) != ImageExtension.NONE;
        }
    }
}
