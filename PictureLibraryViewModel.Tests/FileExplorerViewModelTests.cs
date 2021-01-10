using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using Xunit;

namespace PictureLibraryViewModel.Tests
{
    public class FileExplorerViewModelTests
    {
        [Fact]
        public void CopyFile_ShouldAssignSelectedFileToCopiedFile_WhenSelectedFileIsNotNull()
        {
            var viewModel = new FileExplorerViewModel(new WindowsFileSystemService(), new CommandFactory());

            var imageFile = new ImageFile();
            viewModel.SelectedFile = imageFile;
            viewModel.CopyFile();

            Assert.True(viewModel.CopiedFile.Equals(imageFile));
        }
    }
}
