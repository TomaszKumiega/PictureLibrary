using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryWPF.Dialogs;
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
        private IDialogFactory DialogFactory { get; }

        public FileExplorerToolbar(IFileExplorerToolboxViewModel viewModel, IDialogFactory dialogFactory)
        {
            DialogFactory = dialogFactory;
            DataContext = viewModel;
            InitializeComponent();
        }

        private async void AddToLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IFileExplorerToolboxViewModel;
            var images = new List<ImageFile>();

            foreach(var t in viewModel.CommonViewModel.SelectedElements)
            {
                if (t is ImageFile) images.Add(t as ImageFile);
            }

            var dialog = await DialogFactory.GetAddImagesDialog(images);
            dialog.Show();
        }
    }
}
