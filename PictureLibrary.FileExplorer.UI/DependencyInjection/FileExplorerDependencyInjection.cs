namespace PictureLibrary.FileExplorer.UI
{
    public static class FileExplorerDependencyInjection
    {
        public static MauiAppBuilder RegisterFileExplorer(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<FileExplorerPageViewModel>();

            return builder;
        }
    }
}
