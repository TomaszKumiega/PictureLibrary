using Autofac;
using System;

namespace PictureLibraryModel.DI_Configuration
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterAsSelfWithFuncFactory<T>(this ContainerBuilder builder)
        {
            builder.RegisterType<T>().AsSelf();
            builder.Register<Func<T>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => cc.Resolve<T>();
            });

            return builder;
        }

        public static ContainerBuilder RegisterWithInterfaceAndFuncFactory<TType, TInterface>(this ContainerBuilder builder)
        {
            builder.RegisterType<TType>().As<TInterface>();
            builder.Register<Func<TInterface>>(context =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => cc.Resolve<TInterface>();
            });

            return builder;
        }
    }
}
