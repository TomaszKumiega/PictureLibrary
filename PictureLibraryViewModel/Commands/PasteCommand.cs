using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class PasteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IExplorerViewModel _viewModel;

        public PasteCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.Clipboard.CopiedElements == null && _viewModel.Clipboard.CutElements == null) return false;
            else if (_viewModel.Clipboard.CopiedElements.Any() || _viewModel.Clipboard.CutElements.Any()) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            _viewModel.Paste();
        }
    }
}
