using Autofac;
using PictureLibraryWPF.CustomControls.Files;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PictureLibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilesTree FilesTree { get; set; }
        private GridSplitter LeftPanelGridSplitter { get; set; }
        public MainWindow()
        {
            var container = PictureLibraryViewModel.ContainerConfig.Configure();
            FilesTree = container.Resolve<FilesTree>();
            LeftPanelGridSplitter = new GridSplitter();

            InitializeComponent();
            InitializeControlsOnStartup();
        }

        private void InitializeControlsOnStartup()
        {
            //TODO: change color of filestree scrollbar
            #region FilesTree
            Grid.Children.Add(FilesTree);         
            FilesTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            FilesTree.VerticalAlignment = VerticalAlignment.Stretch;
            Grid.SetColumn(FilesTree, 0);
            Grid.SetRow(FilesTree, 4);
            #endregion

            #region LeftPanelGridSplitter
            Grid.Children.Add(LeftPanelGridSplitter);
            LeftPanelGridSplitter.HorizontalAlignment = HorizontalAlignment.Right;
            LeftPanelGridSplitter.VerticalAlignment = VerticalAlignment.Stretch;
            LeftPanelGridSplitter.ShowsPreview = true;
            LeftPanelGridSplitter.Width = 5;
            LeftPanelGridSplitter.Opacity = 0;
            Grid.SetColumn(LeftPanelGridSplitter, 0);
            Grid.SetRow(LeftPanelGridSplitter, 4);
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
