using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModels
{
    public class LibrariesViewModel : ILibrariesViewModel
    {
        private ILibraryFileService _libraryFileService;
        public ObservableCollection<Library> Libraries { get; private set; }
        public ObservableCollection<ILibraryEntity> DisplayedEntities { get; }

        public LibrariesViewModel(ILibraryFileService libraryFileService)
        {
            _libraryFileService = libraryFileService;
            Libraries = new ObservableCollection<Library>();
            DisplayedEntities = new ObservableCollection<ILibraryEntity>();
            InitializeLibraries();
        }

        public async Task InitializeLibraries()
        {
            Libraries = await _libraryFileService.GetAllLibrariesAsync();
        }
    }
}
