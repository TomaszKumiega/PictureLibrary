using Moq;
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

            viewModel.PropertyChanged += (s, a) => { eventWasRaised = true; };

            viewModel.CurrentlyOpenedPath = "\\";

            Assert.True(eventWasRaised);
        }

        #endregion
    }
}
