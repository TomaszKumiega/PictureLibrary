﻿using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryViewModel.ViewModel.LibraryExplorerViewModels
{
    public class LibraryExplorerViewModel : ILibraryExplorerViewModel
    {
        private IRepository<Library> _libraryRepository;
        private IExplorableElement _currentlyOpenedElement;

        public ObservableCollection<IExplorableElement> CurrentlyShownElements { get; set; }
        public ObservableCollection<IExplorableElement> SelectedElements { get; set; }
        public string InfoText { get; set; }
        public bool IsProcessing { get; set; }
        public IExplorableElement CurrentlyOpenedElement { get; set; }

        public IExplorerHistory ExplorerHistory { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LibraryExplorerViewModel(IRepository<Library> libraryRepository)
        {
            CurrentlyShownElements = new ObservableCollection<IExplorableElement>();
            SelectedElements = new ObservableCollection<IExplorableElement>();
            _libraryRepository = libraryRepository;
        }

        public async Task LoadCurrentlyShownElements()
        {
            if (CurrentlyShownElements == null) return;

            CurrentlyShownElements.Clear();

            if (CurrentlyOpenedElement == null)
            {
                var libraries = await _libraryRepository.GetAllAsync();
                foreach (IExplorableElement t in libraries)
                {
                    CurrentlyShownElements.Add(t);
                }
            }
            else if (CurrentlyOpenedElement is Library)
            {
                foreach (var t in (CurrentlyOpenedElement as Library).Tags)
                {
                    CurrentlyShownElements.Add(t);
                }
            }
            else if (CurrentlyOpenedElement is Tag)
            {
                if((CurrentlyOpenedElement as Tag).Origin == Origin.Local)
                {
                    foreach(var t in (CurrentlyOpenedElement as Tag).ParentLibrary.Images)
                    {
                        CurrentlyShownElements.Add(t);
                    }
                }

                //TODO: Add images from diffrent origins
            }
        }

        public void Back()
        {
            if (ExplorerHistory.BackStack.Count == 0) return;

            ExplorerHistory.ForwardStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.BackStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedElement"));
        }

        public void Forward()
        {
            if (ExplorerHistory.ForwardStack.Count == 0) return;

            ExplorerHistory.BackStack.Push(_currentlyOpenedElement);
            _currentlyOpenedElement = ExplorerHistory.ForwardStack.Pop();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentlyOpenedElement"));
        }
    }
}
