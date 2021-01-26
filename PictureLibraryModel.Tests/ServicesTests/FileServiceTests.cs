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
        public FileServiceTests()
        {
            CleanupFiles();
        }

        private void CleanupFiles()
        {
            try
            {
                Directory.Delete("Tests/", true);
            }
            catch
            {
                return;
            }
        }

        [Fact]
        public void Copy_ShouldCopyTheFile_WhenFileExists()
        {
            var fileName = "testimage.jpg";
            var sourceDirectory = "Tests\\Folder1\\";
            var destinationDirectory = "Tests\\Folder2\\";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);
            var fileStream = File.Create(sourceDirectory + fileName);
            fileStream.Close();

            var service = new FileService();

            service.Copy(sourceDirectory + fileName, destinationDirectory + fileName);

            Assert.True(File.Exists(sourceDirectory + fileName) && File.Exists(destinationDirectory + fileName));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenSourcePathIsNull()
        {
            string sourcePath = null;
            var destinationDirectory = "Tests\\Folder2\\";
            var fileName = "testFile.jpg";

            Directory.CreateDirectory(destinationDirectory);

            var service = new FileService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourcePath, destinationDirectory + fileName));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenDestinationPathIsNull()
        {
            var fileName = "testimage.jpg";
            var sourceDirectory = "Tests\\Folder1\\";
            string destinationPath = null;

            Directory.CreateDirectory(sourceDirectory);
            var fileStream = File.Create(sourceDirectory + fileName);
            fileStream.Close();

            var service = new FileService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourceDirectory + fileName, destinationPath));

            CleanupFiles();
        }
    }
}
