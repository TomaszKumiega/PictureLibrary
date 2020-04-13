using System;
using System.Collections.Generic;
using System.Text;
using PictureLibraryModel.Services;
using Xunit;

namespace PictureLibraryModel.Tests
{
    public class FileSystemService_Tests
    {
        #region CopyFileTests

        [Fact]
        public void CopyFile_ShouldThrowArgumentNullExceptionWhenSourceFilePathIsNull()
        {
            var fileSystemService = new FileSystemService();

            Assert.Throws<ArgumentNullException>(() => fileSystemService.CopyFile(null, "", false));
        }

        [Fact]
        public void CopyFile_ShouldThrowArgumentNullExceptionWhenDestinationFilePathIsNull()
        {
            var fileSystemService = new FileSystemService();

            Assert.Throws<ArgumentNullException>(() => fileSystemService.CopyFile("file.jpg", null, false));
        }

        [Fact]
        public void CopyFile_ShouldThrowArgumentExceptionWhenSourceFilePathIsEmpty()
        {
            var fileSystemService = new FileSystemService();

            Assert.Throws<ArgumentException>(() => fileSystemService.CopyFile("", "/destination", false));
        }

        [Fact]
        public void CopyFile_ShouldThrowArgumentExceptionWhenDestinationFilePathIsEmpty()
        {
            var fileSystemService = new FileSystemService();

            Assert.Throws<ArgumentException>(() => fileSystemService.CopyFile("file.png", "", false));
        }

        [Fact]
        public void CopyFile_ShouldThrowArgumentExceptionWhenSourceFilePathIsWhitespace()
        {
            var fileSystemService = new FileSystemService();

            Assert.Throws<ArgumentException>(() => fileSystemService.CopyFile(" ", "/destination", false));
        }

        [Fact]
        public void CopyFile_ShouldThrowArgumentExceptionWhenDestinationFilePathIsWhitespace()
        {
            var fileSystemService = new FileSystemService();

            Assert.Throws<ArgumentException>(() => fileSystemService.CopyFile("file.jpg", " ", false));
        }

        #endregion

    }
}
