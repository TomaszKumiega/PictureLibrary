using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddTagDialogViewModel
    {
        string Name { get; set; }
        string Color { get; set; }
        string Description { get; set; }
        ICommand AddTagCommand { get; }

        event InvalidInputEventHandler InvalidInput;

        Task AddAsync();
    }
}
