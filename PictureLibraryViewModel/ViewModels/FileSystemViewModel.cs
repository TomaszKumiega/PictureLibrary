using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        private IFileSystemService FileSystemService { get; }
        private string _currentDirectoryPath;


        public Directory CurrentDirectory { get; set; }
        public ObservableCollection<Drive> Drives { get; private set; }


        public FileSystemViewModel(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            Initialize();
        }

        public string CurrentDirectoryPath
        {
            get { return _currentDirectoryPath; }
            set
            {
                _currentDirectoryPath = value;
                UpdateCurrentDirectory();
            }
        }

        private async Task Initialize()
        {
            Drives = await Task.Run(() => FileSystemService.GetDrives());
        }

        private void UpdateCurrentDirectory()
        {
            var directories = FileSystemService.GetAllDirectories(CurrentDirectoryPath, System.IO.SearchOption.TopDirectoryOnly);
            var imageFiles = FileSystemService.GetAllImageFiles(CurrentDirectoryPath);
            ObservableCollection<object> children = new ObservableCollection<object>();

            foreach (var t in directories) children.Add(t);
            foreach (var t in imageFiles) children.Add(t);

            CurrentDirectory = new Directory(CurrentDirectoryPath, children);
        }
    }
}
