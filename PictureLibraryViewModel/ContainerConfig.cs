using Autofac;
using PictureLibraryViewModel.Commands;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();

            return builder.Build();
        }
    }
}
