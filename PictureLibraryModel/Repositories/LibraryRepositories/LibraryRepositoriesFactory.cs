using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Builders.ImageFileBuilder;
using PictureLibraryModel.Services.FileSystemServices;
using PictureLibraryModel.Services.LibraryFileService;
using PictureLibraryModel.Services.SettingsProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LibraryRepositoriesFactory : ILibraryRepositoriesFactory
    {
        private ISettingsProviderService SettingsProvider { get; }

        public LibraryRepositoriesFactory(ISettingsProviderService settingsProvider)
        {
            SettingsProvider = settingsProvider;
        }

        public IRepository<Library> GetLocalLibraryRepository()
        {
            return new LocalLibraryRepository(new FileService(), SettingsProvider, new LibraryFileService(new ImageFileBuilder()));
        }
    }
}
