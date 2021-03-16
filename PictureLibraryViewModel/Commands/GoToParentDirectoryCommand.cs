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
        private IFileExplorerToolboxViewModel ViewModel { get; }
        public event EventHandler CanExecuteChanged;

        public GoToParentDirectoryCommand(IFileExplorerToolboxViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.CommonViewModel.CurrentlyOpenedElement == null) return false;
            var parent = System.IO.Directory.GetParent(ViewModel.CommonViewModel.CurrentlyOpenedElement.FullName);

            if (parent != null) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            ViewModel.GoToParentDirectory();
        }
    }
}
