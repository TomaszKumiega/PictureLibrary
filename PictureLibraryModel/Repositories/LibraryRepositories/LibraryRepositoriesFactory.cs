using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LibraryRepositoriesFactory : ILibraryRepositoriesFactory
    {
        public ILibraryRepository GetLocalLibraryRepository()
        {
            return new LocalLibraryRepository(new FileService());
        }
    }
}
