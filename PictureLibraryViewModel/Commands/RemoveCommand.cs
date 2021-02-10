using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RemoveCommand : ICommand
    {
        private IExplorerToolboxViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public RemoveCommand(IExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CommonViewModel.SelectedElements == null) return false;
            else if (_viewModel.CommonViewModel.SelectedElements.Count > 0) return true;
            else return false;
        }

        public async void Execute(object parameter)
        {
            await _viewModel.Remove();
        }
    }
}
