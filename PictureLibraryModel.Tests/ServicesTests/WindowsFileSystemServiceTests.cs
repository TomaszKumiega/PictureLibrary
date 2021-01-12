using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services;
using Moq;

using Directory = System.IO.Directory;

namespace PictureLibraryModel.Tests.ServicesTests
{
    public class WindowsFileSystemServiceTests
    {

        public WindowsFileSystemServiceTests()
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

        #region Copy Tests
        [Fact]
        public void Copy_ShouldCopyAnEntity_WhenEntityIsAnImageFile()
        {
            var fileName = "testimage.jpg";
            var sourceDirectory = "Tests/Folder1/";
            var destinationPath = "Tests/Folder2/";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationPath);
            var fileStream = File.Create(sourceDirectory + fileName);
            fileStream.Close();

            var imageFile =
                new ImageFile()
                {
                    Name = fileName,
                    FullPath = sourceDirectory + fileName
                };

            var service = new WindowsFileSystemService();

            service.Copy(imageFile, destinationPath);

            Assert.True(File.Exists(sourceDirectory + fileName) && File.Exists(destinationPath + fileName));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldCopyAnEmptyFolder()
        {
            var folderName = "Folder/";
            var sourceDirectory = "Tests/Folder1/";
            var destinationDirectory = "Tests/Folder2/";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);
            var fileStream = Directory.CreateDirectory(sourceDirectory + folderName);

            var folder =
                new Folder()
                {
                    FullPath = sourceDirectory + folderName,
                    Name = folderName,
                };


            var service = new WindowsFileSystemService();

            service.Copy(folder, destinationDirectory);

            Assert.True(Directory.Exists(sourceDirectory + folderName) && Directory.Exists(destinationDirectory + folderName));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldCopyFolderWithItsContents()
        {
            var folderName = "Folder/";
            var sourceDirectory = "Tests/Folder1/";
            var destinationDirectory = "Tests/Folder2/";
            var file1 = "testFile1.jpg";
            var file2 = "testFile2.jpg";
            var subFolder = "subFolder/";
            var file3 = "testFile3.jpg";

            Directory.CreateDirectory(sourceDirectory + folderName);
            Directory.CreateDirectory(destinationDirectory);
            Directory.CreateDirectory(sourceDirectory + folderName + subFolder);

            var filePath1 = folderName + file1;
            var filePath2 = folderName + file2;
            var filePath3 = folderName + subFolder + file3;

            var fileStream = File.Create(sourceDirectory + filePath1);
            fileStream.Close();
            fileStream = File.Create(sourceDirectory + filePath2);
            fileStream.Close();
            fileStream = File.Create(sourceDirectory + filePath3);
            fileStream.Close();

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            service.Copy(folder, destinationDirectory);

            var condition = File.Exists(sourceDirectory + filePath1) && File.Exists(sourceDirectory + filePath2) && File.Exists(sourceDirectory + filePath3) &&
                File.Exists(destinationDirectory + filePath1) && File.Exists(destinationDirectory + filePath2) && File.Exists(destinationDirectory + filePath3);
            
            Assert.True(condition);

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenEntityIsNull()
        {
            var destinationDirectory = "Tests/Folder2/";

            Directory.CreateDirectory(destinationDirectory);

            Folder folder = null;

            var service = new WindowsFileSystemService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(folder, destinationDirectory));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowArgumentNullException_WhenDestinationPathIsNull()
        {
            var folderName = "Folder";
            var sourceDirectory = "Tests/Folder1";
            string destinationDirectory = null;

            Directory.CreateDirectory(sourceDirectory + folderName);

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            Assert.Throws<ArgumentNullException>(() => service.Copy(folder, destinationDirectory));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowArgumentException_WhenDestinationPathIsEmpty()
        {
            var folderName = "Folder";
            var sourceDirectory = "Tests/Folder1";
            var destinationDirectory = "";

            Directory.CreateDirectory(sourceDirectory + folderName);

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            Assert.Throws<ArgumentException>(() => service.Copy(folder, destinationDirectory));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowDirectoryNotFoundException_WhenDestinationDirectoryDoesntExist()
        {
            var folderName = "Folder";
            var sourceDirectory = "Tests/Folder1";
            var destinationDirectory = "Tests/Folder2";

            Directory.CreateDirectory(sourceDirectory + folderName);

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            Assert.Throws<DirectoryNotFoundException>(() => service.Copy(folder, destinationDirectory));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowFileNotFoundException_WhenFileDoesntExist()
        {
            var fileName = "testFile.jpg";
            var sourceDirectory = "Tests/Folder1";
            var destinationDirectory = "Tests/Folder2";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);

            var imageFile =
                new ImageFile()
                {
                    Name = fileName,
                    FullPath = sourceDirectory + fileName,
                    Extension = ".jpg"
                };

            var service = new WindowsFileSystemService();

            Assert.Throws<FileNotFoundException>(() => service.Copy(imageFile, destinationDirectory));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldThrowDirectoryNotFoundException_WhenFolderDoesntExist()
        {
            var folderName = "Folder";
            var sourceDirectory = "Tests/Folder1";
            var destinationDirectory = "Tests/Folder2";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            Assert.Throws<DirectoryNotFoundException>(() => service.Copy(folder, destinationDirectory));

            CleanupFiles();
        }
        #endregion

        #region Move Tests
        [Fact]
        public void Move_ShouldMoveImageFile()
        {
            var fileName = "testImage.jpg";
            var sourceDirectory = "Tests/Folder1/";
            var destinationDirectory = "Tests/Folder2/";

            Directory.CreateDirectory(sourceDirectory);
            Directory.CreateDirectory(destinationDirectory);
            var fileStream = File.Create(sourceDirectory + fileName);
            fileStream.Close();

            var imageFile =
                new ImageFile()
                {
                    Name = fileName,
                    FullPath = sourceDirectory + fileName
                };

            var service = new WindowsFileSystemService();

            service.Move(imageFile, destinationDirectory);

            Assert.True(File.Exists(destinationDirectory + fileName));

            CleanupFiles();
        }

        [Fact]
        public void Move_ShouldMoveAnEmptyFolder()
        {
            var folderName = "Folder";
            var sourceDirectory = "Tests/Folder1/";
            var destinationDirectory = "Tests/Folder2/";

            Directory.CreateDirectory(sourceDirectory + folderName);
            Directory.CreateDirectory(destinationDirectory);

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            service.Move(folder, destinationDirectory);

            Assert.True(Directory.Exists(destinationDirectory + folderName));

            CleanupFiles();
        }

        [Fact]
        public void Move_ShouldMoveFolderWithItsContents()
        {
            var folderName = "Folder/";
            var sourceDirectory = "Tests/Folder1/";
            var destinationDirectory = "Tests/Folder2/";
            var file1 = "testFile1.jpg";
            var file2 = "testFile2.jpg";
            var subFolder = "subFolder/";
            var file3 = "testFile3.jpg";
            
            Directory.CreateDirectory(sourceDirectory + folderName);
            Directory.CreateDirectory(destinationDirectory);
            Directory.CreateDirectory(sourceDirectory + folderName + subFolder);

            var filePath1 = folderName + file1;
            var filePath2 = folderName + file2;
            var filePath3 = folderName + subFolder + file3;

            var fileStream = File.Create(sourceDirectory + filePath1);
            fileStream.Close();
            fileStream = File.Create(sourceDirectory + filePath2);
            fileStream.Close();
            fileStream = File.Create(sourceDirectory + filePath3);
            fileStream.Close();

            var folder =
                new Folder()
                {
                    Name = folderName,
                    FullPath = sourceDirectory + folderName
                };

            var service = new WindowsFileSystemService();

            service.Move(folder, destinationDirectory);

            Assert.True(File.Exists(destinationDirectory + filePath1) && File.Exists(destinationDirectory + filePath2) && File.Exists(destinationDirectory + filePath3));

            CleanupFiles();
        }

        [Fact]
        public void Move_ShouldThrowArgumentNullException_WhenEntityIsNull()
        {
            var destinationPath = "Tests\\Folder2\\";
            Directory.CreateDirectory(destinationPath);

            ImageFile file = null;

            var service = new WindowsFileSystemService();

            Assert.Throws<ArgumentNullException>(() => service.Move(file, destinationPath));

            CleanupFiles();
        }

        [Fact]
        public void Move_ShouldThrowArgumentNullException_WhenDestinationPathIsNull()
        {
            var fileName = "testFile.jpg";
            var sourceDirectory = "Tests\\Folder1\\";
            string destinationDirectory = null;

            var imageFile =
                new ImageFile()
                {
                    Name = fileName,
                    FullPath = sourceDirectory + fileName
                };

            var service = new WindowsFileSystemService();

            Assert.Throws<ArgumentNullException>(() => service.Move(imageFile, destinationDirectory));

            CleanupFiles();
        }

        #endregion

        ~WindowsFileSystemServiceTests()
        {
            CleanupFiles();
        }

    }
}
