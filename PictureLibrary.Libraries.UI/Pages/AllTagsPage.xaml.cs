using PictureLibrary.Libraries.UI.ViewModels;

namespace PictureLibrary.Libraries.UI.Pages;

public partial class AllTagsPage : ContentPage
{
	public AllTagsPage(AllTagsPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}