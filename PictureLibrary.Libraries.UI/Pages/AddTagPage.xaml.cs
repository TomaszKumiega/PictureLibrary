using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.Pages;

public partial class AddTagPage : ContentPage
{
	public AddTagPage(AddTagPageViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}
}