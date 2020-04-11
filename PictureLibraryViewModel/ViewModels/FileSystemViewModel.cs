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
            CurrentDirectoryPath = "My Computer";
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
            await Task.Run(UpdateCurrentDirectoryContent);
        }

        private void UpdateCurrentDirectoryContent()
        {

            CurrentDirectoryContent.Clear();

            if (CurrentDirectoryPath != "My Computer")
            {
                var directories =
                    FileSystemService.GetAllDirectories(CurrentDirectoryPath, System.IO.SearchOption.TopDirectoryOnly);
                var imageFiles = FileSystemService.GetAllImageFiles(CurrentDirectoryPath);
                if(directories!=null) foreach (var t in directories) CurrentDirectoryContent.Add(t);
                if(imageFiles!=null) foreach (var t in imageFiles) CurrentDirectoryContent.Add(t);
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
