using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryWPF.Helpers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace PictureLibraryWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for AddLibraryDialog.xaml
    /// </summary>
    public partial class AddLibraryDialog : Window
    {
        public AddLibraryDialog(IAddLibraryDialogViewModel viewModel)
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PickFolderPathButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as IAddLibraryDialogViewModel;
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog(this.GetIWin32Window());
            
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                PathTextBlock.Text = dialog.SelectedPath;
                viewModel.Directory = dialog.SelectedPath;
            }
        }

        private void TitleBarRectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
