using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class BackCommand : ICommand
    {
        private IFileExplorerToolboxViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public BackCommand(IFileExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return (_viewModel.CommonViewModel as IFileExplorerViewModel).ExplorerHistory.BackStack.Count > 1;
        }

        public void Execute(object parameter)
        {
            (_viewModel.CommonViewModel as IFileExplorerViewModel).Back();
        }
    }
}
