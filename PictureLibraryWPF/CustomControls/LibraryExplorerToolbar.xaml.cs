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
    /// Interaction logic for LibraryExplorerToolbar.xaml
    /// </summary>
    public partial class LibraryExplorerToolbar : UserControl
    {
        public LibraryExplorerToolbar(ILibraryExplorerToolboxViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
