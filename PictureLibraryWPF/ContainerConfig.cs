using Autofac;
using PictureLibraryViewModel.Commands;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureLibraryViewModel.ViewModels;
using PictureLibraryModel.Services;
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
