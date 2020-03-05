using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel
{
    public class MainWindowViewModel 
    {
        public ICommand ButtonCommand { get; private set; }

        public MainWindowViewModel(ICommand ButtonCommand)
        {
            this.ButtonCommand = ButtonCommand;
        }
    }
}
