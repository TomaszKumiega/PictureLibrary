using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services;

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

        ~WindowsFileSystemServiceTests()
        {
            CleanupFiles();
        }

    }
}
