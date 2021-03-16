using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
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
        private IFileExplorerToolboxViewModel ViewModel { get; }

        public PasteCommand(IFileExplorerToolboxViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return ViewModel.Clipboard.ContainsFiles();
        }

        public async void Execute(object parameter)
        {
            await ViewModel.Paste();
        }
    }
}
