using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
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
using System.Windows.Shapes;

namespace PictureLibraryWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for AddImagesDialog.xaml
    /// </summary>
    public partial class AddImagesDialog : Window
    {
        public AddImagesDialog(IAddImagesDialogViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void LibrariesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as IAddImagesDialogViewModel;
            var library = LibrariesListView.SelectedItem as Library;

            viewModel.SelectedLibrary = library;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBarRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
