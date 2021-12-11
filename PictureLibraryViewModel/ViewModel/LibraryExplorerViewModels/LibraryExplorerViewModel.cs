using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteStorages;
using PictureLibraryViewModel.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    //TODO: REFACTOR
    public class LibraryExplorerViewModel : ILibraryExplorerViewModel
    {
        private IExplorableElement _currentlyOpenedElement;

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; set; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public string InfoText { get; set; }
        public bool IsProcessing { get; set; }
        public IExplorableElement CurrentlyOpenedElement { get; set; }
        public IDataSourceCollection DataSourceCollection { get; } 

        public IExplorerHistory ExplorerHistory { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LibraryExplorerViewModel(IDataSourceCollection dataSourceCollection)
        {
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();
            DataSourceCollection = dataSourceCollection;

            DataSourceCollection.Initialize(new List<IRemoteStorageInfo>());
        }

        public async Task LoadCurrentlyShownElementsAsync()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            if (CurrentlyOpenedElement == null)
            {
                var libraries = await Task.Run(() => DataSourceCollection.GetAllLibraries());
                foreach (IExplorableElement t in libraries)
                {
                    CurrentlyShownElements.Add(t);
                }
            }
            else if (CurrentlyOpenedElement is Library library)
            {
                if(library.RemoteStorageInfoId == null)
                {
                    foreach (var t in library.Images)
                    {
                        CurrentlyShownElements.Add(t);
                    }
                }

                //TODO: Add icons to imageFiles from diffrent origins than local

            }
        }

        public async Task LoadCurrentlyShownElements(IEnumerable<Tag> tags)
        {
            if (CurrentlyOpenedElement == null) return;
            if (!(CurrentlyOpenedElement is Library)) return;

            CurrentlyShownElements.Clear();

            if(CurrentlyOpenedElement is Library library && library.RemoteStorageInfoId == null)
            {
                foreach (var i in library.Images)
                {
                    foreach(var t in tags)
                    {
                        if(i.Tags.Contains(t)) await Task.Run(() => CurrentlyShownElements.Add(i));
                    }
                }
            }

            //TODO: Add icons to imageFiles from diffrent origins than local
        }
    

        public void Back()
        {
            if (ExplorerHistory.BackStack.Count == 0) return;

            ExplorerHistory.ForwardStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.BackStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedElement"));
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.ForwardStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedElement"));
        }

        public async Task AddTagAsync(Tag tag)
        {
            var library = CurrentlyOpenedElement as Library;
            library.Tags.Add(tag);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedElement"));
            var dataSource = DataSourceCollection.GetDataSourceByRemoteStorageId(library.RemoteStorageInfoId);
            await Task.Run(() => dataSource.LibraryProvider.UpdateLibrary(library));
        }
    }
}
