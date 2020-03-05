using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PictureLibraryViewModel.Commands;

namespace PictureLibraryViewModel
{
    public class MainWindowViewModel : IMainWindowViewModel
    {
        public ICommand CloseButtonCommand { get; private set; }

        public MainWindowViewModel(ICommandFactory factory)
        {
            this.CloseButtonCommand = factory.GetCloseButtonCommand(this);
        }

        public void Close()
        {
            Application.Current.Shutdown();
        }
    }
}
