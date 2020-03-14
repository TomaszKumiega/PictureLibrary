using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public interface ICloudRepository<T> where T : class
    {
        T GetFromCloud(int id);
        IEnumerable<T> GetAllFromCloud();

        void AddToCloud(T entity);
        void AddRangeToCloud(IEnumerable<T> entities);

        void RemoveFromCloud(T entity);
        void RemoveRangeFromCloud(IEnumerable<T> entities);
    }
}
