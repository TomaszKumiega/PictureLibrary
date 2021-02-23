using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
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
        public TagPanel(ITagPanelViewModel viewModel)
        {
            DataContext = viewModel;

            InitializeComponent();
        }

        private void TagsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as ITagPanelViewModel;

            viewModel.SelectedTag = TagsListView.SelectedItem as Tag;
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
