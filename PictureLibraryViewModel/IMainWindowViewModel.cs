using System.Windows.Input;

namespace PictureLibraryViewModel
{
    public interface IMainWindowViewModel
    {
        ICommand CloseButtonCommand { get; }

        void Close();
    }
}