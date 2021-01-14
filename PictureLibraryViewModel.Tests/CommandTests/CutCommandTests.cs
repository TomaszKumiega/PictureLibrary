using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryViewModel.Commands;

namespace PictureLibraryViewModel.Tests.CommandTests
{
    public class CutCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenSelectedFileIsInitialized()
        {
            var imageFileMock = new Mock<ImageFile>();

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.SelectedFile)
                .Returns(imageFileMock.Object);

            var cutCommand = new CutCommand(viewModelMock.Object);

            Assert.True(cutCommand.CanExecute(new object()));
        }
    }
}
