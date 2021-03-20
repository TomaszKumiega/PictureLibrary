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
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private IRepository<Library> LibraryRepository { get; }
        private IConnectedServicesInfoProviderService ConnectedServices { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public Origin? SelectedOrigin { get; set; }
        public List<Origin> Origins { get => ConnectedServices.GetAllAvailableOrigins(); }
        public ICommand AddLibraryCommand { get; }

        public event InvalidInputEventHandler InvalidInput;

        public AddLibraryDialogViewModel(ILibraryExplorerViewModel commonVM, IRepository<Library> libraryRepository, IConnectedServicesInfoProviderService connectedServices)
        {
            CommonViewModel = commonVM;
            LibraryRepository = libraryRepository;
            ConnectedServices = connectedServices;
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
            if (SelectedOrigin == null) return false;
            else return true;
        }

        public async Task AddAsync()
        {
            if(!IsNameValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Name"));
            if (!IsOriginValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Origin"));

            var library = new Library();

            switch(SelectedOrigin)
            {
                case Origin.Local:
                    {
                        library.Name = Name;
                        if (Description == null) library.Description = "";
                        else library.Description = Description;
                        library.Origin = SelectedOrigin.Value;
                        if (SelectedOrigin == PictureLibraryModel.Model.Origin.Local) library.FullName = FullName;
                    }
                    break;
            }

            await LibraryRepository.AddAsync(library);
            await CommonViewModel.LoadCurrentlyShownElements();
        }
    }
}
