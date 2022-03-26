using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Dialogs;
using System;
using System.Collections.Generic;
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
            var selectedImages = new List<ImageFile>();

            foreach(var t in viewModel.CommonViewModel.SelectedElements)
            {
                if (t is ImageFile)
                {
                    selectedImages.Add((ImageFile)t);
                }
            }

            var dialog = _addImagesDialogLocator();
            dialog.ShowDialog(selectedImages);
        }
    }
}
