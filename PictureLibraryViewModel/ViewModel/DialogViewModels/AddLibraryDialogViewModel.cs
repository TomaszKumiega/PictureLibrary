﻿using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.Events;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    //TODO: REFACTOR
    public class AddLibraryDialogViewModel : IAddLibraryDialogViewModel
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Directory { get; set; }
        public string SelectedStorage { get; set; }
        public List<string> Storages { get; }
        public ICommand AddLibraryCommand { get; }

        public event InvalidInputEventHandler InvalidInput;
        public event ProcessingStatusChangedEventHandler ProcessingStatusChanged;

        public AddLibraryDialogViewModel(ILibraryExplorerViewModel commonVM, ICommandFactory commandFactory)
        {
            CommonViewModel = commonVM;
            AddLibraryCommand = commandFactory.GetAddLibraryCommand(this);
        }

        private bool IsNameValid()
        {
            if (!Regex.Match(Name, "^[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }

            return true;
        }

        private bool IsOriginValid()
        {
            return false;
        }

        private bool IsDirectoryValid()
        {
            if (Directory == null) return false;
            else return true;
        }

        public async Task AddAsync()
        {
            if(!IsNameValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Name"));
            if (!IsOriginValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Origin"));

            var library = new Library();

            //switch(SelectedOrigin)
            //{
            //    case Origin.Local:
            //        {
            //            library.Name = Name;
            //            if (Description == null) library.Description = "";
            //            else library.Description = Description;
            //            library.Origin = SelectedOrigin.Value;
            //            if (!IsDirectoryValid()) InvalidInput?.Invoke(this, new InvalidInputEventArgs("Directory"));
            //            if(!Directory.EndsWith("\\"))
            //            {
            //                Directory += "\\";
            //            }
            //            if (SelectedOrigin == PictureLibraryModel.Model.Origin.Local) library.FullName = Directory + library.Name + ".plib";
            //        }
            //        break;
            //}

            //try
            //{
            //    await LibraryRepository.AddAsync(library);
                
            //}
            //catch
            //{
            //    ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Failed));
            //    return;
            //}


            await CommonViewModel.LoadCurrentlyShownElementsAsync();
            ProcessingStatusChanged?.Invoke(this, new ProcessingStatusChangedEventArgs(ProcessingStatus.Finished));
        }
    }
}
