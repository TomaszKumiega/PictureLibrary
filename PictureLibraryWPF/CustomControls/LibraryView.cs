using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace PictureLibraryWPF.CustomControls
{
    public class LibraryView : ElementsView
    {
        private bool _isTagPanelVisible;
        private TagPanel _tagPanel;
        private GridSplitter _splitter;

        public LibraryView(ILibraryViewViewModel viewModel, GridSplitter splitter, TagPanel tagPanel) : base(viewModel)
        {
            _tagPanel = tagPanel;
            _splitter = splitter;
            
            viewModel.CommonViewModel.PropertyChanged += OnOpenedElementChanged;

            InitializeControls();
        }

        private void InitializeControls()
        {
            // GridSplitter
            var brushColor = Color.FromArgb(0, 34, 36, 39);
            _splitter.Background = new SolidColorBrush(brushColor);
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            _splitter.ResizeDirection = GridResizeDirection.Columns;
            _splitter.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            _splitter.Width = 2;
            Grid.SetColumn(_splitter, 1);
            Grid.SetRow(_splitter, 0);

            // TagPanel
            _tagPanel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            _tagPanel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            Grid.SetColumn(_tagPanel, 2);
            Grid.SetRow(_tagPanel, 0);
        }

        #region Event handlers
        protected void OnOpenedElementChanged(object sender, PropertyChangedEventArgs args)
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
        #endregion

        #region TagsPanel behaviour
        private void ShowTags()
        {
            ElementsViewGrid.Children.Add(_splitter);
            ElementsViewGrid.Children.Add(_tagPanel);

            Grid.SetColumnSpan(ElementsListView, 1);

            _isTagPanelVisible = true;
        }

        private void HideTags()
        {
            ElementsViewGrid.Children.Remove(_splitter);
            ElementsViewGrid.Children.Remove(_tagPanel);

            Grid.SetColumnSpan(ElementsListView, 3);

            _isTagPanelVisible = false;
        }
        #endregion
    }
}
