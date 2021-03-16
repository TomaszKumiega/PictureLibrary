using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class AddTagCommand : ICommand
    {
        private IAddTagDialogViewModel ViewModel { get; }

        public event EventHandler CanExecuteChanged;

        public AddTagCommand(IAddTagDialogViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.AddAsync();
        }
    }
}
