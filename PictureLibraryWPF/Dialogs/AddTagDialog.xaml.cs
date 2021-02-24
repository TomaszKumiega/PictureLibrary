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
    /// Interaction logic for AddTagDialog.xaml
    /// </summary>
    public partial class AddTagDialog : Window
    {
        public AddTagDialog(IAddTagDialogViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
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
    }
}
