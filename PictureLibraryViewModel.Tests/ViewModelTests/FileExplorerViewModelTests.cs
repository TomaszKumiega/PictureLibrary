using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Helpers;
using PictureLibraryViewModel.ViewModel.FileExplorerViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PictureLibraryViewModel.Tests.ViewModelTests
{
    public class FileExplorerViewModelTests
    {
        #region CurrentlyOpenedPath Tests
        [Fact]
        public void CurrentlyOpenedPathSet_ShouldAddPreviousValueToBackStack()
        {
            var defaultPath = "\\";
            var newPath = "Tests\\Folder2";
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            var backStack = new Stack<string>();
            var forwardStack = new Stack<string>();

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(backStack);
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(forwardStack);

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);

            viewModel.CurrentlyOpenedPath = newPath;

            Assert.True(backStack.Contains(defaultPath));
        }

        [Fact]
        public void CurrentlyOpenedPathSet_ShouldClearForwardStack()
        {
            var path = "\\";
            var forwardStackContent = "Tests\\Folder1\\";
            var backStack = new Stack<string>();
            var forwardStack = new Stack<string>();
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();

            forwardStack.Push(forwardStackContent);

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(backStack);
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(forwardStack);

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);
            viewModel.CurrentlyOpenedPath = path;

            Assert.True(forwardStack.Count == 0);
        }

        [Fact]
        public void CurrentlyOpenedPathSet_ShouldRaisePropertyChangedEvent()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            bool eventWasRaised = false;

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(new Stack<string>());
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(new Stack<string>());

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);

            viewModel.PropertyChanged += 
                (s, a) => 
                { 
                    if(a.PropertyName == "CurrentlyOpenedPath") eventWasRaised = true; 
                };

            viewModel.CurrentlyOpenedPath = "\\";

            Assert.True(eventWasRaised);
        }

        #endregion

        #region LoadCurrentlyShownElements Tests
        [Fact]
        public void LoadCurrentlyShownElements_ShouldAssignRootDirectoriesToCurrentlyShownElements_WhenCurrentlyOpenedPathIsRoot()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            
            var folder =
                new Folder()
                {
                    Name = "Folder1",
                    FullPath = "Tests\\Folder1\\"
                };

            var directoriesList = new List<Directory>()
            {
                folder
            };

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(new Stack<string>());
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(new Stack<string>());

            directoryServiceMock.Setup(x => x.GetRootDirectories())
                .Returns(directoriesList);

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);

            viewModel.CurrentlyOpenedPath = "\\";
            viewModel.LoadCurrentlyShownElements();

            Assert.Contains(folder, viewModel.CurrentlyShownElements);
        }

        [Fact]
        public void LoadCurrentlyShownElements_ShouldAssignDirectoryContentToCurrenltyShownElements_WhenDirectoryPathIsAssignedToCurrentlyOpenedPath()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            var path = "Tests\\Folder1\\";

            var folder =
                new Folder()
                {
                    Name = "Folder2",
                    FullPath = path + "Folder2"
                };

            var directoriesList = new List<Directory>()
            {
                folder
            };

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(new Stack<string>());
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(new Stack<string>());

            directoryServiceMock.Setup(x => x.GetDirectoryContent(path))
                .Returns(directoriesList);

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);

            viewModel.CurrentlyOpenedPath = path;
            viewModel.LoadCurrentlyShownElements();

            Assert.Contains(folder, viewModel.CurrentlyShownElements);
        }

        [Fact]
        public void LoadCurrentlyShownElements_ShouldRaisePropertyChangedEvent_ForCurrentlyShownElementsProperty()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            var eventWasRaised = false;

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);

            viewModel.PropertyChanged +=
                (s, a) =>
                {
                    if (a.PropertyName == "CurrentlyShownElements") eventWasRaised = true;
                };

            viewModel.LoadCurrentlyShownElements();

            Assert.True(eventWasRaised);
        }
        #endregion

        #region Back Tests
        [Fact]
        public void Back_ShouldPushCurrentlyOpenedPathValue_ToForwardStack()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            var forwardStack = new Stack<string>();
            var path = "Tests\\Folder1\\";

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(new Stack<string>());
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(forwardStack);

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);
            viewModel.CurrentlyOpenedPath = path;
            viewModel.Back();

            Assert.Contains(path, forwardStack);
        }

        [Fact]
        public void Back_ShouldAssignCurrentlyOpenedPath_ToTheValueFromBackStack()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            var path = "Tests\\Folder1\\";
            var backStack = new Stack<string>();
            backStack.Push(path);

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(backStack);
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(new Stack<string>());

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);
            viewModel.Back();

            Assert.True(viewModel.CurrentlyOpenedPath == path);
        }

        [Fact]
        public void Back_ShouldRaisePropertyChangedEvent_ForCurrentlyOpenedPathProperty()
        {
            var directoryServiceMock = new Mock<IDirectoryService>();
            var explorerHistoryMock = new Mock<IExplorerHistory>();
            var eventWasRaised = false;

            var backStack = new Stack<string>();
            backStack.Push("Tests\\Folder1");

            explorerHistoryMock.SetupGet(x => x.BackStack)
                .Returns(backStack);
            explorerHistoryMock.SetupGet(x => x.ForwardStack)
                .Returns(new Stack<string>());

            var viewModel = new FileExplorerViewModel(directoryServiceMock.Object, explorerHistoryMock.Object);

            viewModel.PropertyChanged +=
                (s, a) =>
                {
                    if (a.PropertyName == "CurrentlyOpenedPath") eventWasRaised = true;
                };

            viewModel.Back();

            Assert.True(eventWasRaised);
        }
        #endregion
    }
}
