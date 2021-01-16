using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SystemInterface;
using SystemInterface.IO;
using SystemWrapper.IO;
using Xunit;

namespace PictureLibraryModel.Tests.ModelTests
{
    public class LocalFileSystemImageFileBuilderTests
    {
        [Fact]
        public void BuildCreationTime_ShouldInitializeCreationTime_WhenFileInfoIsInitialized()
        {
            var dateTime = DateTime.Now;
            var fileInfoMock = new Mock<IFileInfo>();
            var dateTimeMock = new Mock<IDateTime>();

            dateTimeMock.Setup(x => x.DateTimeInstance)
                .Returns(dateTime);
            fileInfoMock.Setup(x => x.CreationTimeUtc)
                .Returns(dateTimeMock.Object);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildCreationTime();

            Assert.True(DateTime.Compare(dateTime, builder.ImageFile.CreationTime) == 0);   
        }

        [Fact]
        public void BuildExtension_ShouldInitializeExtension_WhenFileInfoIsInitialized()
        {
            var extensionString = ".jpg";
            var extension = ImageExtensionHelper.GetExtension(extensionString);
            var fileInfoMock = new Mock<IFileInfo>();

            fileInfoMock.Setup(x => x.Extension)
                .Returns(extensionString);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildExtension();

            Assert.True(extension == builder.ImageFile.Extension);
        }
    }
}
