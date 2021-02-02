using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public interface ISettingsRepository : IRepository<Model.Settings.Settings>
    {
        Task<Model.Settings.Settings> GetByUserId(Guid id);
    }
}
