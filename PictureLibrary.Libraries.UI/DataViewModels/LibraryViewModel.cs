using CommunityToolkit.Mvvm.ComponentModel;
using PictureLibraryModel.Model;

namespace PictureLibrary.Libraries.UI.DataViewModels
{
    public class LibraryViewModel : ObservableObject
    {
        public LibraryViewModel(Library library)
        {
            Library = library;
        }

        public Library Library { get; }
        public ImageSource IconSource => ImageSource.FromStream(
            () =>
            {
                var bytes = Properties.Resources.LibraryIcon;
                return new MemoryStream(bytes);
            });
    }
}
