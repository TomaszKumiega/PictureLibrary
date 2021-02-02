using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public abstract class DatabaseRepository<T> : IRepository<T> where T: class
    {
        private string _tableName;

        public DatabaseRepository(string tableName)
        {
            _tableName = tableName;
        }

        private string GetConnectionString()
        {
            return "Data Source=.\\picture_library.db;Version=3;";
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using(IDbConnection conn = new SQLiteConnection(GetConnectionString()))
            {
                return await conn.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
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
