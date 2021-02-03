using Moq;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using PictureLibraryViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xunit;

namespace PictureLibraryViewModel.Tests.CommandTests
{
    public class PasteCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenContainsFilesRetursTrue()
        {
            var imageFileMock = new Mock<ImageFile>();
            var viewModelMock = new Mock<IExplorerViewModel>();

            viewModelMock.Setup(x => x.Clipboard.ContainsFiles())
                .Returns(true);

            var pasteCommand = new PasteCommand(viewModelMock.Object);

            Assert.True(pasteCommand.CanExecute(new object()));
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenContainsFilesReturnsFalse()
        {
            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.Clipboard.ContainsFiles())
                .Returns(false);

            var pasteCommand = new PasteCommand(viewModelMock.Object);

            Assert.False(pasteCommand.CanExecute(new object()));
        }

        [Fact]
        public void Execute_ShouldTriggerPasteMethod()
        {
            bool methodWasCalled = false;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.PasteSelectedElements())
                .Callback(() => { methodWasCalled = true; });

            var pasteCommand = new PasteCommand(viewModelMock.Object);

            pasteCommand.Execute(new object());

            Assert.True(methodWasCalled);
        }
    }
}
