using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddImagesDialogViewModel : IAddImagesDialogViewModel
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private List<ImageFile> SelectedImages { get; }
        private IRepository<Library> LibraryRepository { get; }

        public List<Library> Libraries { get; }
        public Library SelectedLibrary { get; set; }

        public AddImagesDialogViewModel(ILibraryExplorerViewModel commonVM, IRepository<Library> libraryRepository, List<ImageFile> selectedImages)
        {
            LibraryRepository = libraryRepository;
            CommonViewModel = commonVM;
            SelectedImages = selectedImages;

            Libraries = Task.Run(() => LibraryRepository.GetAllAsync()).Result.ToList();
        }

        public async Task AddAsync()
        {
            // Add images to library folder

            SelectedLibrary.Images.AddRange(SelectedImages);
            await LibraryRepository.UpdateAsync(SelectedLibrary);
            await CommonViewModel.LoadCurrentlyShownElements();
        }
    }
}
