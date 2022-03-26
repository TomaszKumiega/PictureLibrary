namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FilesViewViewModel : IFilesViewViewModel
    {
        public IExplorerViewModel CommonViewModel { get; }

        public FilesViewViewModel(IFileExplorerViewModel viewModel)
        {
            CommonViewModel = viewModel;
        }
    }
}
