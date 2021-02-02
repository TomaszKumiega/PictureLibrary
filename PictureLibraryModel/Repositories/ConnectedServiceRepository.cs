using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using PictureLibraryModel.Model.ConnectedServices;

namespace PictureLibraryModel.Repositories
{
    public class ConnectedServiceRepository : DatabaseRepository<ConnectedService>, IConnectedServiceRepository
    {
        public ConnectedServiceRepository() : base("ConnectedServices")
        {

        }

        public async Task<IEnumerable<ConnectedService>> GetByUserId(Guid id)
        {
            using (var conn = GetConnection())
            {
                return (await conn.QueryAsync<ConnectedService>($"SELECT * FROM {_tableName} WHERE UserId=@Id", new { Id = id })).ToList();
            }
        }
    }
}
