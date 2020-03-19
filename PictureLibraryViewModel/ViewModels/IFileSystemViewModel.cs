using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public interface IFileSystemViewModel
    {
        string CurrentDirectory { get; }
        List<string> Directories { get; }
        
        


    }
}
