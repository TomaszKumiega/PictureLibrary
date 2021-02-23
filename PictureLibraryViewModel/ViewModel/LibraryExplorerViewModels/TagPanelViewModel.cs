using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class TagPanelViewModel : ITagPanelViewModel
    {
        public ILibraryExplorerViewModel CommonViewModel { get; }
        public Tag SelectedTag { get; set; }

        public TagPanelViewModel(ILibraryExplorerViewModel commonVM)
        {
            CommonViewModel = commonVM;
        }
    }
}
