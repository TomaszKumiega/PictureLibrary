using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CreateFolderCommand : ICommand
    {
        private IFileExplorerViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public CreateFolderCommand(IFileExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if ((parameter as string) == null) return false;
            else if ((parameter as string).Trim() == String.Empty) return false;
            else return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.CreateDirectory(parameter as string);
        }
    }
}
