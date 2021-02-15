using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerViewModel : ILibraryExplorerViewModel
    {
        private IRepository<Library> _libraryRepository;

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public string InfoText { get; set; }
        public bool IsProcessing { get; set; }
        public IExplorableElement CurrentlyOpenedElement { get; set; }

        public IExplorerHistory ExplorerHistory { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LibraryExplorerViewModel(IRepository<Library> libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public Task LoadCurrentlyShownElements()
        {
            throw new NotImplementedException();
        }

        public void Back()
        {
            throw new NotImplementedException();
        }

        public void Forward()
        {
            throw new NotImplementedException();
        }
    }
}
