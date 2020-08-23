using PictureLibraryModel.Services;
using System.Collections.ObjectModel;

namespace PictureLibraryModel.Model
{
    public interface IFileSystemEntitiesFactory
    {
        Directory GetDirectory(ObservableCollection<object> children, string name);
        Directory GetDirectory(string fullPath, ObservableCollection<object> children);
        Directory GetDirectory(string fullPath, string name, IFileSystemService fileSystemService);
        Drive GetDrive(string name, IFileSystemService fileSystemService);
        ImageFile GetImageFile();
        ImageFile GetImageFile(string path);
    }
}