using PictureLibraryModel.DataProviders.Repositories;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Attributes;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddTagDialogViewModel : IAddTagDialogViewModel
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly ILibraryExplorerViewModel _commonViewModel;

        #region Public properties
        public string Color { get; set; }
        public string Description { get; set; }

        private string _name;
        public string Name 
        {
            get => _name; 
            set
            {
                _name = value;
                if(!IsNameValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs(nameof(Name)));
            }
        }
        #endregion

        #region Commands
        [Command]
        public ICommand AddTagCommand { get; set; }
        #endregion

        #region Events
        public event InvalidInputEventHandler InvalidInput;
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;
        #endregion

        public AddTagDialogViewModel(
            ILibraryRepository libraryRepository,
            ILibraryExplorerViewModel commonVM, 
            ICommandCreator commandCreator)
        {
            _libraryRepository = libraryRepository;
            _commonViewModel = commonVM;

            commandCreator.InitializeCommands(this);
        }

        #region Command methods
        [Execute(nameof(AddTagCommand))]  
        private async void ExecuteAddTagCommand(object parameter)
        {
            await AddAsync();
        }
        #endregion

        #region Validation
        private bool IsNameValid()
        {
            if(string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return true;
        }
        #endregion 

        public async Task AddAsync()
        {
            if (!IsNameValid()) return;

            var tag = new Tag
            {
                Name = Name,
                Description = Description,
                Color = Color,
                Path = _commonViewModel.CurrentlyOpenedElement.Name + Path.DirectorySeparatorChar + Name,
            };

            var library = _commonViewModel.CurrentlyOpenedElement as Library;
            library.Tags.Add(tag);
            
            _commonViewModel.LoadCurrentlyShownElements(library.Tags);

            await Task.Run(() => _libraryRepository.UpdateLibrary(library));

            _commonViewModel.InvokeTagsChanged();
            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
