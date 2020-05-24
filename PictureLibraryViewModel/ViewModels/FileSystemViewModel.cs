using System.Collections.Generic;
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
        private string _currentDirectoryPath;
        private readonly List<string> _directoriesHistory;
        private readonly string _homeDirectory = "home/";

        public ObservableCollection<Drive> Drives { get; private set; }
        public ObservableCollection<IFileSystemEntity> CurrentDirectoryContent { get; }

        public FileSystemViewModel(IFileSystemService fileSystemService)
        {
            FileSystemService = fileSystemService;
            _directoriesHistory = new List<string>();
            CurrentDirectoryContent = new ObservableCollection<IFileSystemEntity>();
            Initialize();
        }

        public string CurrentDirectoryPath
        {
            get { return _currentDirectoryPath; }
            set
            {
                var index = _directoriesHistory.IndexOf(_currentDirectoryPath);

                if (index < _directoriesHistory.Count - 2 && index != -1 && value != _directoriesHistory[index + 1])
                {
                    _directoriesHistory.RemoveRange(index + 1, _directoriesHistory.Count - index - 2);
                }
                else
                { 
                    _directoriesHistory.Add(_currentDirectoryPath);
                }

                _currentDirectoryPath = value;
                UpdateCurrentDirectoryContent();
            }
        }

        public async Task Initialize()
        {
            Drives = await Task.Run(() => FileSystemService.GetDrives());
            _currentDirectoryPath = _homeDirectory;
            _directoriesHistory.Add(_homeDirectory);
            await Task.Run(UpdateCurrentDirectoryContent);
        }

        private void UpdateCurrentDirectoryContent()
        {
            if (!System.IO.Directory.Exists(CurrentDirectoryPath) && CurrentDirectoryPath != _homeDirectory)
            {
                if (_directoriesHistory.Count>1)
                {
                    var index = _directoriesHistory.IndexOf(_currentDirectoryPath);

                    if (index > 0)
                        _currentDirectoryPath = _directoriesHistory[index - 1];
                    else throw new DirectoryNotFoundException("There is no directory to display.");

                    return;
                }
                
                throw new DirectoryNotFoundException("Directory " + CurrentDirectoryPath + " not found.");
            }
            
            CurrentDirectoryContent.Clear();

            if (CurrentDirectoryPath != _homeDirectory)
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
