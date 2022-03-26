namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryViewViewModel : ILibraryViewViewModel
    {
        public IExplorerViewModel CommonViewModel { get; }

        public LibraryViewViewModel(ILibraryExplorerViewModel commonVM)
        {
            CommonViewModel = commonVM;
        }
    }
}
