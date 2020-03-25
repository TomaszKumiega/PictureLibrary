using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        private IFileSystemService FileSystemService { get; }

        public string CurrentDirectory { get; }
        public ObservableCollection<Drive> Drives { get; private set; }


        public FileSystemViewModel(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            Initialize();
        }

        private async Task Initialize()
        {
            Drives = await Task.Run(() => FileSystemService.GetDrives());
        }
    }
}
