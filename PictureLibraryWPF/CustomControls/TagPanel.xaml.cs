using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Dialogs;
using System;
using System.Linq;
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

        private void TagsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (ITagPanelViewModel)DataContext;

            viewModel.SelectedTags.Clear();

            foreach(var t in TagsListView.SelectedItems)
            {
                var tag = (Tag)t;
                viewModel.SelectedTags.Add(tag);
            }

            viewModel.CommonViewModel.LoadCurrentlyShownElements(viewModel.SelectedTags.ToList());
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            var addTagDialog = _addTagDialogLocator();
            addTagDialog.ShowDialog();
        } 
    }
}
