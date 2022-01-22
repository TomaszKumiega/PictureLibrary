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
        private string _infoText;
        private bool _isProcessing;
        private IExplorableElement _currentlyOpenedElement;

        private IDirectoryService DirectoryService{get;}
  
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public IExplorerHistory ExplorerHistory { get; }

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

        public IExplorableElement CurrentlyOpenedElement 
        { 
            get => _currentlyOpenedElement; 
            set
            {
                ExplorerHistory.BackStack.Push(_currentlyOpenedElement);
                ExplorerHistory.ForwardStack.Clear();

                _currentlyOpenedElement = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentlyOpenedElement)));
            }
        }

        public FileExplorerViewModel(IDirectoryService directoryService, IExplorerHistory explorerHistory)
        {
            DirectoryService = directoryService;
            ExplorerHistory = explorerHistory;

            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();

            PropertyChanged += OnCurrentlyOpenedElementChanged;
            SelectedElements.CollectionChanged += OnSelectedElementsChanged;

            _currentlyOpenedElement = null;
        }

        public async void OnCurrentlyOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == nameof(CurrentlyOpenedElement)) await LoadCurrentlyShownElementsAsync();
        }

        public void OnSelectedElementsChanged(object sender, EventArgs args)
        {
            InfoText = Strings.SelectedElements + ' ' + SelectedElements.Count;
        }

        public async Task LoadCurrentlyShownElementsAsync()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            InfoText = Strings.Loading;
            IsProcessing = true;

            IEnumerable<IExplorableElement> content = new List<IExplorableElement>();

            try
            {
                if (CurrentlyOpenedElement == null)
                {
                    content = await Task.Run(() => DirectoryService.GetRootDirectories());
                }
                else
                {
                    content = await Task.Run(() => DirectoryService.GetDirectoryContent(CurrentlyOpenedElement.Path));
                }
            }
            catch(Exception e)
            {
                _logger.Error(e, e.Message);
                throw new Exception("Application failed loading the contents of: " + CurrentlyOpenedElement.Path + " directory.");
            }

            foreach (var t in content)
            {
                CurrentlyShownElements.Add(t);
                t.LoadIcon();
            }

            InfoText = Strings.ElementsLoaded + ' ' + CurrentlyShownElements.Count.ToString();
            IsProcessing = false;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentlyShownElements)));
        }
        public void Back()
        {
            if (ExplorerHistory.BackStack.Count == 0) return;

            ExplorerHistory.ForwardStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.BackStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentlyOpenedElement)));
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.ForwardStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentlyOpenedElement)));
        }
    }
}
