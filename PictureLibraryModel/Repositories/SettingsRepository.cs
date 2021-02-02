using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public class SettingsRepository : DatabaseRepository<Settings.Settings>, ISettingsRepository
    {
        public SettingsRepository() : base("Settings")
        {

        }

        public async Task<Settings.Settings> GetByUserId(Guid id)
        {
            using(var conn = GetConnection())
            {
                var list = (await conn.QueryAsync<Settings.Settings>($"SELECT * FROM {_tableName} WHERE UserId=@Id", new { Id = id })).ToList();

                if (list.Any()) return list[0];
                else return null;
            }
        }
    }
}
