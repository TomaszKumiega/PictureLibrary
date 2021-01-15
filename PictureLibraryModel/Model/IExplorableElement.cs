using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Model
{
    public enum Origin
    {
        Local,
        RemoteServer
    }

    public interface IExplorableElement
    {
        string Name { get; set; }
        Origin Origin { get; set; }
        string FullPath { get; set; }
        string IconSource { get; }
    }
}
