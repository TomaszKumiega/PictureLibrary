using PictureLibraryWPF.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for SettingsConnectedAccountsView.xaml
    /// </summary>
    public partial class SettingsConnectedAccountsView : UserControl
    {
        private readonly Func<ChooseAccountTypeDialog> _chooseAccountTypeDialogLocator;

        public SettingsConnectedAccountsView(
            Func<ChooseAccountTypeDialog> chooseAccountTypeDialogLocator)
        {
            _chooseAccountTypeDialogLocator = chooseAccountTypeDialogLocator;

            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = _chooseAccountTypeDialogLocator();

            dialog.ShowDialog();
        }
    }
}
