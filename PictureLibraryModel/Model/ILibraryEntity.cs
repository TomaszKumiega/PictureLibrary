using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public interface ILibraryEntity
    {
        string ImageSource { get; }
        string Name { get; }
    }
}
