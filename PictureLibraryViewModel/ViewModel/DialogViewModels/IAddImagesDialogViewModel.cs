using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddImagesDialogViewModel
    {
        List<Library> Libraries { get; }
        Library SelectedLibrary { get; set; }
        ICommand AddImagesCommand { get; }

        Task AddAsync();
    }
}
