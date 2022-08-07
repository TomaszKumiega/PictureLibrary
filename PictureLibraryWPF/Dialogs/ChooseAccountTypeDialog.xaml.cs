using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace PictureLibraryWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for ChooseAccountType.xaml
    /// </summary>
    public partial class ChooseAccountTypeDialog : Window
    {
        private readonly IChooseAccountTypeDialogViewModel _viewModel;
        private readonly Func<GoogleDriveLoginDialog> _googleDriveLoginDialogLocator;

        public ChooseAccountTypeDialog(
            IChooseAccountTypeDialogViewModel chooseAccountTypeDialogViewModel,
            Func<GoogleDriveLoginDialog> googleDriveLoginDialogLocator)
        {
            _viewModel = chooseAccountTypeDialogViewModel;
            DataContext = chooseAccountTypeDialogViewModel;

            _googleDriveLoginDialogLocator = googleDriveLoginDialogLocator;

            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedAccountType == "Google Drive")
            {
                var dialog = _googleDriveLoginDialogLocator();

                this.Close();

                dialog.ShowDialog();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBarRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
