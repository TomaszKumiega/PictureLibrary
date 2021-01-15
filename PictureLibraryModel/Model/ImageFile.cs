using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureLibraryModel.Model
{
    public enum ImageExtensions
    {
        JPG,
        BMP,
        GIF,
        PNG,
        TIFF,
        NONE
    }

    public class ImageExtension
    {
        public static ImageExtensions GetExtension(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg2

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageExtensions.BMP;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageExtensions.GIF;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageExtensions.PNG;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageExtensions.TIFF;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageExtensions.TIFF;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageExtensions.JPG;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageExtensions.JPG;

            return ImageExtensions.NONE;
        }
    }

    public class ImageFile : IExplorableElement
    {
        public string Name { get; set; }
        public ImageExtensions Extension { get; set; }
        public string FullPath { get; set; }
        public string LibraryFullPath { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }
        public List<Tag> Tags { get; set; }
        public string IconSource { get; }
        public Origin Origin { get; set; }

        public ImageFile()
        {
            IconSource = FullPath;
        }

        public static bool IsFileAnImage(string path)
        {
            var fileBytes = File.ReadAllBytes(path);
            return ImageExtension.GetExtension(fileBytes) != ImageExtensions.NONE;
        }
    }
}
