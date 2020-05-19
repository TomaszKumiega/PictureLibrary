using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Castle.Core.Internal;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        private IFileSystemService FileSystemService { get; }
        private string _previousDirectoryPath;
        private string _currentDirectoryPath;
        private readonly string homeDirectory = "home/";

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
                _previousDirectoryPath = _currentDirectoryPath;
                _currentDirectoryPath = value;
                UpdateCurrentDirectoryContent();
            }
        }

        public async Task Initialize()
        {
            Drives = await Task.Run(() => FileSystemService.GetDrives());
            _currentDirectoryPath = homeDirectory;
            await Task.Run(UpdateCurrentDirectoryContent);
        }

        private void UpdateCurrentDirectoryContent()
        {
            if (!System.IO.Directory.Exists(CurrentDirectoryPath) && CurrentDirectoryPath != homeDirectory)
            {
                if (!_previousDirectoryPath.IsNullOrEmpty())
                {
                    //TODO: LIFO QUEUE FOR DIRECTORIES
                    _currentDirectoryPath = _previousDirectoryPath;
                    return;
                }
                
                throw new DirectoryNotFoundException("Directory " + CurrentDirectoryPath + " not found.");
            }
            
            CurrentDirectoryContent.Clear();

            if (CurrentDirectoryPath != homeDirectory)
            {
                var directories =
                    FileSystemService.GetAllDirectories(CurrentDirectoryPath, System.IO.SearchOption.TopDirectoryOnly);
                var imageFiles = FileSystemService.GetAllImageFiles(CurrentDirectoryPath);
                if(directories!=null) foreach (var t in directories) CurrentDirectoryContent.Add(t);
                if(imageFiles!=null) foreach (var t in imageFiles) CurrentDirectoryContent.Add(t);
            }
            else
            {
                if (Drives != null)
                {
                    foreach (var t in Drives)
                    {
                        CurrentDirectoryContent.Add(t);
                    }
                }
            }
        }
    }
}
