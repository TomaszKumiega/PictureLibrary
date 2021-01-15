using System;
using System.Windows;
using System.Windows.Controls;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;

namespace PictureLibraryWPF.CustomControls.Files
{
    /// <summary>
    /// Interaction logic for FilesTree.xaml
    /// </summary>
    public partial class FilesTree : UserControl
    {
        public FilesTree(IFileExplorerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void FilesTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as IFileExplorerViewModel;

            viewModel.SelectedNode = FilesTreeView.SelectedItem as IExplorableElement;
        }
    }
}
