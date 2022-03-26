﻿using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FilesTree.xaml
    /// </summary>
    public partial class ElementsTree : UserControl
    {
        public ElementsTree(IExplorableElementsTreeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        //TODO: remove
        private void TreeViewItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = DataContext as IExplorableElementsTreeViewModel;
            var item = sender as TreeViewItem;

            item.Focusable = true;
            item.Focus();
            item.Focusable = false;

            viewModel.SelectedNode = item.DataContext as IExplorableElement;

            e.Handled = true;
        }
    }
}
