using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
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
        public ICommand RemoveCommand { get; }
        public ICommand RenameCommand { get; }
        public ICommand RefreshCommand { get; }
        public string SearchText { get; set; }
        #endregion

        public LibraryExplorerToolboxViewModel(ILibraryExplorerViewModel commonVM, ICommandFactory commandFactory, IClipboardService clipboard)
        {
            RemoveCommand = commandFactory.GetRemoveCommand(this);
            RenameCommand = commandFactory.GetRenameCommand(this);
            RefreshCommand = commandFactory.GetRefreshCommand(this);

            Clipboard = clipboard;
            CommonViewModel = commonVM;

            CommonViewModel.SelectedElements.CollectionChanged += OnSelectedElementsChanged;
        }

        #region Event Handler methods
        private void OnSelectedElementsChanged(object o, EventArgs args)
        {
            (RemoveCommand as RemoveCommand).OnExecuteChanged();
        }
        #endregion

        #region Public methods
        public async Task Refresh()
        {
            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }

        public async Task Remove()
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
                    await Task.Run(() => dataSource.ImageProvider.RemoveImage(imageFile));
                }

                (CommonViewModel as ILibraryExplorerViewModel).RefreshView(this, EventArgs.Empty);
            }
        }

        public Task Rename()
        {
            throw new NotImplementedException();
        }

        public bool IsDriveSelected()
        {
            return false;
        }
        #endregion
    }
}
