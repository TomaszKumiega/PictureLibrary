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

            var elementsList = new List<IExplorableElement>();
            elementsList.Add(imageFileMock.Object);

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.SelectedElements)
                .Returns(elementsList);

            var cutCommand = new CutCommand(viewModelMock.Object);

            Assert.True(cutCommand.CanExecute(new object()));
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenSelectedFilesIsNull()
        {
            List<IExplorableElement> list = null;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.SelectedElements)
                .Returns(list);

            var cutCommand = new CutCommand(viewModelMock.Object);

            Assert.False(cutCommand.CanExecute(new object()));
        }

        [Fact]
        public void Execute_ShouldCallCutMethod()
        {
            bool methodWasCalled = false;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.Cut())
                .Callback(() => { methodWasCalled = true; });

            var cutCommand = new CutCommand(viewModelMock.Object);

            cutCommand.Execute(new object());

            Assert.True(methodWasCalled);
        }
    }
}
