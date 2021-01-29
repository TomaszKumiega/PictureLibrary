using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public abstract class File : IExplorableElement
    {
        public string Name { get; set; }
        public Origin Origin { get; set; }
        public string FullPath { get; set; }
        public string IconSource { get; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }
        
        public File()
        {
            IconSource = FullPath;
        }
    }
}
