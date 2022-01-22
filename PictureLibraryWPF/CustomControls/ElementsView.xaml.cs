using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FilesView.xaml
    /// </summary>
    public partial class ElementsView : UserControl
    {
        private bool IsTagPanelVisible { get; set; }
        private Func<TagPanel> TagPanelLocator { get; }
        private TagPanel TagPanel { get; set; }

        public ElementsView(IExplorableElementsViewViewModel viewModel, Func<TagPanel> tagPanelLocator)
        {
            InitializeComponent();

            DataContext = viewModel;
            viewModel.CommonViewModel.PropertyChanged += OnOpenedElementChanged;
            TagPanelLocator = tagPanelLocator;
        }

        private void OnOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(ILibraryExplorerViewModel.CurrentlyOpenedElement) && DataContext is LibraryViewViewModel viewModel)
            {
                if (viewModel.CommonViewModel.CurrentlyOpenedElement is Library && !IsTagPanelVisible)
                {
                    ShowTags();
                }
                else if (viewModel.CommonViewModel.CurrentlyOpenedElement == null && IsTagPanelVisible)
                {
                    HideTags();
                }
            }
        }

        private void ShowTags()
        {
            var tagPanel = TagPanelLocator();
            TagPanel = tagPanel;

            ElementsViewGrid.Children.Add(tagPanel);
            tagPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            tagPanel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Grid.SetColumn(tagPanel, 1);
            Grid.SetRow(tagPanel, 0);
            Grid.SetColumnSpan(FilesList, 1);

            IsTagPanelVisible = true;
        }

        private void HideTags()
        {
            ElementsViewGrid.Children.Remove(TagPanel);
            Grid.SetColumnSpan(FilesList, 2);

            IsTagPanelVisible = false;
        }

        private void ItemMouseDoubleClick(object o, EventArgs args)
        {
            (DataContext as IExplorableElementsViewViewModel).CommonViewModel.CurrentlyOpenedElement = ((o as ListViewItem).DataContext as IExplorableElement);
        }

        private void FilesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as IExplorableElementsViewViewModel).CommonViewModel.SelectedElements.Clear();

            foreach (var t in FilesList.SelectedItems)
            {
                (DataContext as IExplorableElementsViewViewModel).CommonViewModel.SelectedElements.Add(t as IExplorableElement);
            }
        }
    }
}
