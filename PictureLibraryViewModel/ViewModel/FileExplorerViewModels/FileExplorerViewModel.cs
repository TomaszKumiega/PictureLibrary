using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _currentDirectoryPath;
        private IDirectoryService _directoryService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public IExplorerHistory ExplorerHistory { get; }

        public string CurrentDirectoryPath 
        {
            get => _currentDirectoryPath;
            set
            {
                ExplorerHistory.BackStack.Push(_currentDirectoryPath);
                ExplorerHistory.ForwardStack.Clear();

                _currentDirectoryPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDirectoryPath"));
            }
        }

        public FileExplorerViewModel(IDirectoryService directoryService, IExplorerHistory explorerHistory)
        {
            _directoryService = directoryService;
            ExplorerHistory = explorerHistory;
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();
            _currentDirectoryPath = "\\";

            LoadCurrentDirectoryContent();
        }

        public void LoadCurrentDirectoryContent()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            IEnumerable<IExplorableElement> content = new List<IExplorableElement>();

            try
            {
                if (CurrentDirectoryPath == "\\")
                {
                    content = _directoryService.GetRootDirectories();
                }
                else
                {
                    content = _directoryService.GetDirectoryContent(CurrentDirectoryPath);
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the contents of: " + CurrentDirectoryPath + " directory.");
            }

            foreach (var t in content)
            {
                CurrentlyShownElements.Add(t);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyShownElements"));
        }
        public void Back()
        {
            if (ExplorerHistory.BackStack.Count < 2) return;

            ExplorerHistory.ForwardStack.Push(_currentDirectoryPath);
            _currentDirectoryPath = ExplorerHistory.BackStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDirectoryPath"));
            LoadCurrentDirectoryContent();
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentDirectoryPath);
            _currentDirectoryPath = ExplorerHistory.ForwardStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDirectoryPath"));
            LoadCurrentDirectoryContent();
        }
    }
}
