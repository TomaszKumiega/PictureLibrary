using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PictureLibraryModel.Tests.ModelTests
{
    public class DirectoryTests
    {
        [Fact]
        public async void LoadSubDirectoriesAsync_ShouldInitializeSubDirectories_WhenGetSubFoldersReturnsSubFolders()
        {
            var fullName = "Tests\\Directory1\\";
            var directoryServiceMock = new Mock<IDirectoryService>();
            var subFolders = GetFolderSample();

            directoryServiceMock.Setup(x => x.GetSubFolders(fullName))
                .Returns(subFolders);

            Directory directory = new Folder(directoryServiceMock.Object)
            {
                FullName = fullName
            };

            await directory.LoadSubDirectoriesAsync();
            
            foreach(var t in subFolders)
            {
                Assert.Contains(t, directory.SubDirectories);
            }
        }

        [Fact]
        public async void LoadSubDirectoriesAsync_ShouldClearSubDirectoriesBeforeAddingNewSubDirectories()
        {
            var fullName = "Tests\\Directory1\\";
            var directoryServiceMock = new Mock<IDirectoryService>();
            var subFolders = GetFolderSample();

            directoryServiceMock.Setup(x => x.GetSubFolders(fullName))
                .Returns(subFolders);

            Directory directory = new Folder(directoryServiceMock.Object)
            {
                FullName = fullName
            };

            await directory.LoadSubDirectoriesAsync();

            Assert.True(directory.SubDirectories.Count == subFolders.Count);
        }

        [Fact]
        public async void LoadSubDirectoriesAsync_ShouldThrowException_WhenGetSubFoldersMethodThrowsException()
        {
            var fullName = "Tests\\Directory1\\";
            var directoryServiceMock = new Mock<IDirectoryService>();

            directoryServiceMock.Setup(x => x.GetSubFolders(fullName))
                .Throws(new Exception());

            Directory directory = new Folder(directoryServiceMock.Object)
            {
                FullName = fullName
            };

            await Assert.ThrowsAsync<Exception>(async () => await directory.LoadSubDirectoriesAsync());
        }

        private List<Folder> GetFolderSample()
        {
            var list = new List<Folder>();

            var folder1 =
                new Folder()
                {
                    Name = "Folder1",
                    FullName = "Tests\\Directory1\\Folder1\\"
                };

            var folder2 =
                new Folder()
                {
                    Name = "Folder2",
                    FullName = "Tests\\Directory1\\Folder2\\"
                };

            list.Add(folder1);
            list.Add(folder2);

            return list;
        }
    }
}
