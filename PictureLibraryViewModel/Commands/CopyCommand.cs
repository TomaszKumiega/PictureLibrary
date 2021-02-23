﻿using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public class CopyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IFileExplorerToolboxViewModel _viewModel;

        public CopyCommand(IFileExplorerToolboxViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OnExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CommonViewModel.SelectedElements == null) return false;
            else if (_viewModel.CommonViewModel.SelectedElements.Any()) return true;
            else return false;
        }

        public async void Execute(object parameter)
        {
            await _viewModel.Copy();
        }
    }
}
