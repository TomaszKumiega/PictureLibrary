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

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.Clipboard.ContainsFiles();
        }

        public void Execute(object parameter)
        {
            _viewModel.PasteSelectedElements();
        }
    }
}
