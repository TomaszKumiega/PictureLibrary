namespace PictureLibrary.FileExplorer.UI.View;

public partial class FileExplorerPage : ContentPage
{
	public FileExplorerPage(FileExplorerPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}