using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public abstract class DatabaseRepository<T> : IRepository<T>
    {
        private string _tableName;

        public DatabaseRepository(string tableName)
        {
            _tableName = tableName;
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
