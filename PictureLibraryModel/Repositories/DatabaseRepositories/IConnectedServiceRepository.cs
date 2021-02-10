using PictureLibraryModel.Model.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories.DatabaseRepositories
{
    public interface IConnectedServiceRepository : IRepository<ConnectedService>
    {
        Task<IEnumerable<ConnectedService>> GetByUserId(Guid id);
    }
}
