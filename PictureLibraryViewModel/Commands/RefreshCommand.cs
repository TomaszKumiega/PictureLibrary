using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class RefreshCommand : ICommand
    {
        private IExplorerToolboxViewModel ViewModel { get; }
        public event EventHandler CanExecuteChanged;

        public RefreshCommand(IExplorerToolboxViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.Refresh();
        }
    }
}
