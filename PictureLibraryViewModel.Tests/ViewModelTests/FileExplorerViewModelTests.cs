using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using Xunit;
using Moq;
using PictureLibraryModel.Services.Clipboard;

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

            viewModel.SelectedFile = imageFile;
            viewModel.Copy();

            clipboardMock.VerifySet(x => x.CopiedElement = imageFile);
        }


        [Fact]
        public void Cut_ShouldAssignSelectedFileToCutFile_WhenSelectedFileIsNotNull()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var clipboardMock = new Mock<IClipboardService>();
            var imageFile = new ImageFile();

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            viewModel.SelectedFile = imageFile;
            viewModel.Cut();

            clipboardMock.VerifySet(x => x.CutElement = imageFile);
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

            clipboardMock.Setup(x => x.CopiedElement)
                .Returns(imageFile);

            windowsFileSystemMock.Setup(x => x.Copy(imageFile, destination))
                .Callback(() => { copyMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            viewModel.CurrentDirectoryPath = destination;

            viewModel.Paste();

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

            var destination = "Tests\\Directory2\\";
            bool moveMethodWasCalled = false;

            clipboardMock.Setup(x => x.CutElement)
                .Returns(imageFile);

            windowsFileSystemMock.Setup(x => x.Move(imageFile, destination))
                .Callback(() => { moveMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object, clipboardMock.Object);

            viewModel.CurrentDirectoryPath = destination;

            viewModel.Paste();

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

            viewModel.SelectedFile = imageFile;

            viewModel.CopyPath();

            clipboardMock.VerifySet(x => x.SystemClipboard = imageFile.FullPath);
        }

    }
}
