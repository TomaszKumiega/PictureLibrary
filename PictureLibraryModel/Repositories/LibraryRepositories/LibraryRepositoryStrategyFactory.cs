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
    public class LibraryRepositoryStrategyFactory : ILibraryRepositoryStrategyFactory
    {
        private ISettingsProviderService SettingsProvider { get; }

        public LibraryRepositoryStrategyFactory(ISettingsProviderService settingsProvider)
        {
            SettingsProvider = settingsProvider;
        }

        public IRepository<Library> GetLocalLibraryRepository()
        {
            return new LocalLibraryRepositoryStrategy(new FileService(), SettingsProvider, new LibraryFileService(new ImageFileBuilder()));
        }
    }
}
