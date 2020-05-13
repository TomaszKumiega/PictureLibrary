using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
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

        public async Task Initialize()
        {
            Drives = await Task.Run(() => FileSystemService.GetDrives());
            _currentDirectoryPath = Drives[0].Name;
            await Task.Run(UpdateCurrentDirectoryContent);
            
        }

        private void UpdateCurrentDirectoryContent()
        {

            if (!System.IO.Directory.Exists(CurrentDirectoryPath) && CurrentDirectoryPath != Drives[0].Name)
            {
                _currentDirectoryPath = System.IO.Directory.GetParent(CurrentDirectoryPath).FullName;
                return;
            }
            

            CurrentDirectoryContent.Clear();

            if (CurrentDirectoryPath != Drives[0].Name)
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
                    foreach (var t in Drives[0].Children)
                    {
                        CurrentDirectoryContent.Add(t as IFileSystemEntity);
                    }
                }
            }
        }
    }
}
