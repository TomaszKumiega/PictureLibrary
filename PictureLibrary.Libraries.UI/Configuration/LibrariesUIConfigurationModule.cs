using Autofac;
using PictureLibrary.Libraries.UI.MainPage;
using PictureLibrary.Libraries.UI.Pages;
using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.Configuration
{
    public class LibrariesUIConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LibrariesPageViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<LibrariesPage>().AsSelf().SingleInstance();
            builder.RegisterType<AddLibraryPage>().AsSelf();
            builder.RegisterType<AddLibraryPageViewModel>().AsSelf();
            builder.RegisterType<LibraryContentPage>().AsSelf();
            builder.RegisterType<LibraryContentPageViewModel>().AsSelf();
            builder.RegisterType<AddTagPage>().AsSelf();
            builder.RegisterType<AddTagPageViewModel>().AsSelf();
            builder.RegisterType<AllTagsPage>().AsSelf();
            builder.RegisterType<AllTagsPageViewModel>().AsSelf();
        }
    }
}
