﻿using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class BackCommand : ICommand
    {
        private IFileExplorerToolboxViewModel ViewModel { get; }
        public event EventHandler CanExecuteChanged;

        public BackCommand(IFileExplorerToolboxViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return (ViewModel.CommonViewModel as IFileExplorerViewModel).ExplorerHistory.BackStack.Count > 1;
        }

        public void Execute(object parameter)
        {
            (ViewModel.CommonViewModel as IFileExplorerViewModel).Back();
        }
    }
}
