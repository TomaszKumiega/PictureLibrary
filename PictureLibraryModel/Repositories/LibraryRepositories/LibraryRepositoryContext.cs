using PictureLibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories.LibraryRepositories
{
    public class LibraryRepositoryContext : ILibraryRepositoryContext
    {
        public ILibraryRepositoryStrategy Strategy { get; set; }

        public async Task AddAsync(Library entity)
        {
            await Strategy.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Library> entities)
        {
            await Strategy.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<Library>> FindAsync(Predicate<Library> predicate)
        {
            return await Strategy.FindAsync(predicate);
        }

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            return await Strategy.GetAllAsync();
        }

        public async Task RemoveAsync(Library entity)
        {
            await Strategy.RemoveAsync(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<Library> entities)
        {
            await Strategy.RemoveRangeAsync(entities);
        }

        public async Task UpdateAsync(Library entity)
        {
            await Strategy.UpdateAsync(entity);
        }
    }
}
