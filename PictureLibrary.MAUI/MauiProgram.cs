using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PictureLibrary.AppSettings.Configuration;
using PictureLibrary.DataAccess.Configuration;
using PictureLibrary.FileSystem.API.Configuration;
using PictureLibrary.GoogleDrive.Configuration;
using PictureLibrary.Infrastructure.Configuration;
using PictureLibrary.Libraries.UI.Configuration;
using PictureLibrary.Tools.Configuration;
using PictureLibraryModel.Configuration;

namespace PictureLibrary.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.ConfigureContainer(new AutofacServiceProviderFactory(), autofacBuilder =>
            {
                autofacBuilder.RegisterModule<LibrariesUIConfigurationModule>();
                autofacBuilder.RegisterModule<ToolsConfigurationModule>();
                autofacBuilder.RegisterModule<DataAccessConfigurationModule>();
                autofacBuilder.RegisterModule<SettingsConfigurationModule>();
                autofacBuilder.RegisterModule<FileSystemConfigurationModule>();
                autofacBuilder.RegisterModule<GoogleDriveConfigurationModule>();
                autofacBuilder.RegisterModule<InfrastructureConfigurationModule>();
                autofacBuilder.RegisterModule<ModelConfigurationModule>();

                autofacBuilder.RegisterType<AppShell>().AsSelf().SingleInstance();
            });


            return builder.Build();
        }
    }
}