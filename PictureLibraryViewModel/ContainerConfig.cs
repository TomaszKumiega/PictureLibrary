using Autofac;
using PictureLibraryViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            var additionalBuilder = new ContainerBuilder(); // additional builder required to inject a container

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<CloseButtonCommand>().AsSelf();

            var container = builder.Build();

            additionalBuilder.RegisterInstance<IContainer>(container);
            additionalBuilder.Update(container);

            return container;
        }
    }
}
