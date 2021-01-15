using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PictureLibraryWPF.CustomControls
{
    public class MainWindowControlsFactory : IMainWindowControlsFactory
    {
        public IFileExplorerViewModel FileExplorerViewModel { get; }

        public MainWindowControlsFactory(IFileExplorerViewModel fileExplorerViewModel)
        {
            FileExplorerViewModel = fileExplorerViewModel;
        }

        public ElementsTree GetFileElementsTree()
        {
            return new ElementsTree(FileExplorerViewModel);
        }

        public ElementsView GetFileElementsView()
        {
            return new ElementsView(FileExplorerViewModel);
        }
    }
}
