using Autofac;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModels;
using PictureLibraryWPF.CustomControls.Files;

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
            builder.RegisterType<FileSystemViewModel>().As<IFileSystemViewModel>();
            builder.RegisterType<FileSystemService>().As<IFileSystemService>();
            builder.RegisterType<FilesTree>().AsSelf();

            return builder.Build();
        }
    }
}
