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
        private Func<AddLibraryDialog> AddLibraryDialogLocator { get; }

        public LibraryExplorerToolbar(ILibraryExplorerToolboxViewModel viewModel, IDialogFactory dialogFactory, Func<AddLibraryDialog> addLibraryDialogLocator)
        {
            DataContext = viewModel;
            DialogFactory = dialogFactory;
            AddLibraryDialogLocator = addLibraryDialogLocator;
            InitializeComponent();
        }

        private void AddLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = AddLibraryDialogLocator();
            dialog.ShowDialog();
        }
    }
}
