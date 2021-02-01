using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace PictureLibraryModel.Tests.RepositoryTests
{
    public class LocalLibraryRepositoryTests
    {
        #region Setup
        private string TestFolder = "Tests\\";

        public LocalLibraryRepositoryTests()
        {
            CleanupFiles();
        }

        ~LocalLibraryRepositoryTests()
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

        #region Helper methods
        private Library GetLibrary()
        {
            var library =
                new Library()
                {
                    FullPath = TestFolder + "library.plib",
                    Name = "library",
                    Description = "picture library",
                    Tags = new List<Tag>(),
                    Images = new List<ImageFile>(),
                    Owners = new List<Guid>(),
                    Origin = Origin.Local,
                };

            return library;
        }

        private string GetLibraryXml()
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n <library owners = \"\" description = \"picture library\" name = \"library\" > \n <tags /> \n <images /> \n </library>";
        }
        #endregion

        #region AddAsync Tests
        [Fact]
        public async void AddAsync_ShouldWriteLibraryToTheStream_WhenLibraryIsInitialized()
        {
            var fileServiceMock = new Mock<IFileService>();
            var memoryStream = new MemoryStream();
            var library = GetLibrary();
            
            fileServiceMock.Setup(x => x.OpenFile(library.FullPath))
                .Returns(memoryStream);

            var repository = new LocalLibraryRepository(fileServiceMock.Object);
            await repository.AddAsync(library);

            // convert memory stream buffer to string
            var buffer = memoryStream.GetBuffer();
            var text = System.Text.Encoding.UTF8.GetString(buffer);
            text = text.Replace('\0', ' ');
            text = text.TrimEnd();

            // read attributes from the string
            var doc = XDocument.Parse(text);
            var name = doc.Element("library").Attribute("name");
            var description = doc.Element("library").Attribute("description");

            Assert.True(name.Value == library.Name);
            Assert.True(description.Value == library.Description);
        }
        #endregion

        #region GetAllAsync Tests
        [Fact]
        public async void GetAllAsync_ShouldReturnLibraries()
        {
            var xml = GetLibraryXml();
            var paths = new List<string>();
            paths.Add("Folder1\\library1.plib");
            paths.Add("Folder2\\library2.plib");
            byte[] buffer = Encoding.UTF8.GetBytes(xml);
            var memoryStream1 = new MemoryStream(buffer);
            var memoryStream2 = new MemoryStream(buffer);

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.FindFiles("*.plib"))
                .Returns(paths);
            fileServiceMock.Setup(x => x.OpenFile(paths[0]))
                .Returns(memoryStream1);
            fileServiceMock.Setup(x => x.OpenFile(paths[1]))
                .Returns(memoryStream2);

            var repository = new LocalLibraryRepository(fileServiceMock.Object);

            var libraries = await repository.GetAllAsync();

            Assert.Contains(libraries, x => x.Name == "library" && x.Description=="picture library");
            Assert.True(libraries.ToList().Count > 1);
        }
        #endregion
    }
}
