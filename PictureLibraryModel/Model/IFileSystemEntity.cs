using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureLibraryModel.Model
{
    public interface IFileSystemEntity
    {
        Bitmap Icon { get; }
        string Name { get; set; }
        string FullPath { get; set; }
    }
}
