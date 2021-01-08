using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public class ImageFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public string LibrarySource { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }
        public List<Tag> Tags { get; set; }

        public ImageFile()
        {

        }

        public ImageFile(string name, string extension, string source, string librarysource, DateTime creationTime, DateTime lastAccessTime, DateTime lastWriteTime, long size, List<Tag> tags)
        {
            Name = name;
            Extension = extension;
            FullPath = source;
            LibrarySource = librarysource;
            CreationTime = creationTime;
            LastAccessTime = lastAccessTime;
            LastWriteTime = lastWriteTime;
            Size = size;
            Tags = tags;
        }
    }
}
