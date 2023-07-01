using PictureLibrary.Libraries.UI.MainPage;
using PictureLibrary.Libraries.UI.Pages;

namespace PictureLibrary.MAUI
{
    public partial class AppShell : Shell
    {
        public LibrariesPage LibrariesPage { get; }

        public AppShell(LibrariesPage librariesPage)
        {
#if WINDOWS || MACCATALYST
            FlyoutBehavior = FlyoutBehavior.Locked;
#else
            FlyoutBehavior = FlyoutBehavior.Flyout;
#endif
            LibrariesPage = librariesPage;
            BindingContext = this;
            RegisterRoutes();
            InitializeComponent();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(LibraryContentPage), typeof(LibraryContentPage));
            Routing.RegisterRoute(nameof(AddLibraryPage), typeof(AddLibraryPage));
            Routing.RegisterRoute(nameof(AddTagPage), typeof(AddTagPage));
        }
    }
}