using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class AddImagesCommand : ICommand
    {
        private IAddImagesDialogViewModel _viewModel;
        public event EventHandler CanExecuteChanged;

        public AddImagesCommand(IAddImagesDialogViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void RaiseCanExecuteChanged(object sender, EventArgs args)
        {
            CanExecuteChanged?.Invoke(this, args);
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedLibrary != null;
        }

        public async void Execute(object parameter)
        {
            await _viewModel.AddAsync();
        }
    }
}
