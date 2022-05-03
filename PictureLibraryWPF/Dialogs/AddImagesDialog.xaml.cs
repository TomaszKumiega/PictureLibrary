using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PictureLibraryWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for AddImagesDialog.xaml
    /// </summary>
    public partial class AddImagesDialog : Window
    {
        private readonly IAddImagesDialogViewModel _viewModel;

        public AddImagesDialog(IAddImagesDialogViewModel viewModel)
        {
            DataContext = viewModel;
            _viewModel = viewModel;
            viewModel.ProcessingStatusChanged += OnProcessingStatusChanged;

            InitializeComponent();
        }

        private void OnProcessingStatusChanged(object sender, ProcessingStatusChangedEventArgs args)
        {
            if (args.Status == ProcessingStatus.Finished)
                this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleBarRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        new public void ShowDialog()
        {
            throw new NotImplementedException("Use ShowDialog(IEnumerable<ImageFile> selectedImages) instead");
        }

        public void ShowDialog(IEnumerable<ImageFile> selectedImages)
        {
            _viewModel.SelectedImages = selectedImages;

            base.ShowDialog();
        }
    }
}
