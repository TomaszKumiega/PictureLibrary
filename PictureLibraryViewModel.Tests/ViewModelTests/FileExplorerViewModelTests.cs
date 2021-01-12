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
    }
}
