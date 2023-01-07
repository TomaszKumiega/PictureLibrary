using System;
using System.Windows;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        private readonly Func<SettingsConnectedAccountsView> _settingsConnectedAccountsViewLocator;

        public SettingsPanel(
            Func<SettingsConnectedAccountsView> settingsConnectedAccountsViewLocator)
        {
            _settingsConnectedAccountsViewLocator = settingsConnectedAccountsViewLocator;
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SettingsConnectedAccountsView settingsConnectedAccountsView = _settingsConnectedAccountsViewLocator();
            MainPanel.Child = settingsConnectedAccountsView;
            settingsConnectedAccountsView.HorizontalAlignment = HorizontalAlignment.Stretch;
            settingsConnectedAccountsView.VerticalAlignment = VerticalAlignment.Stretch;
        }
    }
}
