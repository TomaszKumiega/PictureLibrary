using System.Windows;
using System.Windows.Input;

namespace PictureLibraryViewModel
{
    public interface IMainWindowViewModel
    {
        ICommand CloseButtonCommand { get; }
        ICommand MaximizeButtonCommand { get; }
        ICommand MinimizeButtonCommand { get; }
        WindowState WindowState { get; set; }

        void Close();
        void Maximize();
        void Minimize();
    }
}