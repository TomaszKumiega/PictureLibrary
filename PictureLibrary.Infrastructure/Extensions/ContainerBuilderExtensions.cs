using Autofac;

namespace PictureLibrary.Infrastructure.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterWithFuncFactory<TImplementation, TInterface>(this ContainerBuilder containerBuilder)
            where TInterface : class
            where TImplementation : class
        {
            containerBuilder.RegisterType<TImplementation>().As<TInterface>();
            containerBuilder.Register<Func<TInterface>>(ctx =>
            {
                var cc = ctx.Resolve<IComponentContext>();
                return () => cc.Resolve<TInterface>();
            });

            return containerBuilder;
        }
    }
}
