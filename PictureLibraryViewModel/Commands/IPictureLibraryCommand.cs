using System;
using System.Windows.Input;

namespace PictureLibraryViewModel.Commands
{
    public interface IPictureLibraryCommand : ICommand
    {
        Func<object, bool> CanExecuteProp { get; set; }
        Action<object> ExecuteProp { get; set; }
    }
}
