using Autofac;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryViewModel.Commands;
using PictureLibraryWPF;
using PictureLibraryWPF.Clipboard;
using PictureLibraryWPF.Commands;
using PictureLibraryWPF.CustomControls;
using PictureLibraryWPF.Dialogs;
using System;

namespace PictureLibraryViewModel
{
    /// <summary>
    /// Configures autofac dependency injection container.
    /// </summary>
    public static class ContainerConfig
    {
        /// <summary>
        /// Creates dependency injection container.
        /// </summary>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            RegisterModules(builder);
            RegisterMainWindow(builder);
            RegisterServices(builder);
            RegisterUIElements(builder);
            RegisterDialogs(builder);
            RegisterCommands(builder);


            return builder.Build();
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ViewModelDIModule>();
        }
        private static void RegisterMainWindow(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf();
        }
        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<WPFClipboard>().As<IClipboardService>();
        }
        private static void RegisterUIElements(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryExplorerToolbar>().AsSelf();
            builder.Register<Func<LibraryExplorerToolbar>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LibraryExplorerToolbar>(); };
            });

            builder.RegisterType<TagPanel>().AsSelf();
            builder.Register<Func<TagPanel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<TagPanel>(); };
            });

            builder.RegisterType<FileExplorerToolbar>().AsSelf();
            builder.Register<Func<FileExplorerToolbar>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<FileExplorerToolbar>(); };
            });

            builder.RegisterType<FileTree>().AsSelf();
            builder.Register<Func<FileTree>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<FileTree>(); };
            });

            builder.RegisterType<LibraryTree>().AsSelf();
            builder.Register<Func<LibraryTree>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LibraryTree>(); };
            });

            builder.RegisterType<FilesView>().AsSelf();
            builder.Register<Func<FilesView>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<FilesView>(); };
            });

            builder.RegisterType<LibraryView>().AsSelf();
            builder.Register<Func<LibraryView>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<LibraryView>(); };
            });

        }
        private static void RegisterDialogs(ContainerBuilder builder)
        {
            builder.RegisterType<AddTagDialog>().AsSelf();
            builder.Register<Func<AddTagDialog>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<AddTagDialog>(); };
            });

            builder.RegisterType<AddLibraryDialog>().AsSelf();
            builder.Register<Func<AddLibraryDialog>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<AddLibraryDialog>(); };
            });

            builder.RegisterType<AddImagesDialog>().AsSelf();
            builder.Register<Func<AddImagesDialog>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<AddImagesDialog>(); };
            });
        }
        private static void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterType<Command>().As<IPictureLibraryCommand>();
            builder.Register<Func<IPictureLibraryCommand>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IPictureLibraryCommand>(); };
            });
        }
    }
}
