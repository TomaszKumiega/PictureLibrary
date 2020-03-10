using System.Windows;
using System.Windows.Input;

namespace PictureLibraryViewModel
{
    /// <summary>
    /// Provides logic for main window.
    /// </summary>
    public interface IMainWindowViewModel
    {
        /// <summary>
        /// Delegates action from close button.
        /// </summary>
        ICommand CloseButtonCommand { get; }
        /// <summary>
        /// Delegates action from maximize button.
        /// </summary>
        ICommand MaximizeButtonCommand { get; }

        /// <summary>
        /// <inheritdoc cref="Window.WindowState"/>
        /// </summary>
        WindowState WindowState { get; set; }

        /// <summary>
        /// Shuts down an application.
        /// </summary>
        void Close();
        /// <summary>
        /// Maximizes a window.
        /// </summary>
        void Maximize();
        /// <summary>
        /// Minimizes a window.
        /// </summary>
    }
}