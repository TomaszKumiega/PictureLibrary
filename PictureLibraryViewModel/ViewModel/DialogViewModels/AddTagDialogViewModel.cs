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
            if(!Regex.Match(Name, "^[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }

            return true;
        }

        public async Task AddAsync()
        {
            if (!IsNameValid()) return;

            var tag = new Tag();
            tag.Name = Name;
            tag.Description = Description;
            tag.Color = Color;
            tag.FullName = CommonVM.CurrentlyOpenedElement.Name + "\\" + tag.Name;
            tag.RemoteStorageInfoId = (CommonVM.CurrentlyOpenedElement as Tag).RemoteStorageInfoId;

            await CommonVM.AddTagAsync(tag);
        }
    }
}
