using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;

namespace PictureLibraryViewModel.Tests.CommandTests
{
    public class CopyCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenSelectedFileIsInitialized()
        {
            var imageFileMock = new Mock<ImageFile>();

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFileMock.Object);

            var viewModel = new Mock<IExplorerViewModel>();
            viewModel.Setup(x => x.SelectedFiles)
                .Returns(elementsList);

            var copyCommand = new CopyCommand(viewModel.Object);

            Assert.True(copyCommand.CanExecute(new object()));
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenSelectedFileIsNull()
        {
            ImageFile imageFile = null;

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFile);

            var viewModel = new Mock<IExplorerViewModel>();
            viewModel.Setup(x => x.SelectedFiles)
                .Returns(elementsList);

            var copyCommand = new CopyCommand(viewModel.Object);

            Assert.False(copyCommand.CanExecute(new object()));
        }

        [Fact]
        public void Execute_ShouldTriggerCopyMethod()
        {
            bool methodWasCalled = false;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.Copy())
                .Callback(() => { methodWasCalled = true; });

            var copyCommand = new CopyCommand(viewModelMock.Object);

            copyCommand.Execute(new object());

            Assert.True(methodWasCalled);
        }
    }
}
