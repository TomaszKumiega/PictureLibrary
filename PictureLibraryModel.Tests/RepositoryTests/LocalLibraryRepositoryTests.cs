using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Repositories.LibraryRepositories;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    FullName = TestFolder + "library.plib",
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
        public async void AddAsync_ShouldWriteLibraryToTheStreamAndSaveLibraryPathToSettings()
        {
            var fileServiceMock = new Mock<IFileService>();
            var directoryServiceMock = new Mock<IDirectoryService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
            };


            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);
            settingsProviderMock.Setup(x => x.SaveSettingsAsync())
                .Verifiable();

            var memoryStream = new MemoryStream();
            var library = GetLibrary();
            
            fileServiceMock.Setup(x => x.OpenFile(library.FullName))
                .Returns(memoryStream);

            var repository = new LocalLibraryRepository(fileServiceMock.Object, directoryServiceMock.Object, settingsProviderMock.Object);
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

            settingsProviderMock.Verify(x => x.SaveSettingsAsync());
            settings.ImportedLibraries.Contains(library.FullName);
            Assert.True(name.Value == library.Name);
            Assert.True(description.Value == library.Description);
        }
        #endregion

        #region GetAllAsync Tests
        [Fact]
        public async void GetAllAsync_ShouldReturnLibraries()
        {
            var xml = GetLibraryXmlSamples();
            IEnumerable<string> paths = new List<string>()
            {
                "Folder1\\library1.plib",
                "Folder2\\library2.plib"
            };

            byte[] buffer = Encoding.UTF8.GetBytes(xml[0]);
            byte[] buffer2 = Encoding.UTF8.GetBytes(xml[1]);
            var memoryStream1 = new MemoryStream(buffer);
            var memoryStream2 = new MemoryStream(buffer2);

            var settings = new Settings()
            {
                ImportedLibraries = paths.ToList()
            };

            var settingsProviderMock = new Mock<ISettingsProviderService>();
            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            var rootDirectory =
                new Folder()
                {
                    Name = "\\",
                    FullName = "\\"
                };

            var rootDirectories = new List<Model.Directory>
            {
                rootDirectory
            };

            var directoryServiceMock = new Mock<IDirectoryService>();
            directoryServiceMock.Setup(x => x.GetRootDirectories())
                .Returns(rootDirectories);

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.OpenFile(paths.ToList()[0]))
                .Returns(memoryStream1);
            fileServiceMock.Setup(x => x.OpenFile(paths.ToList()[1]))
                .Returns(memoryStream2);

            var repository = new LocalLibraryRepository(fileServiceMock.Object, directoryServiceMock.Object, settingsProviderMock.Object);

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
            IEnumerable<string> paths = new List<string>()
            {
                "Folder1\\library1.plib",
                "Folder2\\library2.plib"
            };

            byte[] buffer = Encoding.UTF8.GetBytes(xml[0]);
            byte[] buffer2 = Encoding.UTF8.GetBytes(xml[1]);
            var memoryStream1 = new MemoryStream(buffer);
            var memoryStream2 = new MemoryStream(buffer2);

            var settings = new Settings()
            {
                ImportedLibraries = paths.ToList()
            };

            var settingsProviderMock = new Mock<ISettingsProviderService>();
            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            var rootDirectory =
                new Folder()
                {
                    Name = "\\",
                    FullName = "\\"
                };
    
            var rootDirectories = new List<Model.Directory>
            {
                rootDirectory
            };

            var directoryServiceMock = new Mock<IDirectoryService>();
            directoryServiceMock.Setup(x => x.GetRootDirectories())
                .Returns(rootDirectories);

            var fileServiceMock = new Mock<IFileService>();

            fileServiceMock.Setup(x => x.OpenFile(paths.ToList()[0]))
                .Returns(memoryStream1);
            fileServiceMock.Setup(x => x.OpenFile(paths.ToList()[1]))
                .Returns(memoryStream2);

            var repository = new LocalLibraryRepository(fileServiceMock.Object, directoryServiceMock.Object, settingsProviderMock.Object);

            var libraries = repository.FindAsync(x => x.Name == "library2").Result;

            Assert.True(libraries != null);
            Assert.True(libraries.Any());
            Assert.Contains(libraries, x => x.Name == "library2" && x.Description == "picture library2");
        }
        #endregion

        #region RemoveAsync Tests
        [Fact]
        public async void RemoveAsync_ShouldCallRemoveMethod_ForLibraryFullPath()
        {
            var fileServiceMock = new Mock<IFileService>();
            var directoryServiceMock = new Mock<IDirectoryService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();

            var library =
                new Library()
                {
                    Name = "library1",
                    FullName = "Tests\\Folder1\\library1.plib"
                };

            fileServiceMock.Setup(x => x.Remove(library.FullName))
                .Verifiable();

            var repository = new LocalLibraryRepository(fileServiceMock.Object, directoryServiceMock.Object, settingsProviderMock.Object);

            await repository.RemoveAsync(library);

            fileServiceMock.Verify(x => x.Remove(library.FullName));  
        }
        #endregion
    }
}
