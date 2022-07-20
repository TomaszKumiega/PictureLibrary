using PictureLibraryViewModel.ViewModel.DialogViewModels;
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
        public ChooseAccountTypeDialog(IChooseAccountTypeDialogViewModel chooseAccountTypeDialogViewModel)
        {
            _viewModel = chooseAccountTypeDialogViewModel;
            DataContext = chooseAccountTypeDialogViewModel;

            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // open add account dialog
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
