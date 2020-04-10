using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        private IFileSystemService FileSystemService { get; }
        private string _currentDirectoryPath;

        public ObservableCollection<Drive> Drives { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryContent { get; }

        public FileSystemViewModel(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            CurrentDirectoryContent = new ObservableCollection<IFileSystemEntity>();
            Initialize();
        }

        public string CurrentDirectoryPath
        {
            get { return _currentDirectoryPath; }
            set
            {
                _currentDirectoryPath = value;
                UpdateCurrentDirectoryContent();
            }
        }

        private async Task Initialize()
        {
            Drives = await Task.Run(() => FileSystemService.GetDrives());
            CurrentDirectoryPath = "My Computer";
            UpdateCurrentDirectoryContent();
        }

        private void UpdateCurrentDirectoryContent()
        {

            CurrentDirectoryContent.Clear();

            if (CurrentDirectoryPath != "My Computer")
            {
                var directories =
                    FileSystemService.GetAllDirectories(CurrentDirectoryPath, System.IO.SearchOption.TopDirectoryOnly);
                var imageFiles = FileSystemService.GetAllImageFiles(CurrentDirectoryPath);

                foreach (var t in directories) CurrentDirectoryContent.Add(t);
                foreach (var t in imageFiles) CurrentDirectoryContent.Add(t);
            }
            else
            {
                foreach (var t in Drives[0].Children)
                {
                    CurrentDirectoryContent.Add(t as IFileSystemEntity);
                }
            }
        }
    }
}
