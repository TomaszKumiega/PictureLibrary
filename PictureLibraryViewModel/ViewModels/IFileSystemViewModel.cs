using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModels
{
    public interface IFileSystemViewModel
    {
        /// <summary>
        /// Contains a path of directory object is currently operating on
        /// </summary>
        string CurrentDirectory { get; }
        /// <summary>
        /// Contains a list of directories names
        /// </summary>
        List<string> Directories { get; }
        /// <summary>
        /// Contains a list of ImageFiles from current directory
        /// </summary>
        List<ImageFile> ImageFiles { get; }
       
    }
}
