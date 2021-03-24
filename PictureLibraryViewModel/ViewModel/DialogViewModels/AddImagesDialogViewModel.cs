﻿using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PictureLibraryViewModel.ViewModel.DialogViewModels
{
    public class AddImagesDialogViewModel : IAddImagesDialogViewModel, INotifyPropertyChanged
    {
        private ILibraryExplorerViewModel CommonViewModel { get; }
        private List<ImageFile> SelectedImages { get; }
        private IRepository<Library> LibraryRepository { get; }

        private Library _selectedLibrary;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddImagesCommand { get; }
        public List<Library> Libraries { get; private set; }
        public Library SelectedLibrary 
        {
            get => _selectedLibrary; 
            set
            {
                _selectedLibrary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedLibrary"));
            }
        }

        public AddImagesDialogViewModel(ILibraryExplorerViewModel commonVM, IRepository<Library> libraryRepository, List<ImageFile> selectedImages, ICommandFactory commandFactory)
        {
            LibraryRepository = libraryRepository;
            CommonViewModel = commonVM;
            SelectedImages = selectedImages;
            AddImagesCommand = commandFactory.GetAddImagesCommand(this);

            PropertyChanged += OnSelectedLibraryChanged;
        }

        private void OnSelectedLibraryChanged(object sender, PropertyChangedEventArgs args)
        {
            (AddImagesCommand as AddImagesCommand).RaiseCanExecuteChanged(this, EventArgs.Empty);
        }

        public async Task Initialize()
        {
            Libraries = (await LibraryRepository.GetAllAsync()).ToList();
        }

        public async Task AddAsync()
        {
            // Add images to library folder

            SelectedLibrary.Images.AddRange(SelectedImages);
            await LibraryRepository.UpdateAsync(SelectedLibrary);
            await CommonViewModel.LoadCurrentlyShownElementsAsync();
        }
    }
}
