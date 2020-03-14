using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureLibraryModel.Repositories
{
    public interface ICloudRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
