﻿using Autofac;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.Clipboard;

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
            builder.RegisterType<WindowsFileSystemService>().As<FileSystemService>();
            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<PCClipboardService>().As<IClipboardService>();


            return builder.Build();
        }
    }
}
