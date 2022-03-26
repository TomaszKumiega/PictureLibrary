using PictureLibraryModel.DataProviders;
using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public interface ILibraryExplorerViewModel : IExplorerViewModel
    {
        IDataSourceCollection DataSourceCollection { get; }
        void LoadCurrentlyShownElements(IEnumerable<Tag> tags);
        void InvokeTagsChanged();
        void RefreshView(object sender, EventArgs args);

        event EventHandler RefreshViewEvent;
    }
}
