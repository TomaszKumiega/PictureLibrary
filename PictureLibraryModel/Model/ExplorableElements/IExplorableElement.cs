using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public interface IExplorableElement
    {
        string Name { get; set; }
        Guid Origin { get; set; }
        string FullName { get; set; }
        string IconSource { get; }
    }
}
