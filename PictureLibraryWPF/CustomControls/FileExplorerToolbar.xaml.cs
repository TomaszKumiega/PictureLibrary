using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Dialogs;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FileExplorerToolbar.xaml
    /// </summary>
    public partial class FileExplorerToolbar : UserControl
    {
        private readonly Func<AddImagesDialog> _addImagesDialogLocator;

        public FileExplorerToolbar(IFileExplorerToolbarViewModel viewModel, Func<AddImagesDialog> addImagesDialogLocator)
        {
            _addImagesDialogLocator = addImagesDialogLocator;

            DataContext = viewModel;
            InitializeComponent();
        }

        //TODO: remove
        private void AddToLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IFileExplorerToolbarViewModel;

            var dialog = _addImagesDialogLocator();
            dialog.ShowDialog(viewModel.CommonViewModel.SelectedElements.Where(x => x is ImageFile).Cast<ImageFile>());
        }
    }
}
