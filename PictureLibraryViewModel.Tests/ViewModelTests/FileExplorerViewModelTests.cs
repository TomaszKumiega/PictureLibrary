using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using Xunit;
using Moq;
using PictureLibraryModel.Services.Clipboard;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;

namespace PictureLibraryViewModel.Tests
{
    public class FileExplorerViewModelTests
    {
        [Fact]
        public void Copy_ShouldAssignSelectedFileToCopiedFile_WhenSelectedFileIsNotNull()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var clipboardMock = new Mock<IClipboardService>();
            var imageFile = new ImageFile();
            var viewModelMock = new Mock<IFileExplorerViewModel>();

            clipboardMock.Setup(x => x.CutElements)
                .Returns(new ObservableCollection<IExplorableElement>());
            clipboardMock.Setup(x => x.CopiedElements)
                .Returns(new ObservableCollection<IExplorableElement>());

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, new CommandFactory(), clipboardMock.Object);

            var elementsList = new ObservableCollection<IExplorableElement>();
            elementsList.Add(imageFile);

            viewModel.SelectedElements = elementsList;
            viewModel.CopySelectedElements();

            Assert.Contains(imageFile, clipboardMock.Object.CopiedElements);
        }


        [Fact]
        public void Cut_ShouldAssignSelectedFileToCutFile_WhenSelectedFileIsNotNull()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var clipboardMock = new Mock<IClipboardService>();
            var imageFile = new ImageFile();

            clipboardMock.Setup(x => x.CutElements)
                .Returns(new ObservableCollection<IExplorableElement>());
            clipboardMock.Setup(x => x.CopiedElements)
                .Returns(new ObservableCollection<IExplorableElement>());

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, new CommandFactory(), clipboardMock.Object);

            var elementsList = new ObservableCollection<IExplorableElement>();
            elementsList.Add(imageFile);

            viewModel.SelectedElements = elementsList;
            viewModel.CutSelectedElements();

            Assert.Contains(imageFile, clipboardMock.Object.CutElements);
        }

        [Fact]
        public void Paste_ShouldCallCopyMethod_WhenCopiedFileIsInitialized()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var clipboardMock = new Mock<IClipboardService>();

            var imageFile =
                new ImageFile()
                {
                    Name = "testFile.jpg",
                    FullPath = "Tests\\Directory\\testFile.jpg"
                };

            var destination = "Tests\\Directory2\\";
            bool copyMethodWasCalled = false;

            var elementsList = new ObservableCollection<IExplorableElement>();
            elementsList.Add(imageFile);

            clipboardMock.Setup(x => x.CutElements)
                .Returns(new ObservableCollection<IExplorableElement>());
            clipboardMock.Setup(x => x.CopiedElements)
                .Returns(elementsList);

            windowsFileSystemMock.Setup(x => x.Copy(imageFile, destination))
                .Callback(() => { copyMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, new CommandFactory(), clipboardMock.Object);

            viewModel.CurrentDirectoryPath = destination;

            viewModel.PasteSelectedElements();

            Assert.True(copyMethodWasCalled);
        }

        [Fact]
        public void Paste_ShouldCallMoveMethod_WhenCutFileIsInitialized()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var clipboardMock = new Mock<IClipboardService>();

            var imageFile =
                new ImageFile()
                {
                    Name = "testFile.jpg",
                    FullPath = "Tests\\Directory\\testFile.jpg"
                };

            var elementsList = new ObservableCollection<IExplorableElement>();
            elementsList.Add(imageFile);

            var destination = "Tests\\Directory2\\";
            bool moveMethodWasCalled = false;

            clipboardMock.Setup(x => x.CopiedElements)
                .Returns(new ObservableCollection<IExplorableElement>());
            clipboardMock.Setup(x => x.CutElements)
                .Returns(elementsList);

            windowsFileSystemMock.Setup(x => x.Move(imageFile, destination))
                .Callback(() => { moveMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, new CommandFactory(), clipboardMock.Object)
            {
                CurrentDirectoryPath = destination
            };

            viewModel.PasteSelectedElements();

            Assert.True(moveMethodWasCalled);

        }

        [Fact]
        public void CopyPath_ShouldAddFullPathOfSelectedFileToSystemClipboard_WhenSelectedFileIsNotNull()
        {
            var clipboardMock = new Mock<IClipboardService>();
            var windowsFileSystemMock = new Mock<FileSystemService>();

            clipboardMock.Setup(x => x.CutElements)
                .Returns(new ObservableCollection<IExplorableElement>());
            clipboardMock.Setup(x => x.CopiedElements)
                .Returns(new ObservableCollection<IExplorableElement>());


            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, new CommandFactory(), clipboardMock.Object);

            var imageFile =
                new ImageFile()
                {
                    Name = "testFile.jpg",
                    FullPath = "Tests\\Directory\\testFile.jpg"
                };

            viewModel.SelectedElements.Add(imageFile);

            viewModel.CopyPath();

            clipboardMock.VerifySet(x => x.SystemClipboard = imageFile.FullPath);
        }
    }
}

