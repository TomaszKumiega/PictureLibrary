using Autofac;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryWPF.CustomControls;
using PictureLibraryWPF;
using PictureLibraryWPF.Clipboard;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryModel.Repositories.LibraryRepositories;

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

            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<WPFClipboard>().As<IClipboardService>();
            builder.RegisterType<ExplorerHistory>().As<IExplorerHistory>();
            builder.RegisterType<MainWindowControlsFactory>().As<IMainWindowControlsFactory>();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>();
            builder.RegisterType<FileExplorerViewModelFactory>().As<IFileExplorerViewModelFactory>();
            builder.RegisterType<LibraryRepositoriesFactory>().As<ILibraryRepositoriesFactory>();

            return builder.Build();
        }
    }
}
