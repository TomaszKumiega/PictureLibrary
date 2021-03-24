using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerToolboxViewModel : ILibraryExplorerToolboxViewModel
    {
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
        }

        public async Task Refresh()
        {
            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }

        public Task Remove()
        {
            throw new NotImplementedException();
        }

        public Task Rename()
        {
            throw new NotImplementedException();
        }

        public bool IsDriveSelected()
        {
            bool isDriveSelected = false;

            foreach (var t in CommonViewModel.SelectedElements)
            {
                if (t is Drive)
                {
                    isDriveSelected = true;
                    break;
                }
            }

            return isDriveSelected;
        }
    }
}
