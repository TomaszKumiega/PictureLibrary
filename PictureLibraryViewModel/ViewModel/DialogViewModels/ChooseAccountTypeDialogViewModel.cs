using System.Collections.Generic;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class ChooseAccountTypeDialogViewModel : IChooseAccountTypeDialogViewModel
    {
        public IEnumerable<string> AccountTypes { get; }

        public string SelectedAccountType { get; set; }

        public ChooseAccountTypeDialogViewModel()
        {
            AccountTypes = new List<string>()
            {
                "Google Drive",
                "Picture Library API",
            };
        }
    }
}
