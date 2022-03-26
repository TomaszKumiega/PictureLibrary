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
    public class ViewModelDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterModules(builder);
            RegisterCommands(builder);
            RegisterFileExplorerViewModels(builder);
            RegisterLibraryExplorerViewModels(builder);
            RegisterDialogViewModels(builder);
            RegisterDependencies(builder);
        }

        private void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterModule<DataModelDIModule>();
        }
        private void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterType<CommandCreator>().As<ICommandCreator>().SingleInstance();
        }
        private void RegisterFileExplorerViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>();
            builder.RegisterType<FileExplorerViewModelFactory>().As<IFileExplorerViewModelFactory>();
        }
        private void RegisterLibraryExplorerViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryExplorerViewModel>().As<ILibraryExplorerViewModel>().SingleInstance();
            builder.RegisterType<LibraryExplorerViewModelFactory>().As<ILibraryExplorerViewModelFactory>();
            builder.RegisterType<TagPanelViewModel>().As<ITagPanelViewModel>();

            builder.RegisterType<LibraryExplorerToolboxViewModel>().As<ILibraryExplorerToolboxViewModel>();

            builder.Register<Func<ILibraryExplorerToolboxViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<ILibraryExplorerToolboxViewModel>(); };
            });
        }
        private void RegisterDialogViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<DialogViewModelFactory>().As<IDialogViewModelFactory>();
            builder.RegisterType<AddTagDialogViewModel>().As<IAddTagDialogViewModel>();

            builder.RegisterType<AddLibraryDialogViewModel>().As<IAddLibraryDialogViewModel>();
            builder.Register<Func<IAddLibraryDialogViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IAddLibraryDialogViewModel>(); };
            });
        }
        private void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<ExplorerHistory>().As<IExplorerHistory>();
        }
    }
}
