using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteServer;
using PictureLibraryViewModel.ViewModel.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddLibraryDialogViewModel
    {
        string Name { get; set; }
        string Description { get; set; }
        string Directory { get; set; }
        Origin? SelectedOrigin { get; set; }
        List<Origin> Origins { get; }
        ICommand AddLibraryCommand { get; }

        event InvalidInputEventHandler InvalidInput;
        event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        Task AddAsync();
    }
}
