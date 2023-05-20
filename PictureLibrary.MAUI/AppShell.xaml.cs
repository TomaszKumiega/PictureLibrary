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

            InitializeComponent();
        }
    }
}