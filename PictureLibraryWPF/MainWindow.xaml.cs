using Autofac;
using PictureLibraryWPF.CustomControls;
using System;
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
        private IMainWindowControlsFactory _controlsFactory;
        private Pages Page { get; set; }

        public MainWindow(IMainWindowControlsFactory controlsFactory)
        {
            _controlsFactory = controlsFactory;

            InitializeComponent();
            LoadHomePage();
        }

        #region Page loading methods
        private void LoadHomePage()
        {
            Page = Pages.Home;

            ResetButtonClickedRectangles();
            HomeButtonClickedRectangle.Fill = new SolidColorBrush(Color.FromArgb(0,102,255,1)); // change rectangle color to #0066ff
        }

        private void LoadFileExplorerPage()
        {
            Page = Pages.FileExplorer;

            ResetButtonClickedRectangles();
            FileButtonClickedRectangle.Fill = new SolidColorBrush(Color.FromArgb(0, 102, 255, 1)); // change rectangle color to #0066ff

            // Add files tree to the grid
            var filesTree = _controlsFactory.GetFileElementsTree();
            Grid.Children.Add(filesTree);
            filesTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            filesTree.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(filesTree, 0);
            Grid.SetRow(filesTree, 6);

            // Add files view to the grid
            var filesView = _controlsFactory.GetFileElementsView();
            MainPanelGrid.Children.Add(filesView);
            filesView.HorizontalAlignment = HorizontalAlignment.Stretch;
            filesView.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(filesView, 0);
            Grid.SetRow(filesView, 2);
            Grid.SetColumnSpan(filesView, 2);
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
    }
}
