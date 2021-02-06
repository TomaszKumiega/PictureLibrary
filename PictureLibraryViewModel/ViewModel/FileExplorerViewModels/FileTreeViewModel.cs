using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PictureLibraryViewModel.ViewModel.FileExplorerViewModels
{
    public class FileTreeViewModel : IFileTreeViewModel
    {
        private IExplorableElement _selectedNode;

        public IExplorerViewModel CommonViewModel { get; }
        public ObservableCollection<IExplorableElement> ExplorableElementsTree { get; }
        public IExplorableElement SelectedNode 
        { 
            get => _selectedNode; 
            set
            {
                _selectedNode = value;
                (CommonViewModel as IFileExplorerViewModel).CurrentDirectoryPath = _selectedNode.FullPath;
            }
        }

        public FileTreeViewModel(IFileExplorerViewModel viewModel)
        {
            ExplorableElementsTree = new ObservableCollection<IExplorableElement>();
            CommonViewModel = viewModel;
        }
    }
}
