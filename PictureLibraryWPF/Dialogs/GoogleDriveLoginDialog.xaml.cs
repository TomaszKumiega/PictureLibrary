using PictureLibraryViewModel.ViewModel.DialogViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PictureLibraryWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for GoogleDriveLoginDialog.xaml
    /// </summary>
    public partial class GoogleDriveLoginDialog : Window
    {
        private readonly IGoogleDriveLoginDialogViewModel _viewModel;

        public GoogleDriveLoginDialog(IGoogleDriveLoginDialogViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = viewModel;

            viewModel.PropertyChanged += OnPropertyChanged;

            InitializeComponent();
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(IGoogleDriveLoginDialogViewModel.Authorized) && _viewModel.Authorized)
            {
                this.Close();
            }
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
