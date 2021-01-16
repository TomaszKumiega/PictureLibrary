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

        [Fact]
        public void BuildFullPath_ShouldInitializeFullPath_WhenFileInfoIsInitialized()
        {
            var filePath = "Tests\\fileName.jpg";
            var fileInfoMock = new Mock<IFileInfo>();

            fileInfoMock.Setup(x => x.FullName)
                .Returns(filePath);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildFullPath();

            Assert.True(filePath == builder.ImageFile.FullPath);
        }

        [Fact]
        public void BuildIconSource_ShouldInitializeIconSource_WhenFileInfoIsInitialized()
        {
            var filePath = "Tests\\fileName.jpg";
            var fileInfoMock = new Mock<IFileInfo>();

            fileInfoMock.Setup(x => x.FullName)
                .Returns(filePath);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildIconSource();

            Assert.True(filePath == builder.ImageFile.IconSource);
        }

        [Fact]
        public void BuildLastAccessTime_ShouldInitializeLastAccessTime_WhenFileInfoIsInitialized()
        {
            var dateTime = DateTime.Now;
            var fileInfoMock = new Mock<IFileInfo>();
            var dateTimeMock = new Mock<IDateTime>();

            dateTimeMock.Setup(x => x.DateTimeInstance)
                .Returns(dateTime);
            fileInfoMock.Setup(x => x.LastAccessTimeUtc)
                .Returns(dateTimeMock.Object);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildLastAccessTime();

            Assert.True(DateTime.Compare(dateTime, builder.ImageFile.LastAccessTime) == 0);
        }

        [Fact]
        public void BuildLastWriteTime_ShouldInitializeLastWriteTime_WhenFileInfoIsInitialized()
        {
            var dateTime = DateTime.Now;
            var fileInfoMock = new Mock<IFileInfo>();
            var dateTimeMock = new Mock<IDateTime>();

            dateTimeMock.Setup(x => x.DateTimeInstance)
                .Returns(dateTime);
            fileInfoMock.Setup(x => x.LastWriteTimeUtc)
                .Returns(dateTimeMock.Object);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildLastWriteTime();

            Assert.True(DateTime.Compare(dateTime, builder.ImageFile.LastWriteTime) == 0);
        }

        [Fact]
        public void BuildLibraryFullPath_ShouldMakeLibraryFullPathNull_WhenFileInfoIsInitialized()
        {
            var fileInfoMock = new Mock<IFileInfo>();

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildLibraryFullPath();

            Assert.True(builder.ImageFile.LibraryFullPath == null);
        }

        [Fact]
        public void BuildName_ShouldInitializeName_WhenFileInfoIsInitialized()
        {
            var name = "filename.jpg";
            var fileInfoMock = new Mock<IFileInfo>();

            fileInfoMock.Setup(x => x.Name)
                .Returns(name);

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildName();

            Assert.True(name == builder.ImageFile.Name);
        }

        [Fact]
        public void BuildOrigin_ShouldSetOriginToLocal()
        {
            var fileInfoMock = new Mock<IFileInfo>();

            var builder = new LocalFileSystemImageFileBuilder(fileInfoMock.Object);

            builder.BuildOrigin();

            Assert.True(Origin.Local == builder.ImageFile.Origin);
        }
    }
}
