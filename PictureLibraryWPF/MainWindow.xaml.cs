using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using PictureLibraryWPF.CustomControls.Files;

namespace PictureLibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            // TODO move filestree initialization to diffrent method 
            // TODO change color of filestree scrollbar
            var container = PictureLibraryViewModel.ContainerConfig.Configure();
            var filesTree = container.Resolve<FilesTree>();
            InitializeComponent();
            Grid.Children.Add(filesTree);
            Grid.SetColumn(filesTree, 0);
            Grid.SetRow(filesTree, 4);
            filesTree.HorizontalAlignment = HorizontalAlignment.Stretch;
            filesTree.VerticalAlignment = VerticalAlignment.Stretch;

            // TODO move grid splitter initialization to diffrent method
            var gridSplitter = new GridSplitter();
            Grid.Children.Add(gridSplitter);
            gridSplitter.HorizontalAlignment = HorizontalAlignment.Right;
            gridSplitter.VerticalAlignment = VerticalAlignment.Stretch;
            gridSplitter.ShowsPreview = true;
            gridSplitter.Width = 5;
            gridSplitter.Opacity = 0;

            Grid.SetColumn(gridSplitter, 0);
            Grid.SetRow(gridSplitter, 4);            
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
