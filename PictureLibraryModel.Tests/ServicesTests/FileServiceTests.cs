using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace PictureLibraryModel.Tests.ServicesTests
{
    public class FileServiceTests
    {
        #region Setup
        private string TestFolder = "FileServiceTests\\";
        public FileServiceTests()
        {
            CleanupFiles();
        }

        ~FileServiceTests()
        {
            CleanupFiles();
        }

        private void CleanupFiles()
        {
            try
            {
                Directory.Delete(TestFolder, true);
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region Copy tests
        [Fact]
        public void Copy_ShouldCopyTheFile_WhenFileExists()
        {
            var fileName = "testimage.jpg";
            var sourceDirectory = TestFolder + "1B4F7D42-C732-4621-98A0-C90D983FA935\\";
            var destinationDirectory = TestFolder + "F804B037-5085-4F3D-A59E-4E6EA17D45D6\\";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);
            var fileStream = File.Create(sourceDirectory + fileName);
            fileStream.Close();

            var service = new FileService();

            service.Copy(sourceDirectory + fileName, destinationDirectory + fileName);

            Assert.True(File.Exists(sourceDirectory + fileName) && File.Exists(destinationDirectory + fileName));
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenSourcePathIsNull()
        {
            string sourcePath = null;
            var destinationDirectory = TestFolder + "93E75D1C-8EEA-4C60-A2B9-A0B7EC84598A\\";
            var fileName = "testFile.jpg";

            Directory.CreateDirectory(destinationDirectory);

            var service = new FileService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourcePath, destinationDirectory + fileName));
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenDestinationPathIsNull()
        { 
            var fileName = "testimage.jpg";
            var sourceDirectory = TestFolder + "BBE57FBE-3506-4F99-8BA1-F2E2867C546B\\";
            string destinationPath = null;

            Directory.CreateDirectory(sourceDirectory);
            var fileStream = File.Create(sourceDirectory + fileName);
            fileStream.Close();

            var service = new FileService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourceDirectory + fileName, destinationPath));
        }
        #endregion

        #region Rename tests
        [Fact]
        public void Rename_ShouldRenameAFile()
        {
            var extension = ".jpg";
            var fileName = "testimage";
            var newFileName = "testImage2";
            var sourceDirectory = TestFolder + "802C6BDA-8348-44E4-BF7F-12E4338C7656\\";

            Directory.CreateDirectory(sourceDirectory);
            var fileStream = File.Create(sourceDirectory + fileName + extension);
            fileStream.Close();

            var service = new FileService();

            service.Rename(sourceDirectory + fileName + extension, newFileName);

            Assert.True(File.Exists(sourceDirectory + newFileName + extension));
        }

        [Fact]
        public void Rename_ShouldThrowIOException_WhenFileWithTheSameNameAlreadyExists()
        {
            var extension = ".jpg";
            var fileName = "testimage";
            var newFileName = "testImage2";
            var sourceDirectory = TestFolder + "15B6C2CF-F9B9-4DE1-ACC5-0BBBE29437FA\\";

            Directory.CreateDirectory(sourceDirectory);
            var fileStream = File.Create(sourceDirectory + fileName + extension);
            fileStream.Close();
            fileStream = File.Create(sourceDirectory + newFileName + extension);
            fileStream.Close();

            var service = new FileService();

            Assert.Throws<IOException>(() => service.Rename(sourceDirectory + fileName + extension, newFileName));
        }
        #endregion
    }
}
