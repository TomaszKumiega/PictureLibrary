using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RemoveCommand : ICommand
    {
        private IExplorerToolboxViewModel ViewModel { get; }
        public event EventHandler CanExecuteChanged;

        public RemoveCommand(IExplorerToolboxViewModel viewModel)
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
            else if (ViewModel.CommonViewModel.SelectedElements.Any() && !ViewModel.IsDriveSelected()) return true;
            else return false;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.Remove();
        }
    }
}
