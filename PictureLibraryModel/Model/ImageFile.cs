using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureLibraryModel.Model
{
    public abstract class ImageFile : IFileSystemEntity
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public string LibraryFullPath { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }
        public List<Tag> Tags { get; set; }

        public Bitmap Icon { get; set; }
        public Origin Origin { get; set; }

        ~ImageFile()
        {
            Icon?.Dispose();
        }
    }
}
