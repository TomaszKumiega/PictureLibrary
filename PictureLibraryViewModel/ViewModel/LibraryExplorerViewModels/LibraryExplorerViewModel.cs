using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerViewModel : ILibraryExplorerViewModel
    {
        private IExplorableElement _currentlyOpenedElement;
        private IRepository<Library> LibraryRepository { get; }

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; set; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public string InfoText { get; set; }
        public bool IsProcessing { get; set; }
        public IExplorableElement CurrentlyOpenedElement { get; set; }

        public IExplorerHistory ExplorerHistory { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LibraryExplorerViewModel(IRepository<Library> libraryRepository)
        {
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();
            LibraryRepository = libraryRepository;
        }

        public async Task LoadCurrentlyShownElementsAsync()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            if (CurrentlyOpenedElement == null)
            {
                var libraries = await LibraryRepository.GetAllAsync();
                foreach (IExplorableElement t in libraries)
                {
                    CurrentlyShownElements.Add(t);
                }
            }
            else if (CurrentlyOpenedElement is Library)
            {
                if(CurrentlyOpenedElement.Origin == Origin.Local)
                {
                    foreach (var t in (CurrentlyOpenedElement as Library).Images)
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

            if(CurrentlyOpenedElement.Origin == Origin.Local)
            {
                foreach (var i in (CurrentlyOpenedElement as Library).Images)
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
            var library = (CurrentlyOpenedElement as Library);
            library.Tags.Add(tag);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedElement"));
            await LibraryRepository.UpdateAsync(library);
        }
    }
}
