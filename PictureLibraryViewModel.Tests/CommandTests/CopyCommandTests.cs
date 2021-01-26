﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PictureLibraryViewModel.ViewModel;
using PictureLibraryModel.Model;
using PictureLibraryViewModel.Commands;
using System.Collections.ObjectModel;

namespace PictureLibraryViewModel.Tests.CommandTests
{
    public class CopyCommandTests
    {
        [Fact]
        public void CanExecute_ShouldReturnTrue_WhenSelectedFileIsInitialized()
        {
            var imageFileMock = new Mock<ImageFile>();

            var elementsList = new ObservableCollection<IExplorableElement>();
            elementsList.Add(imageFileMock.Object);

            var viewModel = new Mock<IExplorerViewModel>();
            viewModel.Setup(x => x.SelectedElements)
                .Returns(elementsList);

            var copyCommand = new CopyCommand(viewModel.Object);

            Assert.True(copyCommand.CanExecute(new object()));
        }

        [Fact]
        public void CanExecute_ShouldReturnFalse_WhenSelectedFilesIsNull()
        {
            ObservableCollection<IExplorableElement> list = null;

            var viewModel = new Mock<IExplorerViewModel>();
            viewModel.Setup(x => x.SelectedElements)
                .Returns(list);

            var copyCommand = new CopyCommand(viewModel.Object);

            Assert.False(copyCommand.CanExecute(new object()));
        }

        [Fact]
        public void Execute_ShouldTriggerCopyMethod()
        {
            bool methodWasCalled = false;

            var viewModelMock = new Mock<IExplorerViewModel>();
            viewModelMock.Setup(x => x.CopySelectedElements())
                .Callback(() => { methodWasCalled = true; });

            var copyCommand = new CopyCommand(viewModelMock.Object);

            copyCommand.Execute(new object());

            Assert.True(methodWasCalled);
        }
    }
}
