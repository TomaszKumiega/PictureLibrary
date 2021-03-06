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
using PublicResXFileCodeGenerator;
using System.Windows.Forms;
using PictureLibraryWPF.Helpers;

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
            LocationComboBox.DataContext = viewModel.Origins;
            InitializeLocationComboBox();
        }

        private void InitializeLocationComboBox()
        {
            var locationList = new List<string>();
            var viewModel = DataContext as IAddLibraryDialogViewModel;
            locationList.Add(Strings.ThisComputer);

            foreach(var t in viewModel.Origins)
            {
                switch(t)
                {
                    case PictureLibraryModel.Model.Origin.RemoteServer:
                        {
                            locationList.Add(Strings.RemoteServer);
                        }
                        break;
                }
            }
        }

        private void LocationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                viewModel.FullName = dialog.SelectedPath;
            }
        }
    }
}
