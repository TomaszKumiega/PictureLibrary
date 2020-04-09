using System;
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
                viewModel.CurrentDirectoryPath = (this.FilesTreeView.SelectedItem as IFileSystemEntity).FullPath;
            }
            catch 
            {
                //TODO add logger
            }

        }
    }
}
