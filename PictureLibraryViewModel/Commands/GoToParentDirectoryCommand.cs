using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class GoToParentDirectoryCommand : ICommand
    {
        private IFileExplorerToolboxViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public GoToParentDirectoryCommand(IFileExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CommonViewModel.CurrentlyOpenedElement == null) return false;
            var parent = System.IO.Directory.GetParent(_viewModel.CommonViewModel.CurrentlyOpenedElement.FullPath);

            if (parent != null) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.GoToParentDirectory();
        }
    }
}
