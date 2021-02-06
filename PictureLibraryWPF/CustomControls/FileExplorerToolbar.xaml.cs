using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
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
    /// Interaction logic for FileExplorerToolbar.xaml
    /// </summary>
    public partial class FileExplorerToolbar : UserControl
    {
        public FileExplorerToolbar(IFileExplorerToolboxViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
