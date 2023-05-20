using Autofac;
using PictureLibrary.FileSystem.API.Directories;
using PictureLibrary.FileSystem.API.Files;

namespace PictureLibrary.FileSystem.API.Configuration
{
    public class FileSystemConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PathFinder>().As<IPathFinder>().SingleInstance();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>().SingleInstance();
            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
        }
    }
}
