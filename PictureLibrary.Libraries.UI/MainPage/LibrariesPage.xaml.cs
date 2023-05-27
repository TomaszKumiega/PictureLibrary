using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.MainPage;

public partial class LibrariesPage : ContentPage
{
	public LibrariesPage(LibrariesPageViewModel librariesPageViewModel)
	{
		BindingContext = librariesPageViewModel;
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		((LibrariesPageViewModel)BindingContext).OpenLibraryCommand.Execute(null);
    }
}