using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteServer;
using PictureLibraryModel.Repositories;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddLibraryDialogViewModel : IAddLibraryDialogViewModel
    {
        private ILibraryExplorerViewModel _commonViewModel;
        private IRepository<Library> _libraryRepository;
        private IConnectedServicesInfoProviderService _connectedServices;

        public string Name { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public Origin? Origin { get; set; }
        public List<Origin> Origins { get => _connectedServices.GetAllAvailableOrigins(); }
        public List<User> Owners { get; }
        public ICommand AddLibraryCommand { get; }

        public event InvalidInputEventHandler InvalidInput;

        public AddLibraryDialogViewModel(ILibraryExplorerViewModel commonVM, IRepository<Library> libraryRepository, IConnectedServicesInfoProviderService connectedServices)
        {
            _commonViewModel = commonVM;
            _libraryRepository = libraryRepository;
            _connectedServices = connectedServices;
        }

        private bool IsNameValid()
        {
            if (!Regex.Match(Name, "^[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }

            return true;
        }

        private bool IsOriginValid()
        {
            if (Origin == null) return false;
            else return true;
        }

        public async Task AddAsync()
        {
            if(!IsNameValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Name"));
            if (!IsOriginValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Origin"));

            var library = new Library();

            library.Name = Name;
            if (Description == null) library.Description = "";
            else library.Description = Description;
            library.Origin = Origin.Value;

            if (Origin == PictureLibraryModel.Model.Origin.Local) library.FullName = FullName;

            await _libraryRepository.AddAsync(library);
            await _commonViewModel.LoadCurrentlyShownElements();
        }
    }
}
