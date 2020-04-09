using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureLibraryModel.Model
{
    public interface IFileSystemEntity
    {
        Icon Icon { get; }
        string Name { get; }
        string FullPath { get; }
    }
}
