﻿using System;
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
        private void CleanupFiles()
        {
            try
            {
                Directory.Delete("Tests/");
            }
            catch
            {
                return;
            }
        }

        [Fact]
        public void Copy_ShouldCopyAnEntity_WhenEntityIsAnImageFile()
        {
            var fileName = "testimage.jpg";
            var fileSourceDirectory = "Tests/Folder1/";
            var destinationPath = "Tests/Folder2/";

            Directory.CreateDirectory(fileSourceDirectory);
            Directory.CreateDirectory(destinationPath);
            var fileStream = File.Create(fileSourceDirectory + fileName);
            fileStream.Close();

            var imageFile =
                new ImageFile()
                {
                    Name = fileName,
                    Extension = ".jpg",
                    FullPath = fileSourceDirectory + fileName
                };

            var service = new WindowsFileSystemService();

            service.Copy(imageFile, destinationPath);

            Assert.True(File.Exists(destinationPath + fileName));

            CleanupFiles();
        }

        [Fact]
        public void Copy_ShouldCopyAnEntity_WhenEntityIsAFolder()
        {
            var folderName = "Folder";
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

            Assert.True(Directory.Exists(destinationDirectory + folderName));

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

        ~WindowsFileSystemServiceTests()
        {
            CleanupFiles();
        }

    }
}
