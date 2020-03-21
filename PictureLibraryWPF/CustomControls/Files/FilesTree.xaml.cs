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
using PictureLibraryModel.Model;
using PictureLibraryViewModel;
using PictureLibraryViewModel.ViewModels;

namespace PictureLibraryWPF.CustomControls.Files
{
    /// <summary>
    /// Interaction logic for FilesTree.xaml
    /// </summary>
    public partial class FilesTree : UserControl
    {
        public FilesTree(IFileSystemViewModel fileSystemViewModel)
        {
            InitializeComponent();
            DataContext = fileSystemViewModel;
        }
    }
}
