using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public interface ILibraryExplorerViewModel : IExplorerViewModel
    {
        Task LoadCurrentlyShownElements(IEnumerable<Tag> tags);
        Task AddTagAsync(Tag tag);
    }
}
