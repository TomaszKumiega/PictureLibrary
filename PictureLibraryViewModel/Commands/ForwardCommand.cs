using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class ForwardCommand : ICommand
    {
        private IFileExplorerToolboxViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public ForwardCommand(IFileExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return (_viewModel.CommonViewModel as IFileExplorerViewModel).ExplorerHistory.ForwardStack.Count > 0;
        }

        public void Execute(object parameter)
        {
            (_viewModel.CommonViewModel as IFileExplorerViewModel).Forward();
        }
    }
}
