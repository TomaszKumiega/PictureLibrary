using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.MainPage;

public partial class LibrariesPage : ContentPage
{
	public LibrariesPage(LibrariesPageViewModel librariesPageViewModel)
	{
		BindingContext = librariesPageViewModel;
		InitializeComponent();
	}
}