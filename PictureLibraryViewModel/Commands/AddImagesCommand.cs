using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class AddImagesCommand : ICommand
    {
        private IAddImagesDialogViewModel ViewModel { get; }
        public event EventHandler CanExecuteChanged;

        public AddImagesCommand(IAddImagesDialogViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void RaiseCanExecuteChanged(object sender, EventArgs args)
        {
            CanExecuteChanged?.Invoke(this, args);
        }

        public bool CanExecute(object parameter)
        {
            return ViewModel.SelectedLibrary != null;
        }

        public async void Execute(object parameter)
        {
            await ViewModel.AddAsync();
        }
    }
}
