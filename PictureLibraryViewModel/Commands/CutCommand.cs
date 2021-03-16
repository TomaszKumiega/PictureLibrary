using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IFileExplorerToolboxViewModel ViewModel { get; }

        public CutCommand(IFileExplorerToolboxViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.CommonViewModel.SelectedElements == null) return false;
            else if (ViewModel.CommonViewModel.SelectedElements.Any()) return true;
            else return false;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.Cut();
        }
    }
}
