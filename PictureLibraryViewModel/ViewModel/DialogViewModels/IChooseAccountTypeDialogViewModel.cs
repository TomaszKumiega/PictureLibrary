using System.Collections.Generic;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public interface IChooseAccountTypeDialogViewModel
    {
        IEnumerable<string> AccountTypes { get; }
        string SelectedAccountType { get; set; }
    }
}
