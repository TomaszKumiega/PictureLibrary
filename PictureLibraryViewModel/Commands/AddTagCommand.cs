using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class AddTagCommand : ICommand
    {
        private IAddTagDialogViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public AddTagCommand(IAddTagDialogViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _viewModel.AddAsync();
        }
    }
}
