using Autofac;
using PictureLibraryModel;
using PictureLibraryModel.DI_Configuration;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;

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
            RegisterHelpers(builder);
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
            builder.RegisterWithInterfaceAndFuncFactory<AddLibraryDialogViewModel, IAddLibraryDialogViewModel>();
            builder.RegisterWithInterfaceAndFuncFactory<AddImagesDialogViewModel, IAddImagesDialogViewModel>();
            builder.RegisterWithInterfaceAndFuncFactory<AddTagDialogViewModel, IAddTagDialogViewModel>();

            builder.RegisterType<ChooseAccountTypeDialogViewModel>().As<IChooseAccountTypeDialogViewModel>();
            builder.RegisterType<GoogleDriveLoginDialogViewModel>().As<IGoogleDriveLoginDialogViewModel>();
        }
        private void RegisterHelpers(ContainerBuilder builder)
        {
            builder.RegisterType<ExplorerHistory>().As<IExplorerHistory>();
        }
    }
}
