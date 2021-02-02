using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public interface ISettingsRepository : IRepository<Settings.Settings>
    {
        Task<Settings.Settings> GetByUserId(Guid id);
    }
}
