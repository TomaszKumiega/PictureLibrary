using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Dialogs;
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
        private readonly IDialogFactory _dialogFactory;

        public FileExplorerToolbar(IFileExplorerToolboxViewModel viewModel, IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
            DataContext = viewModel;
            InitializeComponent();
        }

        //TODO: remove
        private async void AddToLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IFileExplorerToolboxViewModel;
            var images = new List<ImageFile>();

            foreach(var t in viewModel.CommonViewModel.SelectedElements)
            {
                if (t is ImageFile) images.Add(t as ImageFile);
            }

            var dialog = await _dialogFactory.GetAddImagesDialog(images);
            dialog.ShowDialog();
        }
    }
}
