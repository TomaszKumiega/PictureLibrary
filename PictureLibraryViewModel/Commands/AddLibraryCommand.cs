using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class AddLibraryCommand : ICommand
    {
        private IAddLibraryDialogViewModel ViewModel { get; }

        public event EventHandler CanExecuteChanged;

        public AddLibraryCommand(IAddLibraryDialogViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return ViewModel.CanAdd();
        }

        public async void Execute(object parameter)
        {
            await ViewModel.AddAsync();
        }
    }
}
