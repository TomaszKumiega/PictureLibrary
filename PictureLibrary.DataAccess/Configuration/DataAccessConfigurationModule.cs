using Autofac;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.LibraryService;
using PictureLibrary.Tools.Extensions;

namespace PictureLibrary.DataAccess.Configuration
{
    public class DataAccessConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataStoreInfoService>().As<IDataStoreInfoService>().SingleInstance();
            builder.RegisterTypeWithLocator<FileSystemLibraryService, IFileSystemLibraryService>();
            builder.RegisterTypeWithLocator<GoogleDriveLibraryService, IGoogleDriveLibraryService>();
            builder.RegisterType<LibrariesProvider>().As<ILibrariesProvider>().SingleInstance();
        }
    }
}
