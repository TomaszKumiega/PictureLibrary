using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
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
    /// Interaction logic for LibraryExplorerToolbar.xaml
    /// </summary>
    public partial class LibraryExplorerToolbar : UserControl
    {
        private IDialogFactory DialogFactory { get; }

        public LibraryExplorerToolbar(ILibraryExplorerToolboxViewModel viewModel, IDialogFactory dialogFactory)
        {
            DataContext = viewModel;
            DialogFactory = dialogFactory;
            InitializeComponent();
        }

        private void AddLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = DialogFactory.GetAddLibraryDialog();
            dialog.Show();
        }
    }
}
