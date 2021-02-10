using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _currentlyOpenedPath;
        private string _infoText;
        private bool _isProcessing;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public IExplorerHistory ExplorerHistory { get; }
        public IDirectoryService DirectoryService { get; }

        public string InfoText 
        {
            get => _infoText;
            set
            {
                _infoText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InfoText"));
            }
        }
        public bool IsProcessing 
        { 
            get => _isProcessing; 
            set
            {
                _isProcessing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsProcessing"));
            }
        }

        public string CurrentlyOpenedPath 
        {
            get => _currentlyOpenedPath;
            set
            {
                ExplorerHistory.BackStack.Push(_currentlyOpenedPath);
                ExplorerHistory.ForwardStack.Clear();

                _currentlyOpenedPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedPath"));
            }
        }

        public FileExplorerViewModel(IDirectoryService directoryService, IExplorerHistory explorerHistory)
        {
            DirectoryService = directoryService;
            ExplorerHistory = explorerHistory;

            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();

            PropertyChanged += OnCurrentlyOpenedPathChanged;
            SelectedElements.CollectionChanged += OnSelectedElementsChanged;

            _currentlyOpenedPath = "\\";

            Task.Run(() => LoadCurrentlyShownElements()).Wait();
        }

        public async void OnCurrentlyOpenedPathChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == "CurrentlyOpenedPath") await LoadCurrentlyShownElements();
        }

        public void OnSelectedElementsChanged(object sender, EventArgs args)
        {
            InfoText = Strings.SelectedElements + ' ' + SelectedElements.Count;
        }

        public async Task LoadCurrentlyShownElements()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            InfoText = Strings.Loading;
            IsProcessing = true;

            IEnumerable<IExplorableElement> content = new List<IExplorableElement>();

            try
            {
                if (CurrentlyOpenedPath == "\\")
                {
                    content = await Task.Run(() => DirectoryService.GetRootDirectories());
                }
                else
                {
                    content = await Task.Run(() => DirectoryService.GetDirectoryContent(CurrentlyOpenedPath));
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the contents of: " + CurrentlyOpenedPath + " directory.");
            }

            foreach (var t in content)
            {
                CurrentlyShownElements.Add(t);
            }

            InfoText = Strings.ElementsLoaded + ' ' + CurrentlyShownElements.Count.ToString();
            IsProcessing = false;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyShownElements"));
        }
        public void Back()
        {
            if (ExplorerHistory.BackStack.Count == 0) return;

            ExplorerHistory.ForwardStack.Push(_currentlyOpenedPath);
            _currentlyOpenedPath = ExplorerHistory.BackStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedPath"));
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentlyOpenedPath);
            _currentlyOpenedPath = ExplorerHistory.ForwardStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedPath"));
        }
    }
}
