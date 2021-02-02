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

        private List<string> GetProperties()
        {
            return (from prop in typeof(T).GetProperties()
                    select prop.Name).ToList();
        }

        private string GetInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");

            var properties = GetProperties();
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1) // remove last comma
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1) // remove last comma
                .Append(")");

            return insertQuery.ToString();
        }

        private string GetUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GetProperties();

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); // remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
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
