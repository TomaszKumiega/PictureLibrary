using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RenameCommand : ICommand
    {
        private IExplorerToolboxViewModel ViewModel { get; }
        public event EventHandler CanExecuteChanged;

        public RenameCommand(IExplorerToolboxViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.CommonViewModel.SelectedElements == null) return false;
            else if (ViewModel.CommonViewModel.SelectedElements.Any() && !ViewModel.IsDriveSelected()) return true;
            else return false;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.Rename();
        }
    }
}
