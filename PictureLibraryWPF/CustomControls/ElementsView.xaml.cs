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
using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for FilesView.xaml
    /// </summary>
    public partial class ElementsView : UserControl
    {
        public ElementsView(IExplorableElementsViewViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ItemMouseDoubleClick(object o, EventArgs args)
        {
            if(DataContext is IFilesViewViewModel)
            {
                (DataContext as IFilesViewViewModel).CommonViewModel.CurrentlyOpenedElement = ((o as ListViewItem).DataContext as IExplorableElement);
            }
        }

        private void FilesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataContext is IFilesViewViewModel)
            {
                (DataContext as IFilesViewViewModel).CommonViewModel.SelectedElements.Clear();

                foreach (var t in FilesList.SelectedItems)
                {
                    (DataContext as IFilesViewViewModel).CommonViewModel.SelectedElements.Add(t as IExplorableElement);
                }
            }
        }
    }
}
