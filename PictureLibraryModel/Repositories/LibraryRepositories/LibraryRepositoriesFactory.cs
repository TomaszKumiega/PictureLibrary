using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LibraryRepositoriesFactory : ILibraryRepositoriesFactory
    {
        public IRepository<Library> GetLocalLibraryRepository()
        {
            return new LocalLibraryRepository(new FileService(), new DirectoryService());
        }
    }
}
