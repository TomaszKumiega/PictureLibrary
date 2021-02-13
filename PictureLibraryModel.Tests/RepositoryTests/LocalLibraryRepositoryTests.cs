using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories.LibraryRepositories;
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

        private List<string> GetLibraryXmlSamples()
        {
            var libraries = new List<string>();

            libraries.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n <library owners = \"\" description = \"picture library1\" name = \"library1\" > \n <tags /> \n <images /> \n </library>");
            libraries.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n <library owners = \"\" description = \"picture library2\" name = \"library2\" > \n <tags /> \n <images /> \n </library>");
            libraries.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?> \n <library owners = \"\" description = \"picture library3\" name = \"library3\" > \n <tags /> \n <images /> \n </library>");
            
            return libraries;
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
            var xml = GetLibraryXmlSamples();
            var paths = new List<string>();
            paths.Add("Folder1\\library1.plib");
            paths.Add("Folder2\\library2.plib");
            byte[] buffer = Encoding.UTF8.GetBytes(xml[0]);
            byte[] buffer2 = Encoding.UTF8.GetBytes(xml[1]);
            var memoryStream1 = new MemoryStream(buffer);
            var memoryStream2 = new MemoryStream(buffer2);

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.FindFiles("*.plib"))
                .Returns(paths);
            fileServiceMock.Setup(x => x.OpenFile(paths[0]))
                .Returns(memoryStream1);
            fileServiceMock.Setup(x => x.OpenFile(paths[1]))
                .Returns(memoryStream2);

            var repository = new LocalLibraryRepository(fileServiceMock.Object);

            var libraries = await repository.GetAllAsync();

            Assert.True(libraries.ToList().Count > 1);
            Assert.Contains(libraries, x => x.Name == "library1" && x.Description == "picture library1");
            Assert.Contains(libraries, x => x.Name == "library2" && x.Description == "picture library2");
        }
        #endregion

        #region FindAsync Tests
        [Fact]
        public void FindAsync_ShouldFindLibraryLibrariesMatchingThePredicate()
        {
            var xml = GetLibraryXmlSamples();
            var paths = new List<string>();
            paths.Add("Folder1\\library1.plib");
            paths.Add("Folder2\\library2.plib");
            byte[] buffer = Encoding.UTF8.GetBytes(xml[0]);
            byte[] buffer2 = Encoding.UTF8.GetBytes(xml[1]);
            var memoryStream1 = new MemoryStream(buffer);
            var memoryStream2 = new MemoryStream(buffer2);

            var fileServiceMock = new Mock<IFileService>();

            fileServiceMock.Setup(x => x.FindFiles("*.plib"))
                .Returns(paths);
            fileServiceMock.Setup(x => x.OpenFile(paths[0]))
                .Returns(memoryStream1);
            fileServiceMock.Setup(x => x.OpenFile(paths[1]))
                .Returns(memoryStream2);

            var repository = new LocalLibraryRepository(fileServiceMock.Object);

            var libraries = repository.FindAsync(x => x.Name == "library2").Result;

            Assert.True(libraries != null);
            Assert.True(libraries.Any());
            Assert.Contains(libraries, x => x.Name == "library2" && x.Description == "picture library2");
        }
        #endregion
    }
}
