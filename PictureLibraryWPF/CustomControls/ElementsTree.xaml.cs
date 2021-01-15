using System;
using System.Windows;
using System.Windows.Controls;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FilesTree.xaml
    /// </summary>
    public partial class ElementsTree : UserControl
    {
        public ElementsTree(IFileExplorerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ElementTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as IFileExplorerViewModel;

            viewModel.SelectedNode = ElementTreeView.SelectedItem as IExplorableElement;
        }
    }
}
