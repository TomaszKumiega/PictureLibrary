using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.Pages;

public partial class AddLibraryPage : ContentPage
{
	public AddLibraryPage(AddLibraryPageViewModel addLibraryPageViewModel)
	{
		BindingContext = addLibraryPageViewModel;
		InitializeComponent();
	}
}