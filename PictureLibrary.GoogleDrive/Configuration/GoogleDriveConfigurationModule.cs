using Autofac;
using PictureLibrary.Infrastructure.Extensions;
using PictureLibraryModel.Services.CredentialsProvider;
using PictureLibraryModel.Services.GoogleDriveAPIClient;

namespace PictureLibrary.GoogleDrive.Configuration
{
    public class GoogleDriveConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterWithFuncFactory<QueryBuilder, IQueryBuilder>();
            builder.RegisterType<CredentialsProvider>().As<ICredentialsProvider>().SingleInstance();
            builder.RegisterType<GoogleDriveApiClient>().As<IGoogleDriveApiClient>().SingleInstance();
        }
    }
}
