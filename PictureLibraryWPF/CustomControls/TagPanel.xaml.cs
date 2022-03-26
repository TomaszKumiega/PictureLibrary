﻿using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for TagPanel.xaml
    /// </summary>
    public partial class TagPanel : UserControl
    {
        private readonly Func<AddTagDialog> _addTagDialogLocator;

        public TagPanel(ITagPanelViewModel viewModel, Func<AddTagDialog> addTagDialogLocator)
        {
            DataContext = viewModel;
            _addTagDialogLocator = addTagDialogLocator;

            InitializeComponent();
        }

        //TODO: remove methods
        private void TagsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as ITagPanelViewModel;

            foreach(var t in TagsListView.SelectedItems)
            {
                var tag = (t as ListViewItem)?.DataContext as Tag;
                
                if (tag != null)
                    viewModel.SelectedTags.Add(tag);
            }
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            var addTagDialog = _addTagDialogLocator();
            addTagDialog.ShowDialog();
        } 
    }
}
