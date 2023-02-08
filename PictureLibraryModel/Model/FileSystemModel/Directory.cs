using System.Collections.ObjectModel;

namespace PictureLibraryModel.Model
{
    public abstract class Directory
    {
        #region Public Properties
        public string Name { get; set; }
        public string Path { get; set; }
        public ObservableCollection<Directory> SubDirectories { get; protected set; }
        #endregion
    }
}
