using PictureLibraryModel.Model;
using PictureLibraryModel.Model.RemoteServer;
using PictureLibraryViewModel.ViewModel.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IAddLibraryDialogViewModel
    {
        string Name { get; set; }
        string Description { get; set; }
        string FullName { get; set; }
        Origin? Origin { get; set; }
        List<Origin> Origins { get; }
        List<User> Owners { get; }

        event InvalidInputEventHandler InvalidInput;

        Task AddAsync();
    }
}
