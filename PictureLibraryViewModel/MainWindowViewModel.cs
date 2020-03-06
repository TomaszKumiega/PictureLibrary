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
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        public ICommand CloseButtonCommand { get; private set; }
        public ICommand MaximizeButtonCommand { get; private set; }

        private WindowState _windowState;
        public MainWindowViewModel(ICommandFactory factory)
        {
            this.CloseButtonCommand = factory.GetCloseButtonCommand(this);
            this.MaximizeButtonCommand = factory.GetMaximizeButtonCommand(this);
        }

        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                OnPropertyChanged("WindowState");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Close()
        {
            Application.Current.Shutdown();
        }

        public void Maximize()
        {
            
            WindowState = (WindowState != WindowState.Maximized ? WindowState.Maximized : WindowState.Normal);
        }
    }
}
