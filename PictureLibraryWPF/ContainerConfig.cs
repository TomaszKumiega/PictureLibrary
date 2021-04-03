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
using PictureLibraryModel.Services.SettingsProvider;
using PictureLibraryModel.Model.ConnectedServices;
using PictureLibraryModel.Services.ConnectedServicesInfoProvider;
using PictureLibraryModel.Services.StringEncryption;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryWPF.Dialogs;
using PictureLibraryModel.Repositories;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.ImageProviderService;

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

            #region Model 
            builder.RegisterType<LibraryRepositoryStrategyFactory>().As<ILibraryRepositoryStrategyFactory>();
            builder.RegisterType<LibraryRepository>().As<IRepository<Library>>();
            builder.RegisterType<SettingsProviderService>().As<ISettingsProviderService>().SingleInstance();
            builder.RegisterType<ConnectedServicesInfoProviderService>().As<IConnectedServicesInfoProviderService>().SingleInstance();
            builder.RegisterType<StringEncryptionService>().As<IStringEncryptionService>();
            builder.RegisterType<ImageFileBuilder>().As<IImageFileBuilder>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();
            builder.RegisterType<FileService>().As<IFileService>();

            builder.RegisterType<ImageProviderContext>().As<IImageProviderContext>();
            builder.RegisterType<ImageProviderStrategyFactory>().As<IImageProviderStrategyFactory>();
            builder.RegisterType<ImageProviderService>().As<IImageProviderService>();

            builder.RegisterType<LibraryRepositoryContext>().As<ILibraryRepositoryContext>();
            #endregion

            #region ViewModel 
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<ExplorerHistory>().As<IExplorerHistory>();

            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>();
            builder.RegisterType<FileExplorerViewModelFactory>().As<IFileExplorerViewModelFactory>();

            builder.RegisterType<LibraryExplorerViewModel>().As<ILibraryExplorerViewModel>().SingleInstance();
            builder.RegisterType<DialogViewModelFactory>().As<IDialogViewModelFactory>();
            builder.RegisterType<LibraryExplorerViewModelFactory>().As<ILibraryExplorerViewModelFactory>();
            #endregion

            #region View
            builder.RegisterType<WPFClipboard>().As<IClipboardService>();
            builder.RegisterType<DialogFactory>().As<IDialogFactory>();
            builder.RegisterType<LibraryExplorerControlsFactory>().As<ILibraryExplorerControlsFactory>();
            builder.RegisterType<FileExplorerControlsFactory>().As<IFileExplorerControlsFactory>();
            builder.RegisterType<MainWindow>().AsSelf();
            #endregion

            return builder.Build();
        }
    }
}
