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
    public class MainWindowViewModel 
    {
        public ICommand ButtonCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.ButtonCommand = new CloseButtonCommand(this);
        }

        public void Close()
        {
            Application.Current.Shutdown();
        }
    }
}
