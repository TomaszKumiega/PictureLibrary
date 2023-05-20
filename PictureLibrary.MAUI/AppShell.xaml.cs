using PictureLibrary.Libraries.UI.MainPage;
using PictureLibrary.Libraries.UI.Pages;

namespace PictureLibrary.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
#if WINDOWS || MACCATALYST
            FlyoutBehavior = FlyoutBehavior.Locked;
#else
            FlyoutBehavior = FlyoutBehavior.Flyout;
#endif
            RegisterRoutes();
            InitializeComponent();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(LibrariesPage), typeof(LibrariesPage));
            Routing.RegisterRoute(nameof(LibraryContentPage), typeof(LibraryContentPage));
        }
    }
}