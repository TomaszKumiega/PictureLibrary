using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class ForwardCommand : ICommand
    {
        private IExplorerViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public ForwardCommand(IExplorerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged(object sender, EventArgs args)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
