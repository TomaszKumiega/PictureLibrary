using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using Xunit;
using Moq;
using PictureLibraryModel.Services.Clipboard;
using System.Collections.Generic;

namespace PictureLibraryViewModel.Tests
{
    public class FileExplorerViewModelTests
    {
        [Fact]
        public void Copy_ShouldAssignSelectedFileToCopiedFile_WhenSelectedFileIsNotNull()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var clipboardMock = new Mock<IClipboardService>();
            var imageFile = new ImageFile();

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFile);

            viewModel.SelectedElements = elementsList;
            viewModel.CopySelectedElements();

            clipboardMock.VerifySet(x => x.CopiedElements = elementsList);
        }


        [Fact]
        public void Cut_ShouldAssignSelectedFileToCutFile_WhenSelectedFileIsNotNull()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var clipboardMock = new Mock<IClipboardService>();
            var imageFile = new ImageFile();

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFile);

            viewModel.SelectedElements = elementsList;
            viewModel.CutSelectedElements();

            clipboardMock.VerifySet(x => x.CutElements = elementsList);
        }

        [Fact]
        public void Paste_ShouldCallCopyMethod_WhenCopiedFileIsInitialized()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var clipboardMock = new Mock<IClipboardService>();

            var imageFile =
                new ImageFile()
                {
                    Name = "testFile.jpg",
                    FullPath = "Tests\\Directory\\testFile.jpg"
                };

            var destination = "Tests\\Directory2\\";
            bool copyMethodWasCalled = false;

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFile);

            clipboardMock.Setup(x => x.CopiedElements)
                .Returns(elementsList);

            windowsFileSystemMock.Setup(x => x.Copy(imageFile, destination))
                .Callback(() => { copyMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            viewModel.CurrentDirectoryPath = destination;

            viewModel.PasteSelectedElements();

            Assert.True(copyMethodWasCalled);
        }

        [Fact]
        public void Paste_ShouldCallMoveMethod_WhenCutFileIsInitialized()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var clipboardMock = new Mock<IClipboardService>();

            var imageFile =
                new ImageFile()
                {
                    Name = "testFile.jpg",
                    FullPath = "Tests\\Directory\\testFile.jpg"
                };

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFile);

            var destination = "Tests\\Directory2\\";
            bool moveMethodWasCalled = false;

            clipboardMock.Setup(x => x.CutElements)
                .Returns(elementsList);

            windowsFileSystemMock.Setup(x => x.Move(imageFile, destination))
                .Callback(() => { moveMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            viewModel.CurrentDirectoryPath = destination;

            viewModel.PasteSelectedElements();

            Assert.True(moveMethodWasCalled);

        }

        [Fact]
        public void CopyPath_ShouldAddFullPathOfSelectedFileToSystemClipboard_WhenSelectedFileIsNotNull()
        {
            var clipboardMock = new Mock<IClipboardService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var windowsFileSystemMock = new Mock<FileSystemService>();

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

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
