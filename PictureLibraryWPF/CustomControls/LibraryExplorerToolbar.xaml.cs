using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryWPF.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for LibraryExplorerToolbar.xaml
    /// </summary>
    public partial class LibraryExplorerToolbar : UserControl
    {
        private Func<AddLibraryDialog> AddLibraryDialogLocator { get; }

        public LibraryExplorerToolbar(ILibraryExplorerToolboxViewModel viewModel, Func<AddLibraryDialog> addLibraryDialogLocator)
        {
            DataContext = viewModel;
            AddLibraryDialogLocator = addLibraryDialogLocator;
            InitializeComponent();
        }

        //TODO: remove
        private void AddLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = AddLibraryDialogLocator();
            dialog.ShowDialog();
        }
    }
}
