using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;

namespace PictureLibraryViewModel.Tests
{
    public class CopyCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenSelectedFileIsInitialized()
        {
            var imageFile = new Mock<ImageFile>();
            var viewModel = new Mock<IExplorerViewModel>();
            viewModel.Setup(x => x.SelectedFile)
                .Returns(imageFile.Object);

            var copyFileCommand = new CopyCommand(viewModel.Object);

            Assert.True(copyFileCommand.CanExecute(new object()));
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenSelectedFileIsNull()
        {
            ImageFile file = null;
            var viewModel = new Mock<IExplorerViewModel>();
            viewModel.Setup(x => x.SelectedFile)
                .Returns(file);

            var copyFileCommand = new CopyCommand(viewModel.Object);

            Assert.False(copyFileCommand.CanExecute(new object()));
        }

        [Fact]
        public void Execute_ShouldTriggerCopyFileMethod()
        {
            bool methodWasCalled = false;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.Copy())
                .Callback(() => { methodWasCalled = true; });

            var copyFileCommand = new CopyCommand(viewModelMock.Object);

            copyFileCommand.Execute(new object());

            Assert.True(methodWasCalled);
        }
    }
}
