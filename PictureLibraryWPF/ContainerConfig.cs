using Autofac;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryWPF;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.CustomControls;
using PictureLibraryWPF.Dialogs;

namespace PictureLibraryViewModel
{
    /// <summary>
    /// Configures autofac dependency injection container.
    /// </summary>
    public static class ContainerConfig
    {
        /// <summary>
        /// Creates dependency injection container.
        /// </summary>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ViewModelDIModule>();

            builder.RegisterType<WPFClipboard>().As<IClipboardService>();
            builder.RegisterType<DialogFactory>().As<IDialogFactory>();
            builder.RegisterType<LibraryExplorerControlsFactory>().As<ILibraryExplorerControlsFactory>();
            builder.RegisterType<FileExplorerControlsFactory>().As<IFileExplorerControlsFactory>();
            builder.RegisterType<MainWindow>().AsSelf();

            return builder.Build();
        }
    }
}
