using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using PictureLibraryViewModel.Commands;

namespace PictureLibraryViewModel
{
    /// <summary>
    /// <inheritdoc cref="IMainWindowViewModel"/>
    /// </summary>
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private WindowState _windowState;

        public ICommand CloseButtonCommand { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="factory"></param>
        public MainWindowViewModel(ICommandFactory factory)
        {
            this.CloseButtonCommand = factory.GetCloseButtonCommand(this);
            this.MaximizeButtonCommand = factory.GetMaximizeButtonCommand(this);
        }
      
        public WindowState WindowState
        {
            get 
            {
                return _windowState; 
            }

            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }

        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Close()
        {
            Application.Current.Shutdown();
        }
    }
}
