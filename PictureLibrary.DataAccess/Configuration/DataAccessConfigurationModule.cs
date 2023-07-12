using Autofac;
using PictureLibrary.DataAccess.DataStoreInfos;
using PictureLibrary.DataAccess.ImageFileService;
using PictureLibrary.DataAccess.LibraryService;
using PictureLibrary.DataAccess.TagService;
using PictureLibrary.Tools.Extensions;
using PictureLibraryModel.Model.DataStoreInfo;

namespace PictureLibrary.DataAccess.Configuration
{
    public class DataAccessConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataStoreInfoService>().As<IDataStoreInfoService>().SingleInstance();
            builder.RegisterTypeWithLocator<FileSystemLibraryService, IFileSystemLibraryService>();
            builder.RegisterTypeWithLocator<GoogleDriveLibraryService, IGoogleDriveLibraryService>();
            builder.Register<Func<IPictureLibraryAPILibraryService>>(ctx =>
            {
                return () => null;
            });

            builder.RegisterType<FileSystemLibraryService>().Keyed<ILibraryService>(DataStoreType.Local);
            builder.RegisterType<GoogleDriveLibraryService>().Keyed<ILibraryService>(DataStoreType.GoogleDrive);

            builder.RegisterType<FileSystemTagService>().Keyed<ITagService>(DataStoreType.Local);
            builder.RegisterType<GoogleDriveTagService>().Keyed<ITagService>(DataStoreType.GoogleDrive);

            builder.RegisterType<FileSystemImageFileService>().Keyed<IImageFileService>(DataStoreType.Local);
            builder.RegisterType<GoogleDriveImageFileService>().Keyed<IImageFileService>(DataStoreType.GoogleDrive);

            builder.RegisterType<LibrariesProvider>().As<ILibrariesProvider>().SingleInstance();

            builder.RegisterType<TagsProvider>().As<ITagsProvider>().SingleInstance();

            builder.RegisterType<ImageFilesProvider>().As<IImageFilesProvider>().SingleInstance();
        }
    }
}
