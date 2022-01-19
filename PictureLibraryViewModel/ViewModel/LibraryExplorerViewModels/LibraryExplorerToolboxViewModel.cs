using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerToolboxViewModel : ILibraryExplorerToolboxViewModel
    {
        private ILibraryExplorerViewModel LibraryCommonViewModel => (ILibraryExplorerViewModel)CommonViewModel;

        public IExplorerViewModel CommonViewModel { get; }
        public IClipboardService Clipboard { get; }
        public ICommand RemoveCommand { get; }
        public ICommand RenameCommand { get; }
        public ICommand RefreshCommand { get; }
        public string SearchText { get; set; }

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
                    await CommonViewModel.LoadCurrentlyShownElementsAsync();
                }
            }
            else if (CommonViewModel.SelectedElements.All(x => x is ImageFile))
            {
                foreach (ImageFile imageFile in CommonViewModel.SelectedElements)
                {
                    var dataSource = LibraryCommonViewModel.DataSourceCollection.GetDataSourceByRemoteStorageId(imageFile.RemoteStorageInfoId);
                    await Task.Run(() => dataSource.ImageProvider.RemoveImage(imageFile));
                    await CommonViewModel.LoadCurrentlyShownElementsAsync();
                }
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
    }
}
