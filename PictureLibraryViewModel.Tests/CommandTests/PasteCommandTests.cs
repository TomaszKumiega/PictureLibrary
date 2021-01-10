using Moq;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PictureLibraryViewModel.Tests.CommandTests
{
    public class PasteCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenCopiedFileIsInitialized()
        {
            var imageFileMock = new Mock<ImageFile>();

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.CopiedFile)
                .Returns(imageFileMock.Object);

            var pasteCommand = new PasteCommand(viewModelMock.Object);

            Assert.True(pasteCommand.CanExecute(new object()));
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenCopiedFileIsNull()
        {
            ImageFile imageFile = null;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.CopiedFile)
                .Returns(imageFile);

            var pasteCommand = new PasteCommand(viewModelMock.Object);

            Assert.False(pasteCommand.CanExecute(new object()));
        }
    }
}
