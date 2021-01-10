using Autofac;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryWPF.CustomControls.Files;
using PictureLibraryModel.Model;
namespace PictureLibraryViewModel
{
    /// <summary>
    /// Configurates autofac dependency injection container.
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
            builder.RegisterType<FilesTree>().AsSelf();
            builder.RegisterType<FilesView>().AsSelf();
            builder.RegisterType<WindowsFileSystemService>().As<FileSystemService>();
            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();


            return builder.Build();
        }
    }
}
