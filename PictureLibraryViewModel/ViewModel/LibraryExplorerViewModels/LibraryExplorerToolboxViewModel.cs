using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerToolboxViewModel : ILibraryExplorerToolboxViewModel
    {
        #region Private fields
        private ILibraryExplorerViewModel LibraryCommonViewModel => (ILibraryExplorerViewModel)CommonViewModel;
        #endregion

        #region Public properties
        public IExplorerViewModel CommonViewModel { get; }
        public IClipboardService Clipboard { get; }
        public string SearchText { get; set; }
        #endregion

        #region Commands
        [Command]
        public ICommand RemoveCommand { get; set; }
        [Command]
        public ICommand RenameCommand { get; set; }
        [Command]
        public ICommand RefreshCommand { get; set; }
        #endregion

        public LibraryExplorerToolboxViewModel(ILibraryExplorerViewModel commonVM, ICommandCreator commandCreator, IClipboardService clipboard)
        {
            Clipboard = clipboard;
            CommonViewModel = commonVM;

            commandCreator.InitializeCommands(this);
        }

        #region Command methods
        [CanExecute(nameof(RemoveCommand))]
        private bool CanExecuteRemoveCommand(object parameter)
        {
            return CommonViewModel.SelectedElements != null
                && CommonViewModel.SelectedElements.Any();
        }

        [Execute(nameof(RemoveCommand))]
        private async void ExecuteRemoveCommand(object parameter)
        {
            if (CommonViewModel.SelectedElements.All(x => x is Library))
            {
                foreach (Library library in CommonViewModel.SelectedElements)
                {
                    var dataSource = LibraryCommonViewModel.DataSourceCollection.GetDataSourceByRemoteStorageId(library.RemoteStorageInfoId);
                    await Task.Run(() => dataSource.LibraryProvider.RemoveLibrary(library));
                }

                (CommonViewModel as ILibraryExplorerViewModel).RefreshView(this, EventArgs.Empty);
            }
            else if (CommonViewModel.SelectedElements.All(x => x is ImageFile))
            {
                foreach (ImageFile imageFile in CommonViewModel.SelectedElements)
                {
                    var dataSource = LibraryCommonViewModel.DataSourceCollection.GetDataSourceByRemoteStorageId(imageFile.RemoteStorageInfoId);
                    var library = (Library)CommonViewModel.CurrentlyOpenedElement;
                    
                    library.Images.Remove(imageFile);

                    await Task.Run(() => dataSource.LibraryProvider.UpdateLibrary(library));
                    await Task.Run(() => dataSource.ImageProvider.RemoveImage(imageFile));
                }

                (CommonViewModel as ILibraryExplorerViewModel).RefreshView(this, EventArgs.Empty);
            }
        }

        [CanExecute(nameof(RenameCommand))]
        private bool CanExecuteRenameCommand(object parameter)
        {
            return CommonViewModel.SelectedElements.Count == 1;
        }

        [Execute(nameof(RenameCommand))]
        private void ExecuteRenameCommand(object parameter)
        {
            throw new NotImplementedException();
        }

        [Execute(nameof(RefreshCommand))]
        private async void ExecuteRefreshCommand(object parameter)
        {
            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }
        #endregion
    }
}
