﻿using Autofac;
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
            builder.RegisterType<FileSystemViewModel>().As<IFileSystemViewModel>().SingleInstance();
            builder.RegisterType<FileSystemService>().As<IFileSystemService>();
            builder.RegisterType<LibraryFileService>().As<ILibraryFileService>();
            builder.RegisterType<FilesTree>().AsSelf();
            builder.RegisterType<FilesView>().AsSelf();

            return builder.Build();
        }
    }
}
