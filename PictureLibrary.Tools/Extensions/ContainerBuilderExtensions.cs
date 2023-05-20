using Autofac;

namespace PictureLibrary.Tools.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterTypeWithLocator<TType, TInterface>(this ContainerBuilder builder)
            where TType : class
            where TInterface : class
        {
            builder.RegisterType<TType>().As<TInterface>();
            builder.Register<Func<TInterface>>(ctx =>
            {
                var cc = ctx.Resolve<IComponentContext>();
                return () => cc.Resolve<TInterface>();
            });

            return builder;
        }
    }
}
