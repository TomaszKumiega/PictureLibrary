using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.MainPage;

public partial class LibrariesPage : ContentPage
{
	public LibrariesPage(LibrariesPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}