using Autofac;
using PictureLibraryModel.Services.Clipboard;
using PictureLibraryWPF;
using PictureLibraryWPF.Clipboard;
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

            builder.RegisterModule<ViewModelDIModule>();

            builder.RegisterType<WPFClipboard>().As<IClipboardService>();
            builder.RegisterType<DialogFactory>().As<IDialogFactory>();
            builder.RegisterType<LibraryExplorerControlsFactory>().As<ILibraryExplorerControlsFactory>();
            builder.RegisterType<FileExplorerControlsFactory>().As<IFileExplorerControlsFactory>();
            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<AddLibraryDialog>().AsSelf();
            builder.Register<Func<AddLibraryDialog>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<AddLibraryDialog>(); };
            });

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

            builder.RegisterType<AddTagDialog>().AsSelf();
            builder.Register<Func<AddTagDialog>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<AddTagDialog>(); };
            });

            return builder.Build();
        }
    }
}
