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
        public ObservableCollection<Library> Libraries { get; }
        public ObservableCollection<ILibraryEntity> DisplayedEntities { get; }

        public LibrariesViewModel(ILibraryFileService libraryFileService)
        {
            _libraryFileService = libraryFileService;
            Libraries = new ObservableCollection<Library>();
            DisplayedEntities = new ObservableCollection<ILibraryEntity>();
            Initialize();
        }

        private async Task Initialize()
        {
            var libraries = await _libraryFileService.GetAllLibraries();

            foreach (var t in libraries)
            {
                Libraries.Add(t);
                DisplayedEntities.Add(t);
            }
        }
    }
}
