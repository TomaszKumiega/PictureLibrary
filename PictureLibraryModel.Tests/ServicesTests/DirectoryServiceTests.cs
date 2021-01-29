using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace PictureLibraryModel.Tests.ServicesTests
{
    public class DirectoryServiceTests
    {

        public DirectoryServiceTests()
        {
            CleanupFiles();
        }

        private void CleanupFiles()
        {
            try
            {
                Directory.Delete("Tests\\", true);
            }
            catch
            {
                return;
            }
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenSourcePathIsNull()
        {
            string sourcePath = null;
            var destinationDirectoryPath = "Tests\\Folder1";

            Directory.CreateDirectory(destinationDirectoryPath);

            var service = new DirectoryService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourcePath, destinationDirectoryPath));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenDestinationDirectoryPathIsNull()
        {
            var sourcePath = "Tests\\Folder1";
            string destinationDirectoryPath = null;

            Directory.CreateDirectory(sourcePath);

            var service = new DirectoryService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourcePath, destinationDirectoryPath));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldCopyFolder_WithItsContent()
        {
            var sourcePath = "Tests\\Folder1\\";
            var destinationDirectoryPath = "Tests\\Folder2\\";
            var subFolder1 = "folder1\\";
            var subFolder2 = "folder2\\";
            var file1 = "testFile1.jpg";

            Directory.CreateDirectory(sourcePath);
            Directory.CreateDirectory(destinationDirectoryPath);
            Directory.CreateDirectory(sourcePath + subFolder1);
            Directory.CreateDirectory(sourcePath + subFolder2);

            var fileStream = File.Create(sourcePath + subFolder2 + file1);
            fileStream.Close();

            var service = new DirectoryService();

            service.Copy(sourcePath, destinationDirectoryPath);

            Assert.True(Directory.Exists(sourcePath + subFolder1) && Directory.Exists(destinationDirectoryPath + subFolder1) 
                && File.Exists(sourcePath + subFolder2 + file1) && File.Exists(sourcePath + subFolder2 + file1));

            CleanupFiles();
        }
    }
}
