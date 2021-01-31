using PictureLibraryModel.Model;
using PictureLibraryModel.Services.FileSystemServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public class LocalLibraryRepository : ILibraryRepository
    {
        private IFileService _fileService;

        public LocalLibraryRepository(IFileService fileService)
        {
            _fileService = fileService;
        }

        public Task AddAsync(Library library)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Library> libraries)
        {
            throw new NotImplementedException();
        }

        public Task<Library> FindAsync(Predicate<Library> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Library>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Library> GetByPathAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Library library)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Library> libraries)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Library library)
        {
            throw new NotImplementedException();
        }
    }
}
