using PictureLibraryViewModel.ViewModel;
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
        private IExplorerViewModel _viewModel;

        public CutCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.SelectedElements == null) return false;
            else if (_viewModel.SelectedElements.Any()) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.CutSelectedElements();
        }
    }
}
