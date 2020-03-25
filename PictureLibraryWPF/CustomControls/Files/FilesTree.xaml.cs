using PictureLibraryViewModel.ViewModels;
using System.Windows.Controls;

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
