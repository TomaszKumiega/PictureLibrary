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
            builder.RegisterType<FileExplorerViewModel>().As<IFileExplorerViewModel>().SingleInstance();
            builder.RegisterType<FileExplorerToolbarViewModel>().As<IFileExplorerToolbarViewModel>();
            builder.RegisterType<FileTreeViewModel>().As<IFileTreeViewModel>();
            builder.RegisterType<FilesViewViewModel>().As<IFilesViewViewModel>();
        }
        private void RegisterLibraryExplorerViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<LibraryExplorerViewModel>().As<ILibraryExplorerViewModel>().SingleInstance();
            builder.RegisterType<LibraryTreeViewModel>().As<ILibraryTreeViewModel>();
            builder.RegisterType<LibraryExplorerToolboxViewModel>().As<ILibraryExplorerToolboxViewModel>();
            builder.RegisterType<LibraryViewViewModel>().As<ILibraryViewViewModel>();

            builder.RegisterType<TagPanelViewModel>().As<ITagPanelViewModel>();
        }
        private void RegisterDialogViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<AddLibraryDialogViewModel>().As<IAddLibraryDialogViewModel>();
            builder.Register<Func<IAddLibraryDialogViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IAddLibraryDialogViewModel>(); };
            });

            builder.RegisterType<AddImagesDialogViewModel>().As<IAddImagesDialogViewModel>();
            builder.Register<Func<IAddImagesDialogViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IAddImagesDialogViewModel>(); };
            });

            builder.RegisterType<AddTagDialogViewModel>().As<IAddTagDialogViewModel>();
            builder.Register<Func<IAddTagDialogViewModel>>((context) =>
            {
                var cc = context.Resolve<IComponentContext>();
                return () => { return cc.Resolve<IAddTagDialogViewModel>(); };
            });

            builder.RegisterType<ChooseAccountTypeDialogViewModel>().As<IChooseAccountTypeDialogViewModel>();
            builder.RegisterType<GoogleDriveLoginDialogViewModel>().As<IGoogleDriveLoginDialogViewModel>();
        }
        private void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<ExplorerHistory>().As<IExplorerHistory>();
        }
    }
}
