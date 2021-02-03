using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class GoToParentDirectoryCommand : ICommand
    {
        private IFileExplorerViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public GoToParentDirectoryCommand(IFileExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            var parent = System.IO.Directory.GetParent(_viewModel.CurrentDirectoryPath);

            if (parent != null) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.GoToParentDirectory();
        }
    }
}
