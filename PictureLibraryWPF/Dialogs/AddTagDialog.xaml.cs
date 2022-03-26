using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.Events;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var viewModel = DataContext as IAddTagDialogViewModel;
            var color = ColorPicker.SelectedColor.Value.ToString();
            viewModel.Color = color;
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
