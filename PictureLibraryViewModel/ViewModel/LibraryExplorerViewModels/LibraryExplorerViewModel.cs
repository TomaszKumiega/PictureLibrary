using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
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
        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; set; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public string InfoText { get; set; }
        public bool IsProcessing { get; set; }
        public IDataSourceCollection DataSourceCollection { get; } 

        public IExplorerHistory ExplorerHistory { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RefreshViewEvent;

        public LibraryExplorerViewModel(IDataSourceCollection dataSourceCollection, IExplorerHistory explorerHistory)
        {
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();
            DataSourceCollection = dataSourceCollection;
            ExplorerHistory = explorerHistory;

            DataSourceCollection.Initialize(new List<IRemoteStorageInfo>());

            PropertyChanged += OnCurrentlyOpenedElementChanged;
            RefreshViewEvent += OnRefreshView;
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

        private void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadCurrentlyShownElementsAsync()
        {
            CurrentlyShownElements.Clear();

            if (CurrentlyOpenedElement == null)
            {
                var libraries = await Task.Run(() => DataSourceCollection.GetAllLibraries());
                foreach (IExplorableElement t in libraries)
                {
                    CurrentlyShownElements.Add(t);
                    t.LoadIcon();
                }
            }
            else if (CurrentlyOpenedElement is Library library)
            {
                foreach (var t in library.Images)
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
                    if (i.Tags.Intersect(tags).Any())
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
            InvokePropertyChanged(nameof(CurrentlyOpenedElement));
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.ForwardStack.Pop();
            InvokePropertyChanged(nameof(CurrentlyOpenedElement));
        }

        public void InvokeTagsChanged()
        {
            InvokePropertyChanged(nameof(Library.Tags));
        }
        
        public void RefreshView(object sender, EventArgs args)
        {
            RefreshViewEvent?.Invoke(sender, args);
        }
    }
}
