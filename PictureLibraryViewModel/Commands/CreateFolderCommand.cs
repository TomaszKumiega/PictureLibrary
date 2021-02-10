using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CreateFolderCommand : ICommand
    {
        private IFileExplorerToolboxViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public CreateFolderCommand(IFileExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if ((parameter as string) == null) return false;
            else if ((parameter as string).Trim() == String.Empty) return false;
            else return true;
        }

        public async void Execute(object parameter)
        {
            await _viewModel.CreateDirectory();
        }
    }
}
