using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using Xunit;
using Moq;

namespace PictureLibraryViewModel.Tests
{
    public class FileExplorerViewModelTests
    {
        [Fact]
        public void CopyFile_ShouldAssignSelectedFileToCopiedFile_WhenSelectedFileIsNotNull()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object);

            var imageFile = new ImageFile();
            viewModel.SelectedFile = imageFile;
            viewModel.CopyFile();

            Assert.True(viewModel.CopiedFile.Equals(imageFile));
        }

        [Fact]
        public void PastFile_ShouldCallCopyMethod_WhenCopiedFileIsInitialized()
        {
            var windowsFileSystemMock = new Mock<WindowsFileSystemService>();
            var commandFactoryMock = new Mock<ICommandFactory>();
            var imageFile =
                new ImageFile()
                {
                    Name = "testFile.jpg",
                    FullPath = "Tests\\Directory\\testFile.jpg"
                };
            var destination = "Tests\\Directory2\\";
            bool copyMethodWasCalled = false;

            windowsFileSystemMock.Setup(x => x.Copy(imageFile, destination))
                .Callback(() => { copyMethodWasCalled = true; });

            var viewModel = new FileExplorerViewModel(windowsFileSystemMock.Object, commandFactoryMock.Object);

            viewModel.CopiedFile = imageFile;
            viewModel.CurrentDirectoryPath = destination;

            viewModel.PasteFile();

            Assert.True(copyMethodWasCalled);
        }
    }
}
