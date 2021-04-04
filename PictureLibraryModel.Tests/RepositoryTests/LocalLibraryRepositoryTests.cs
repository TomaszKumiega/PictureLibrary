using Moq;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Settings;
using PictureLibraryModel.Repositories.LibraryRepositories;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
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

        #region Helper methods
        private Library GetLibrary()
        {
            var library =
                new Library()
                {
                    FullName = "Tests\\library.plib",
                    Name = "library",
                    Description = "picture library",
                    Tags = new List<Tag>(),
                    Images = new List<ImageFile>(),
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
        public async void AddAsync_ShouldCallFileCreateMethod()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            var library = GetLibrary();

            fileServiceMock.Setup(x => x.Create(library.FullName))
                .Verifiable();
            
            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);

            await repository.AddAsync(library);

            fileServiceMock.Verify(x => x.Create(library.FullName));
        }

        [Fact]
        public async void AddAsync_ShouldCallOpenFileMethod_ForPreviouslyCreatedFile()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            var library = GetLibrary();

            fileServiceMock.Setup(x => x.OpenFile(library.FullName, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()))
                .Verifiable();

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);

            await repository.AddAsync(library);

            fileServiceMock.Verify(x => x.OpenFile(library.FullName, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()));
        }

        [Fact]
        public async void AddAsync_ShouldAddLibraryFullNameToSettings_AndSaveSettings()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);
            settingsProviderMock.Setup(x => x.SaveSettingsAsync())
                .Verifiable();

            var library = GetLibrary();

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);

            await repository.AddAsync(library);

            Assert.Contains(library.FullName, settings.ImportedLibraries);
            settingsProviderMock.Verify(x => x.SaveSettingsAsync());
        }

        [Fact]
        public async void AddAsync_ShouldCallWriteLibraryToStreamAsyncMethod()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            var library = GetLibrary();

            libraryFileServiceMock.Setup(x => x.WriteLibraryToStreamAsync(It.IsAny<FileStream>(), library))
                .Verifiable();

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);

            await repository.AddAsync(library);

            libraryFileServiceMock.Verify(x => x.WriteLibraryToStreamAsync(It.IsAny<FileStream>(), library));
        }
       
        #endregion

        #region GetAllAsync Tests
        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyList_WhenImportedLibrariesListIsNull()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = null
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);

            var libraries = await repository.GetAllAsync();

            Assert.Empty(libraries);
        }

        [Fact]
        public async void GetAllAsync_ShouldOpenFileAndReadFromIt_ForeachFilePathInImportedLibraries()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
                {
                    "Tests\\path1.plib",
                    "Tests\\path2.plib",
                    "Tests\\path3.plib"
                }
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            int openFileCallCounter = 0;
            int readLibraryCallCounter = 0;

            fileServiceMock.Setup(x => x.OpenFile(It.IsAny<string>(), It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()))
                .Callback(() => { openFileCallCounter++; });
            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(It.IsAny<Stream>(), It.IsAny<Origin>()))
                .Callback(() => { readLibraryCallCounter++; });

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            var libraries = await repository.GetAllAsync();

            Assert.True(openFileCallCounter == settings.ImportedLibraries.Count && readLibraryCallCounter == settings.ImportedLibraries.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfLibraries_WithAsManyElementsAsImportedLibrariesList()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();
            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
                {
                    "Tests\\path1.plib",
                    "Tests\\path2.plib",
                    "Tests\\path3.plib"
                }
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(It.IsAny<Stream>(), It.IsAny<Origin>()))
                .Returns(Task.FromResult(GetLibrary()));

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            var libraries = await repository.GetAllAsync();

            Assert.True(libraries.ToList().Count == settings.ImportedLibraries.Count);
        }
        #endregion

        #region FindAsync Tests
        [Fact]
        public async void FindAsync_ShouldReturnPartOfLibrariesListReturnedFromGetAllAsync_MatchingGivenPredicate()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();

            var libraries =
                new List<Library>()
                {
                    new Library()
                    {
                        FullName = "Tests\\path1.plib"
                    },

                    new Library()
                    { 
                        FullName = "Tests\\path2.plib"
                    },

                    new Library()
                    {
                        FullName = "Tests\\path3.plib"
                    }
                };

            var fileStreams =
                new List<Stream>()
                {
                    new MemoryStream(),
                    new MemoryStream(),
                    new MemoryStream()
                };

            var settings = new Settings()
            {
                ImportedLibraries = new List<string>()
                {
                    libraries[0].FullName,
                    libraries[1].FullName,
                    libraries[2].FullName
                }
            };

            settingsProviderMock.Setup(x => x.Settings)
                .Returns(settings);

            fileServiceMock.Setup(x => x.OpenFile(libraries[0].FullName, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()))
                .Returns(fileStreams[0]);
            fileServiceMock.Setup(x => x.OpenFile(libraries[1].FullName, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()))
                .Returns(fileStreams[1]);
            fileServiceMock.Setup(x => x.OpenFile(libraries[2].FullName, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()))
                .Returns(fileStreams[2]);

            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(fileStreams[0], It.IsAny<Origin>()))
                .Returns(Task.FromResult(libraries[0]));
            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(fileStreams[1], It.IsAny<Origin>()))
                .Returns(Task.FromResult(libraries[1]));
            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(fileStreams[2], It.IsAny<Origin>()))
                .Returns(Task.FromResult(libraries[2]));

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            var result = await repository.FindAsync(x => x.FullName == libraries[0].FullName || x.FullName == libraries[1].FullName);

            Assert.Contains(libraries[0], result);
            Assert.Contains(libraries[1], result);
        }
        #endregion

        #region RemoveAsync Tests
        [Fact]
        public async void RemoveAsync_ShouldCallRemoveMethod()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();

            fileServiceMock.Setup(x => x.Remove(It.IsAny<string>()))
                .Verifiable();

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            await repository.RemoveAsync("Tests\\path1.plib");

            fileServiceMock.Verify(x => x.Remove(It.IsAny<string>()));

        }

        [Fact]
        public async void RemoveRangeAsync_ShouldCallRemoveMethod_ForeachLibraryInACollection()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();

            var libraries =
                new List<Library>()
                {
                    new Library()
                    {
                        FullName = "Tests\\path1.plib"
                    },

                    new Library()
                    {
                        FullName = "Tests\\path2.plib"
                    }
                };

            fileServiceMock.Setup(x => x.Remove(libraries[0].FullName))
                .Verifiable();
            fileServiceMock.Setup(x => x.Remove(libraries[1].FullName))
                .Verifiable();

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            await repository.RemoveRangeAsync(libraries);

            fileServiceMock.Verify(x => x.Remove(libraries[0].FullName));
            fileServiceMock.Verify(x => x.Remove(libraries[1].FullName));

        }
        #endregion

        #region GetByPathAsync
        [Fact]
        public async void GetByPathAsync_ShouldOpenAndReadFromASpecifiedFile()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();

            var filePath = "Tests\\path1.plib";
            var memoryStream = new MemoryStream();

            fileServiceMock.Setup(x => x.OpenFile(filePath, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()))
                .Returns(memoryStream);
            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(memoryStream, It.IsAny<Origin>()))
                .Verifiable();

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            var library = await repository.GetByPathAsync(filePath);

            fileServiceMock.Verify(x => x.OpenFile(filePath, It.IsAny<FileMode>(), It.IsAny<FileAccess>(), It.IsAny<FileShare>()));
            libraryFileServiceMock.Verify(x => x.ReadLibraryFromStreamAsync(memoryStream, It.IsAny<Origin>()));
        }

        [Fact]
        public async void GetByPathAsync_ShouldReturnLibrary()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();

            var library =
                new Library()
                {
                    FullName = "Tests\\path1.plib"
                };

            libraryFileServiceMock.Setup(x => x.ReadLibraryFromStreamAsync(It.IsAny<Stream>(), It.IsAny<Origin>()))
                .Returns(Task.FromResult(library));

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);
            var result = await repository.GetByPathAsync(It.IsAny<string>());

            Assert.True(result.FullName == library.FullName);
        }
        #endregion

        #region UpdateAsync 
        [Fact]
        public async void UpdateAsync_ShouldThrowArgumentNullException_WhenLibraryIsNull()
        {
            var fileServiceMock = new Mock<IFileService>();
            var settingsProviderMock = new Mock<ISettingsProviderService>();
            var libraryFileServiceMock = new Mock<ILibraryFileService>();

            Library library = null;

            var repository = new LocalLibraryRepositoryStrategy(fileServiceMock.Object, settingsProviderMock.Object, libraryFileServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.UpdateAsync(library));

        }
        #endregion
    }
}
