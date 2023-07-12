using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.Pages;

public partial class LibraryContentPage : ContentPage
{
	public LibraryContentPage(LibraryContentPageViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{

	}
}