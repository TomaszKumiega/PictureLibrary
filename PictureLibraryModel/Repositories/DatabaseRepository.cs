using Dapper;
using Microsoft.Data.Sqlite;
using PictureLibraryModel.Model;
using PictureLibraryModel.Model.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public abstract class DatabaseRepository<T> : IRepository<T> where T: IDatabaseEntity
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

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        private List<string> GetListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
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
