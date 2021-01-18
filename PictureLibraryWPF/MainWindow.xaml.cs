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

        private ElementsView ElementsView { get; }
        private ElementsTree ElementsTree { get; }

        private Pages Page { get; set; }

        public MainWindow(IMainWindowControlsFactory controlsFactory)
        {
            _controlsFactory = controlsFactory;

            InitializeComponent();
            LoadHomePage();
        }

        private void LoadHomePage()
        {
            Page = Pages.Home;

            ResetButtonClickedRectangles();
            HomeButtonClickedRectangle.Fill = new SolidColorBrush(Color.FromArgb(0,102,255,1)); // change rectangle color to #0066ff
        }

        private void ResetButtonClickedRectangles()
        {
            HomeButtonClickedRectangle.Fill = Brushes.Transparent;
            FileButtonClickedRectangle.Fill = Brushes.Transparent;
            LibrariesButtonClickedRectangle.Fill = Brushes.Transparent;
        }

        private void InitializeControlsOnStartup()
        {
            //TODO: change color of filestree scrollbar
            //TODO: fix formatting / add spaces after and before regions
            
            #region ElementsTree
            Grid.Children.Add(ElementsTree);         
            ElementsTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            ElementsTree.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(ElementsTree, 0);
            Grid.SetRow(ElementsTree, 4);
            #endregion

            #region ElementsView
            Grid.Children.Add(ElementsView);
            ElementsView.HorizontalAlignment = HorizontalAlignment.Stretch;
            ElementsView.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(ElementsView, 2);
            Grid.SetRow(ElementsView, 4);
            #endregion
        }


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
    }
}
