using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for TagPanel.xaml
    /// </summary>
    public partial class TagPanel : UserControl
    {
        private Func<AddTagDialog> AddTagDialogLocator { get; }
        public TagPanel(ITagPanelViewModel viewModel, Func<AddTagDialog> addTagDialogLocator)
        {
            DataContext = viewModel;
            AddTagDialogLocator = addTagDialogLocator;

            InitializeComponent();
        }

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
            var addTagDialog = AddTagDialogLocator();
            addTagDialog.ShowDialog();
        } 
    }
}
