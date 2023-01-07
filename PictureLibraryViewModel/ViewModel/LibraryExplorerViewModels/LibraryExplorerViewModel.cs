using PictureLibraryModel.DataProviders.Repositories;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerViewModel : ILibraryExplorerViewModel
    {
        #region Private fields
        private readonly ILibraryRepository _libraryRepository;
        #endregion

        #region Public properties
        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; set; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public string InfoText { get; set; }
        public bool IsProcessing { get; set; }
        public IExplorerHistory ExplorerHistory { get; }

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

        #region Event handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RefreshViewEvent;
        #endregion 

        public LibraryExplorerViewModel(
            IExplorerHistory explorerHistory,
            ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;

            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();
            ExplorerHistory = explorerHistory;

            PropertyChanged += OnCurrentlyOpenedElementChanged;
            RefreshViewEvent += OnRefreshView;
        }

        #region Event handlers
        private async void OnCurrentlyOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(CurrentlyOpenedElement))
            {
                await LoadCurrentlyShownElementsAsync();
                
                if (CurrentlyOpenedElement is Library)
                    InvokeTagsChanged();
            }
        }

        private async void OnRefreshView(object sender, EventArgs args)
        {
            await LoadCurrentlyShownElementsAsync();
        }
        #endregion

        #region Private methods
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Public methods
        public async Task LoadCurrentlyShownElementsAsync()
        {
            CurrentlyShownElements.Clear();

            if (CurrentlyOpenedElement == null)
            {
                var libraries = await Task.Run(() => _libraryRepository.Query().GetAll().ToList());
                foreach (IExplorableElement t in libraries)
                {
                    CurrentlyShownElements.Add(t);
                    t.LoadIcon();
                }
            }
            else if (CurrentlyOpenedElement is Library library)
            {
                var updatedLibrary = _libraryRepository.Query() // reload library
                    .FromAllDataSources()
                    .WithId(library.Id)
                    .ToList()
                    .SingleOrDefault(); 

                if (updatedLibrary == null)
                    return;

                _currentlyOpenedElement = updatedLibrary;
                
                foreach (var t in updatedLibrary.Images)
                {
                    CurrentlyShownElements.Add(t);
                    t.LoadIcon();
                }
            }
        }

        public void LoadCurrentlyShownElements(IEnumerable<Tag> tags)
        {
            if (CurrentlyOpenedElement == null) return;

            CurrentlyShownElements.Clear();

            if(CurrentlyOpenedElement is Library library)
            {
                foreach (var i in library.Images)
                {
                    if (i.Tags.Intersect(tags).Any() || !tags.Any())
                    {
                        CurrentlyShownElements.Add(i);
                        i.LoadIcon();
                    }
                }
            }
        }

        public void Back()
        {
            if (ExplorerHistory.BackStack.Count == 0) return;

            ExplorerHistory.ForwardStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.BackStack.Pop();
            NotifyPropertyChanged(nameof(CurrentlyOpenedElement));
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.ForwardStack.Pop();
            NotifyPropertyChanged(nameof(CurrentlyOpenedElement));
        }

        public void InvokeTagsChanged()
        {
            NotifyPropertyChanged(nameof(Library.Tags));
        }
        
        public void RefreshView(object sender, EventArgs args)
        {
            RefreshViewEvent?.Invoke(sender, args);
        }
        #endregion
    }
}
