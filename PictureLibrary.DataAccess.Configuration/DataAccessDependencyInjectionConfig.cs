using PictureLibrary.DataAccess.Builders;
using PictureLibrary.DataAccess.Queries;

namespace PictureLibrary.DataAccess.Configuration
{
    public static class DataAccessDependencyInjectionConfig
    {
        public static MauiAppBuilder RegisterDataAccess(this MauiAppBuilder builder) 
        {
            builder.Services
                .AddTransient<StorageQuery>()
                .AddTransient<IDataSourceBuilder, LocalDataSourceBuilder>(;

            return builder;
        }
    }
}