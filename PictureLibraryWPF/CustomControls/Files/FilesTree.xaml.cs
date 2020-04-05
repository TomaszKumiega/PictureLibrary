﻿using System;
using System.Windows;
using PictureLibraryViewModel.ViewModels;
using System.Windows.Controls;
using PictureLibraryModel.Model;

namespace PictureLibraryWPF.CustomControls.Files
{
    /// <summary>
    /// Interaction logic for FilesTree.xaml
    /// </summary>
    public partial class FilesTree : UserControl
    {
        public FilesTree(IFileSystemViewModel fileSystemViewModel)
        {
            InitializeComponent();
            DataContext = fileSystemViewModel;
        }

        private void FilesTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as IFileSystemViewModel;

            try
            {
                if (this.FilesTreeView.SelectedItem is Directory)
                    viewModel.CurrentDirectory = (this.FilesTreeView.SelectedItem as Directory).FullPath;
                else if (this.FilesTreeView.SelectedItem is Drive)
                    viewModel.CurrentDirectory = (this.FilesTreeView.SelectedItem as Drive).FullPath;
            }
            catch 
            {
                //TODO add logger
            }

        }
    }
}
