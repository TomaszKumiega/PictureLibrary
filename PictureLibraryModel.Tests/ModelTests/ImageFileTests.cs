using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace PictureLibraryModel.Tests.ModelTests
{
    public class ImageFileTests
    {
        #region Setup
        private string TestFolder = "DirectoryServiceTests\\";

        public ImageFileTests()
        {
            CleanupFiles();
        }

        ~ImageFileTests()
        {
            CleanupFiles();
        }

        private void CleanupFiles()
        {
            try
            {
                System.IO.Directory.Delete(TestFolder, true);
            }
            catch
            {
                return;
            }
        }
        #endregion

        [Fact]
        public void IsFileAnImage_ShouldReturnTrue_WhenFileHasImageExtension()
        {
            var filePath = "Image.jpg";
            var folder = TestFolder;

            System.IO.Directory.CreateDirectory(folder);
            var filestream = System.IO.File.Create(folder + filePath);
            filestream.Close();

            var fileInfo = new FileInfo(folder + filePath);
            var result = ImageFile.IsFileAnImage(fileInfo);

            Assert.True(result);
        }

        [Fact]
        public void IsFileAnImage_ShouldReturnFalse_WhenFileHasExtensionThatIsNotImageExtension()
        {
            var filePath = "file.txt";
            var folder = TestFolder;

            System.IO.Directory.CreateDirectory(folder);
            var filestream = System.IO.File.Create(folder + filePath);
            filestream.Close();

            var fileInfo = new FileInfo(folder + filePath);
            var result = ImageFile.IsFileAnImage(fileInfo);

            Assert.False(result);
        }
    }
}
