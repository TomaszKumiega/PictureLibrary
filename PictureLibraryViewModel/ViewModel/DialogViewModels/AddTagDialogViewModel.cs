using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddTagDialogViewModel : IAddTagDialogViewModel
    {
        private string _name;
        private ILibraryExplorerViewModel CommonVM { get; }

        public event InvalidInputEventHandler InvalidInput;
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;

        public ICommand AddTagCommand { get; }
        public string Color { get; set; }
        public string Description { get; set; }

        public string Name 
        {
            get => _name; 
            set
            {
                _name = value;
                if(!IsNameValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Name"));
            }
        }        

        public AddTagDialogViewModel(ILibraryExplorerViewModel commonVM ,ICommandFactory commandFactory)
        {
            CommonVM = commonVM;
            AddTagCommand = commandFactory.GetAddTagCommand(this);
        }

        private bool IsNameValid()
        {
            if(string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return true;
        }

        public async Task AddAsync()
        {
            if (!IsNameValid()) return;

            var tag = new Tag
            {
                Name = Name,
                Description = Description,
                Color = Color,
                Path = CommonVM.CurrentlyOpenedElement.Name + "\\" + Name,
                RemoteStorageInfoId = (CommonVM.CurrentlyOpenedElement as Library).RemoteStorageInfoId
            };

            var library = CommonVM.CurrentlyOpenedElement as Library;
            library.Tags.Add(tag);
            
            CommonVM.LoadCurrentlyShownElements(library.Tags);

            var dataSource = CommonVM.DataSourceCollection.GetDataSourceByRemoteStorageId(library.RemoteStorageInfoId);
            await Task.Run(() => dataSource.LibraryProvider.UpdateLibrary(library));

            CommonVM.InvokeTagsChanged();
            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
