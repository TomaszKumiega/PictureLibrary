using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.Events;
using System.Windows;
using System.Windows.Input;

namespace PictureLibraryWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for AddTagDialog.xaml
    /// </summary>
    public partial class AddTagDialog : Window
    {
        public AddTagDialog(IAddTagDialogViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

            viewModel.ProcessingStatusChanged += OnProcessingStatusChanged;
        }

        //TODO: remove methods
        private void OnProcessingStatusChanged(object sender, ProcessingStatusChangedEventArgs args)
        {
            if (args.Status == ProcessingStatus.Finished) this.Close();
        }

        //TODO: add color picker

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
