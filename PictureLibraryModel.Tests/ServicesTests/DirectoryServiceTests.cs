using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace PictureLibraryModel.Tests.ServicesTests
{
    public class DirectoryServiceTests
    {
        #region Setup
        private string TestFolder = "DirectoryServiceTests\\";

        public DirectoryServiceTests()
        {
            CleanupFiles();
        }

        ~DirectoryServiceTests()
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

        #region Copy Tests
        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenSourcePathIsNull()
        {
            string sourcePath = null;
            var destinationDirectoryPath = TestFolder + "0972F3EA-7C52-43A0-8139-C9862D1F34EA";

            Directory.CreateDirectory(destinationDirectoryPath);

            var service = new DirectoryService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourcePath, destinationDirectoryPath));
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenDestinationDirectoryPathIsNull()
        {
            var sourcePath = TestFolder + "0972F3EA-7C52-43A0-8139-C9862D1F34EA";
            string destinationDirectoryPath = null;

            Directory.CreateDirectory(sourcePath);

            var service = new DirectoryService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(sourcePath, destinationDirectoryPath));
        }

        [Fact]
        public void Copy_ShouldCopyFolder_WithItsContent()
        {
            var sourcePath = TestFolder + "FB51D97B-5106-45EC-8EA6-A8ACE3EA7558\\";
            var destinationDirectoryPath = TestFolder + "E5C698D6-C470-412C-8F3E-703100AD2A79\\";
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
        }
        #endregion

        #region GetDirectoryContent Tests
        [Fact]
        public void GetDirectoryContent_ShouldReturnContentOfTheDirectory()
        {
            var subFolder1 = "folder1";
            var subFolder2 = "folder2";
            var fileName = "testFile1.jpg";
            var sourcePath = TestFolder + "6589E232-641A-48FC-B4F7-48F421B8CBCF\\";

            Directory.CreateDirectory(sourcePath);
            Directory.CreateDirectory(sourcePath+subFolder1);
            Directory.CreateDirectory(sourcePath+subFolder2);
            var fileStream = File.Create(sourcePath + fileName);
            fileStream.Close();

            var service = new DirectoryService();
            var content = service.GetDirectoryContent(sourcePath);

            Assert.Contains(content, x => x.Name == subFolder1);
            Assert.Contains(content, x => x.Name == subFolder2);
            Assert.Contains(content, x => x.Name == fileName);
        }

        [Fact]
        public void GetDirectoryContent_ShouldThrowArgumentNullException_WhenPathIsNull()
        {
            string path = null;

            var service = new DirectoryService();

            Assert.Throws<ArgumentNullException>(() => service.GetDirectoryContent(path));
        }
        #endregion

        #region GetSubFolders Tests
        [Fact]
        public void GetSubFolders_ShouldReturnSubFolders_WhenPathIsCorrect()
        {
            var directory = "3D2DCE88-3524-4CB7-9311-47864BC0DAEE\\";
            var subFolder1 = "Folder1";
            var subFolder2 = "Folder2";

            Directory.CreateDirectory(TestFolder + directory + subFolder1);
            Directory.CreateDirectory(TestFolder + directory + subFolder2);

            var service = new DirectoryService();
            var folders = service.GetSubFolders(TestFolder + directory);

            Assert.Contains(folders, x => x.Name == subFolder1);
            Assert.Contains(folders, x => x.Name == subFolder2);
        }

        [Fact]
        public void GetSubFolders_ShouldThrowArgumentNullException_WhenPathIsNull()
        {
            var service = new DirectoryService();

            Assert.Throws<ArgumentNullException>(() => service.GetSubFolders(null));
        }

        [Fact]
        public void GetSubFolders_ShouldThrowArgumentException_WhenPathIsEmpty()
        {
            var service = new DirectoryService();

            Assert.Throws<ArgumentException>(() => service.GetSubFolders(String.Empty));
        }

        [Fact]
        public void GetSubFolders_ShouldThrowDirectoryNotFoundException_WhenPathDoesntExist()
        {
            var service = new DirectoryService();

            Assert.Throws<DirectoryNotFoundException>(() => service.GetSubFolders(TestFolder + "randomString"));
        }
        #endregion
    }
}
