using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public interface ILibraryRepository
    {
        Task AddAsync(Library library);
        Task AddRangeAsync(IEnumerable<Library> libraries);
        Task<Library> FindAsync(Predicate<Library> predicate);
        Task<IEnumerable<Library>> GetAllAsync();
        Task<Library> GetByPathAsync(string path);
        Task RemoveAsync(string path);
        Task RemoveAsync(Library library);
        Task RemoveRangeAsync(IEnumerable<Library> libraries);
        Task UpdateAsync(Library library);
    } 
}
