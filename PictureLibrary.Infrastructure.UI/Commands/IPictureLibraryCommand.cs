using System.Windows.Input;

namespace PictureLibrary.Infrastructure.UI
{
    public interface IPictureLibraryCommand : ICommand
    {
        Func<object, bool> CanExecuteProp { get; set; }
        Action<object> ExecuteProp { get; set; }

        void RaiseCanExecuteChanged();
    }
}
