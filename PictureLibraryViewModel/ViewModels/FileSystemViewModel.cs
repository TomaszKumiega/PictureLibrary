using System.Collections.Generic;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Castle.Core.Internal;
using System;

namespace PictureLibraryViewModel.ViewModels
{
    public class FileSystemViewModel : IFileSystemViewModel
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

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
                _directoriesHistory.Add(_currentDirectoryPath);
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
                try
                {
                    var directories =
                        FileSystemService.GetAllDirectories(CurrentDirectoryPath, System.IO.SearchOption.TopDirectoryOnly);
                    var imageFiles = FileSystemService.GetAllImageFiles(CurrentDirectoryPath);
                    if (directories != null) foreach (var t in directories) CurrentDirectoryContent.Add(t);
                    if (imageFiles != null) foreach (var t in imageFiles) CurrentDirectoryContent.Add(t);
                }
                catch(Exception e)
                {
                    _logger.Debug(e.Message, e);
                }
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

        public void NextDirectory()
        {
            var currentDirIndex = _directoriesHistory.IndexOf(_currentDirectoryPath);

            if (currentDirIndex < _directoriesHistory.Count - 1 && currentDirIndex!=-1)
            {
                _currentDirectoryPath = _directoriesHistory[currentDirIndex + 1];
                UpdateCurrentDirectoryContent();
            }
        }

        public void PreviousDirectory()
        {
            var currentDirIndex = _directoriesHistory.IndexOf(_currentDirectoryPath);

            if (currentDirIndex > 0)
            {
                _currentDirectoryPath = _directoriesHistory[currentDirIndex - 1];
                UpdateCurrentDirectoryContent();
            }
        }
    }
}
