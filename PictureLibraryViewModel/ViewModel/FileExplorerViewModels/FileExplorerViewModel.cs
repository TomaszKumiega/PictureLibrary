using NLog;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileExplorerViewModel : IFileExplorerViewModel
    {
        #region Private fields
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IDirectoryService _directoryService;
        #endregion

        #region Public properties
        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public IExplorerHistory ExplorerHistory { get; }

        private string _infoText;
        public string InfoText 
        {
            get => _infoText;
            set
            {
                _infoText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InfoText)));
            }
        }

        private bool _isProcessing;
        public bool IsProcessing 
        { 
            get => _isProcessing; 
            set
            {
                _isProcessing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsProcessing)));
            }
        }

        private IExplorableElement _currentlyOpenedElement;
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
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public FileExplorerViewModel(
            IDirectoryService directoryService, 
            IExplorerHistory explorerHistory)
        {
            _directoryService = directoryService;
            ExplorerHistory = explorerHistory;

            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();

            PropertyChanged += OnCurrentlyOpenedElementChanged;
            SelectedElements.CollectionChanged += OnSelectedElementsChanged;

            _currentlyOpenedElement = null;
        }

        #region Event handlers
        public async void OnCurrentlyOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == nameof(CurrentlyOpenedElement)) await LoadCurrentlyShownElementsAsync();
        }

        public void OnSelectedElementsChanged(object sender, EventArgs args)
        {
            InfoText = Strings.SelectedElements + ' ' + SelectedElements.Count;
        }
        #endregion

        #region Public mehtods
        public async Task LoadCurrentlyShownElementsAsync()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            InfoText = Strings.Loading;
            IsProcessing = true;

            IEnumerable<IExplorableElement> content = new List<IExplorableElement>();

            if (CurrentlyOpenedElement == null)
            {
                content = await Task.Run(() => _directoryService.GetRootDirectories());
            }
            else
            {
                content = await Task.Run(() => _directoryService.GetDirectoryContent(CurrentlyOpenedElement.Path));
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
        #endregion
    }
}
