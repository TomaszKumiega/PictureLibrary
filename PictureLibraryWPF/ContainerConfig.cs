using Autofac;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using PictureLibraryWPF;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.Commands;
using PictureLibraryWPF.CustomControls;
using PictureLibraryWPF.Dialogs;
using System.Windows.Controls;

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

            RegisterModules(builder);
            RegisterMainWindow(builder);
            RegisterServices(builder);
            RegisterUIElements(builder);
            RegisterDialogs(builder);
            RegisterCommands(builder);

            return builder.Build();
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ViewModelDIModule>();
        }

        private static void RegisterMainWindow(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<WPFClipboard>().As<IClipboardService>();
        }

        private static void RegisterUIElements(ContainerBuilder builder)
        {
            builder.RegisterAsSelfWithFuncFactory<LibraryExplorerToolbar>();
            builder.RegisterAsSelfWithFuncFactory<FileExplorerToolbar>();
            builder.RegisterAsSelfWithFuncFactory<FileTree>();
            builder.RegisterAsSelfWithFuncFactory<LibraryTree>();
            builder.RegisterAsSelfWithFuncFactory<FilesView>();
            builder.RegisterAsSelfWithFuncFactory<LibraryView>();
            builder.RegisterAsSelfWithFuncFactory<SettingsConnectedAccountsView>();
            builder.RegisterAsSelfWithFuncFactory<SettingsPanel>();

            builder.RegisterType<TagPanel>().AsSelf();
            builder.RegisterType<GridSplitter>().AsSelf();
        }

        private static void RegisterDialogs(ContainerBuilder builder)
        {
            builder.RegisterAsSelfWithFuncFactory<AddTagDialog>();
            builder.RegisterAsSelfWithFuncFactory<AddLibraryDialog>();
            builder.RegisterAsSelfWithFuncFactory<AddImagesDialog>();
            builder.RegisterAsSelfWithFuncFactory<ChooseAccountTypeDialog>();
            builder.RegisterAsSelfWithFuncFactory<GoogleDriveLoginDialog>();
        }

        private static void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterWithInterfaceAndFuncFactory<Command, IPictureLibraryCommand>();
        }
    }
}
