using PictureLibraryWPF.CustomControls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PictureLibraryWPF
{
    public enum Pages
    {
        Home,
        FileExplorer,
        LibraryExplorer,
        Settings
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Control> _currentPageControls;
        private readonly Func<LibraryExplorerToolbar> _libraryExplorerToolbarLocator;
        private readonly Func<FileExplorerToolbar> _fileExplorerToolbarLocator;
        private readonly Func<FileTree> _fileTreeLocator;
        private readonly Func<LibraryTree> _libraryTreeLocator;
        private readonly Func<FilesView> _filesViewLocator;
        private readonly Func<LibraryView> _libraryViewLocator;
        private readonly Func<SettingsPanel> _settingsPanelLocator;

        public MainWindow(
            Func<LibraryExplorerToolbar> libraryExplorerToolbarLocator,
            Func<FileExplorerToolbar> fileExplorerToolbarLocator,
            Func<FileTree> fileTreeLocator,
            Func<LibraryTree> libraryTreeLocator,
            Func<FilesView> filesViewLocator,
            Func<LibraryView> libraryViewLocator,
            Func<SettingsPanel> settingsPanelLocator)
        {
            _fileExplorerToolbarLocator = fileExplorerToolbarLocator;
            _currentPageControls = new List<Control>();
            _libraryExplorerToolbarLocator = libraryExplorerToolbarLocator;
            _fileTreeLocator = fileTreeLocator;
            _libraryTreeLocator= libraryTreeLocator;
            _filesViewLocator = filesViewLocator;
            _libraryViewLocator = libraryViewLocator;
            _settingsPanelLocator = settingsPanelLocator;

            InitializeComponent();
            LoadHomePage();
        }

        //TODO: refactor
        #region Page loading methods
        private void LoadHomePage()
        {
            // Reset window to default state
            ResetButtonClickedRectangles();
            RemoveCurrentPageControlsFromTheGrid();

            HomeButtonClickedRectangle.Fill = new SolidColorBrush(Color.FromArgb(0,102,255,1)); // change rectangle color to #0066ff
        }

        private async Task LoadFileExplorerPageAsync()
        {
            // Reset window to default state
            ResetButtonClickedRectangles();
            RemoveCurrentPageControlsFromTheGrid();

            FileButtonClickedRectangle.Fill = new SolidColorBrush(Color.FromArgb(0, 102, 255, 1)); // change rectangle color to #0066ff
            ToolBarShadow.Fill = new SolidColorBrush(Color.FromArgb(29, 31, 33, 1)); // change rectangle color to #2b2d30

            // Add files tree to the grid
            var filesTree = _fileTreeLocator();
            await filesTree.Initialize();

            Grid.Children.Add(filesTree);
            filesTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            filesTree.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(filesTree, 0);
            Grid.SetRow(filesTree, 8);

            // Add files view to the grid
            var filesView = _filesViewLocator();
            await filesView.Initialize();
            MainPanelGrid.Children.Add(filesView);
            filesView.HorizontalAlignment = HorizontalAlignment.Stretch;
            filesView.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(filesView, 0);
            Grid.SetRow(filesView, 2);
            Grid.SetColumnSpan(filesView, 2);

            // Add file explorer toolbar to the grid
            var toolbar = _fileExplorerToolbarLocator();
            MainPanelGrid.Children.Add(toolbar);
            toolbar.HorizontalAlignment = HorizontalAlignment.Stretch;
            toolbar.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(toolbar, 0);
            Grid.SetRow(toolbar, 0);
            Grid.SetColumnSpan(toolbar, 2);

            _currentPageControls.Add(filesTree);
            _currentPageControls.Add(filesView);
            _currentPageControls.Add(toolbar);
        }

        private async Task LoadLibraryExplorerPageAsync()
        {
            // Reset window to default state
            ResetButtonClickedRectangles();
            RemoveCurrentPageControlsFromTheGrid();

            LibrariesButtonClickedRectangle.Fill = new SolidColorBrush(Color.FromArgb(0, 102, 255, 1)); // change rectangle color to #0066ff
            ToolBarShadow.Fill = new SolidColorBrush(Color.FromArgb(29, 31, 33, 1)); // change rectangle color to #2b2d30


            // Add libraries tree to the grid
            var librariesTree = _libraryTreeLocator();
            await librariesTree.Initialize();

            Grid.Children.Add(librariesTree);
            librariesTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            librariesTree.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(librariesTree, 0);
            Grid.SetRow(librariesTree, 8);

            // Add libraries view to the grid
            var librariesView = _libraryViewLocator();
            await librariesView.Initialize();
            MainPanelGrid.Children.Add(librariesView);
            librariesView.HorizontalAlignment = HorizontalAlignment.Stretch;
            librariesView.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(librariesView, 0);
            Grid.SetRow(librariesView, 2);
            Grid.SetColumnSpan(librariesView, 2);

            // Add libraries toolbar to the grid
            var librariesToolbar = _libraryExplorerToolbarLocator();
            MainPanelGrid.Children.Add(librariesToolbar);
            librariesToolbar.HorizontalAlignment = HorizontalAlignment.Stretch;
            librariesToolbar.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(librariesToolbar, 0);
            Grid.SetRow(librariesToolbar, 0);
            Grid.SetColumnSpan(librariesToolbar, 2);

            _currentPageControls.Add(librariesTree);
            _currentPageControls.Add(librariesView);
            _currentPageControls.Add(librariesToolbar);
        }

        private async Task LoadSettingsPageAsync()
        {
            ResetButtonClickedRectangles();
            RemoveCurrentPageControlsFromTheGrid();

            // Add libraries view to the grid
            var settingsPanel = _settingsPanelLocator();
            MainPanelGrid.Children.Add(settingsPanel);
            settingsPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            settingsPanel.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(settingsPanel, 0);
            Grid.SetRow(settingsPanel, 1);
            Grid.SetColumnSpan(settingsPanel, 2);
            Grid.SetRowSpan(settingsPanel, 2);

            _currentPageControls.Add(settingsPanel);
        }

        private void RemoveCurrentPageControlsFromTheGrid()
        {
            foreach(var t in _currentPageControls)
            {
                Grid.Children.Remove(t);
                MainPanelGrid.Children.Remove(t);
            }

            _currentPageControls.Clear();
        }

        private void ResetButtonClickedRectangles()
        {
            HomeButtonClickedRectangle.Fill = Brushes.Transparent;
            FileButtonClickedRectangle.Fill = Brushes.Transparent;
            LibrariesButtonClickedRectangle.Fill = Brushes.Transparent;
        }
        #endregion

        #region Window behaviour methods
        /// <summary>
        /// Allows to drag window with a mouse with its left button down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleTitleBarBackground_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        private void Minimize(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Maximizes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Maximize(object sender, EventArgs e)
        {
            WindowState = (WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal);
        }

        /// <summary>
        /// Shuts down the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Button click event handlers
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            LoadHomePage();
        }

        private async void FilesButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadFileExplorerPageAsync();
        }

        private async void LibrariesButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadLibraryExplorerPageAsync();
        }

        private async void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadSettingsPageAsync();
        }
        #endregion
    }
}
