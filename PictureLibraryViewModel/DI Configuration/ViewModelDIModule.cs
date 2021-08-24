using Autofac;
using PictureLibraryModel;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel.DialogViewModels;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;

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
        }
    }
}
