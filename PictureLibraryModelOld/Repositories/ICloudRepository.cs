using System.Collections.Generic;

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
