using Autofac;
using PictureLibraryModel;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;

namespace PictureLibraryViewModel
{
    public class ViewModelDIModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataModelDIModule>();

            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<ExplorerHistory>().As<IExplorerHistory>();

            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>();
            builder.RegisterType<FileExplorerViewModelFactory>().As<IFileExplorerViewModelFactory>();

            builder.RegisterType<LibraryExplorerViewModel>().As<ILibraryExplorerViewModel>().SingleInstance();
            builder.RegisterType<DialogViewModelFactory>().As<IDialogViewModelFactory>();
            builder.RegisterType<LibraryExplorerViewModelFactory>().As<ILibraryExplorerViewModelFactory>();

            builder.RegisterType<TagPanelViewModel>().As<ITagPanelViewModel>();
            builder.RegisterType<AddLibraryDialogViewModel>().As<IAddLibraryDialogViewModel>();

            builder.Register<Func<IAddLibraryDialogViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IAddLibraryDialogViewModel>(); };
            });

            builder.RegisterType<LibraryExplorerToolboxViewModel>().As<ILibraryExplorerToolboxViewModel>();

            builder.Register<Func<ILibraryExplorerToolboxViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<ILibraryExplorerToolboxViewModel>(); };
            });
        }
    }
}
