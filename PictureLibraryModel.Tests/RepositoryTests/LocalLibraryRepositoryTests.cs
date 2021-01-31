using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Repositories;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
