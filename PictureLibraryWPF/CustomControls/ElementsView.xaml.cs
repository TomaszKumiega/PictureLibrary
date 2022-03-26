﻿using PictureLibraryModel.Model;
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
        private readonly Func<TagPanel> _tagPanelLocator;
        private bool _isTagPanelVisible;
        private TagPanel _tagPanel;

        public ElementsView(IExplorableElementsViewViewModel viewModel, Func<TagPanel> tagPanelLocator)
        {
            InitializeComponent();

            DataContext = viewModel;
            viewModel.CommonViewModel.PropertyChanged += OnOpenedElementChanged;
            _tagPanelLocator = tagPanelLocator;
        }

        //TODO: remove methods
        private void OnOpenedElementChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(ILibraryExplorerViewModel.CurrentlyOpenedElement) && DataContext is LibraryViewViewModel viewModel)
            {
                if (viewModel.CommonViewModel.CurrentlyOpenedElement is Library && !_isTagPanelVisible)
                {
                    ShowTags();
                }
                else if (viewModel.CommonViewModel.CurrentlyOpenedElement == null && _isTagPanelVisible)
                {
                    HideTags();
                }
            }
        }

        private void ShowTags()
        {
            var tagPanel = _tagPanelLocator();
            _tagPanel = tagPanel;

            ElementsViewGrid.Children.Add(tagPanel);
            tagPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            tagPanel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Grid.SetColumn(tagPanel, 1);
            Grid.SetRow(tagPanel, 0);
            Grid.SetColumnSpan(FilesList, 1);

            _isTagPanelVisible = true;
        }

        private void HideTags()
        {
            ElementsViewGrid.Children.Remove(_tagPanel);
            Grid.SetColumnSpan(FilesList, 2);

            _isTagPanelVisible = false;
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
